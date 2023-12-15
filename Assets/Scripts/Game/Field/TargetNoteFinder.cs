using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    public static class TargetNoteFinder
    {
         /// <summary>
        /// 位置と時間的に最も適切なノーツを見つける
        /// </summary>
        /// <param name="finger"></param>
        public static NoteController Find(HashSet<NoteController> controllersOnField,float posX, int beatTime)
        {

            // 今回のタッチに対し、最適なターゲットを探し、この変数に代入していく
            NoteController target = null;
            
            // 各ノーツに対し、判定対象として適切か否かを表す「Score」を計算する
            // 上記targetのScoreをここに保持する(値が小さいほどより適切)
            var minScore = float.MaxValue;

            foreach (var note in controllersOnField)
            {
                var plan = note.Properties.Plan;

                // 位置ズレ、時間ズレを計算
                var gapPosX = note.Trn.position.x - posX;
                var gapTime = plan.BeatTime - beatTime;

                // あまりにもズレが大きい場合は無視する
                var x_threshold = (Settings.Instance.NoteTapTolerance + 1) / 2;
                var time_threshold = Settings.Instance.JudgeTime(JudgeEnum.Miss);
                if(Mathf.Abs(gapPosX)>x_threshold || Mathf.Abs(gapTime)>time_threshold) continue;
                
                // このノーツを判定すべきかを表すScoreを計算
                var score = gapTime * gapTime + (float)(gapPosX * gapPosX * 0.25e6);

                if (note.Properties.State is NoteController_Slide)
                {
                    // このノーツがスライド中の場合 (つまり持ち替え操作の場合)
                    // 大きくハンデをつける。
                    // (持ち替えの失敗より、ノーツを見逃すことの方が圧倒的に痛いため)
                    score += (float)1e8;
                }

                // scoreが現在の最小値を下回った時
                if (score < minScore)
                {
                    // 判定対象を更新する
                    minScore = score;
                    target = note;
                }
            }

            return target;
        }
    }
}