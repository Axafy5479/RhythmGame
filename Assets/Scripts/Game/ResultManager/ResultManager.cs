using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 判定やスコアなど、プレイ結果を保持するクラス
    /// </summary>
    public class ResultManager : MonoSingleton<ResultManager>, INeedInitializing
    {
        /// <summary>
        /// 全ノーツ数
        /// </summary>
        public int AllNoteNum { get; private set; }
        
        /// <summary>
        /// NoteIdと、そのノーツの結果を文字する辞書
        /// </summary>
        private Dictionary<int, NoteResult> ResultMap { get; set; }
        
        /// <summary>
        /// 現在のコンボ数
        /// </summary>
        public int CurrentCombo { get; private set; }
        
        /// <summary>
        /// 最大コンボ数
        /// </summary>
        public int MaxCombo { get; private set; }

        /// <summary>
        /// 各判定を出した個数
        /// </summary>
        public Dictionary<JudgeEnum, int> JudgeNumMap { get; } = new()
        {
            { JudgeEnum.Perfect, 0 }, { JudgeEnum.Good, 0 }, { JudgeEnum.Miss, 0 },{ JudgeEnum.None, 0 },
        };
        
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="gameInfo"></param>
        public void Initialize(GameInfo gameInfo)
        {
            // id、ノーツの判定結果の辞書をあらかじめ作成しておく
            ResultMap = gameInfo.GetPlanMap().Keys.ToDictionary(id => id,id=>new NoteResult(id));
            
            // 全ノーツ数
            AllNoteNum = gameInfo.GetPlanMap().Count;
        }
        

        /// <summary>
        /// ノーツの判定を記録する
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="judge"></param>
        /// <param name="timeGap"></param>
        public void AddResult(int noteId, JudgeEnum judge, int timeGap)
        {
            if (judge is JudgeEnum.Perfect or JudgeEnum.Good)
            {
                // PerfectかGoodの場合はコンボを増加させる
                CurrentCombo++;
                MaxCombo = Mathf.Max(MaxCombo, CurrentCombo);
            }
            
            // Missの場合はコンボをリセット
            else CurrentCombo = 0;

            // そのjudgeの個数を増加
            JudgeNumMap[judge]++;
            
            // 結果を記録
            ResultMap[noteId].SetResult(judge,timeGap);
            
            Debug.Log(ResultMap[noteId]+"\tScore"+Score);
        }

        /// <summary>
        /// Maxで10000点の得点
        /// </summary>
        public int Score
        {
            get
            {
                int perfect = JudgeNumMap[JudgeEnum.Perfect];
                int good = JudgeNumMap[JudgeEnum.Good];
                return (10000 * perfect + 5000 * good)/AllNoteNum;
            }
        }
    }

    /// <summary>
    /// ノーツの判定結果
    /// </summary>
    public class NoteResult
    {
        public NoteResult(int noteId)
        {
            NoteId = noteId;
        }

        /// <summary>
        /// 判定結果を設定
        /// </summary>
        /// <param name="judge"></param>
        /// <param name="gapTime"></param>
        public void SetResult(JudgeEnum judge, int gapTime)
        {
            JudgeEnum = judge;
            GapTime = gapTime;
        }
        
        /// <summary>
        /// ノーツId
        /// </summary>
        public int NoteId { get; }
        
        /// <summary>
        /// 判定
        /// </summary>
        public JudgeEnum JudgeEnum { get; set; }
        
        /// <summary>
        /// 叩いた時刻のズレ
        /// </summary>
        public int GapTime { get; set; }

        public override string ToString()
        {
            return $"noteId:{NoteId,4}\tJudge:{JudgeEnum,8}\tGapTime:{GapTime,4}";
        }
    }
}