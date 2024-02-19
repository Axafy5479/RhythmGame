using System.Collections.Generic;
using Game.Plan;
using UniRx;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    /// <summary>
    ///     NoteControllerが持つ、そのノーツの特定のプロパティをまとめたクラス
    ///     NoteControllerは判定後も削除されずオブジェクトプールに帰るので、使いまわされる。
    ///     使いまわされた時、以前のノーツの情報が残っているとバグに繋がる。
    ///     このような事態を防ぐため、特定のノーツの情報は NoteControllerProperty に集約し、
    ///     間違いなくリセットされるように注意する
    /// </summary>
    public class NoteControllerProperty
    {
        /// <summary>
        ///     このノーツが出した判定
        /// </summary>
        public JudgeEnum Judge { get; private set; }

        /// <summary>
        ///     Beat時間のエラー
        /// </summary>
        public int GapTime { get; private set; } = int.MaxValue;

        /// <summary>
        ///     このノーツの振る舞い
        /// </summary>
        public NotePlan Plan { get; private set; }

        /// <summary>
        ///     このノーツをホールドしている指
        /// </summary>
        public HashSet<Finger> Fingers { get; } = new();

        /// <summary>
        ///     このノーツの状態
        /// </summary>
        public NoteController_State State { get; private set; }

        /// <summary>
        ///     このオブジェクトが有効であるか
        ///     (プールの中にいるならfalse、取り出されているならtrue)
        /// </summary>
        public ReactiveProperty<bool> IsValid { get; } = new(false);

        public NoteController ChildNote { get; set; }

        /// <summary>
        ///     ノーツの振る舞いを設定し、ノーツとして動く状態にする
        /// </summary>
        /// <param name="plan"></param>
        public void Activated(NotePlan plan)
        {
            Plan = plan;
            IsValid.Value = true;
        }

        /// <summary>
        ///     このノーツの状態を変更sる
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="newState"></param>
        public void ChangeState(NoteController controller, NoteController_State newState)
        {
            // 現時点での状態の終了メソッドを呼ぶ
            State?.OnExit(controller, newState);

            // 新しい状態の開始メソッドを呼ぶ
            newState?.OnEnter(controller, State);

            // プロパティの更新
            State = newState;
        }

        public void SetJudge(Finger finger, bool isPerfect = false)
        {
            if (finger == null)
            {
                Judge = JudgeEnum.Miss;
            }
            else
            {
                if (!isPerfect)
                {
                    int time = TimeCalculator.Instance.GetBeatTime(finger);
                    GapTime = Plan.BeatTime - time;
                }
                else
                {
                    GapTime = 0;
                }
                Judge = Settings.Instance.GetJudge(GapTime);

            }

            ResultManager.Instance.AddResult(Plan.NoteId,Judge,GapTime);
        }


        /// <summary>
        ///     このノーツの情報を削除する
        /// </summary>
        /// <param name="controller"></param>
        public void Clear(NoteController controller)
        {
            ChangeState(controller, null);
            IsValid.Value = false;
            Judge = JudgeEnum.None;
            Plan = null;
            Fingers.Clear();
            ChildNote = null;
            GapTime = int.MaxValue;
        }
    }
}