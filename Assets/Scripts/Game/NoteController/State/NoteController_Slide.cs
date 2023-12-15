using UnityEngine;

namespace Game
{
    /// <summary>
    ///     ロングノーツの場合に設定されるState
    /// </summary>
    public class NoteController_Slide : NoteController_State
    {
        // スライドの開始位置と終了位置
        private float startPosX, goalPosX;

        // スライドの開始時間と終了時間
        private int startTime, goalTime;

        /// <summary>
        ///     引数の時間にどこに位置すべきか
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private float GetPosXByTime(int time)
        {
            return startPosX + (goalPosX - startPosX) * (time - startTime) / (goalTime - startTime);
        }

        public override void OnEnter(NoteController noteController, NoteController_State previousState)
        {
            // 現在の位置
            var currentPos = noteController.Trn.position;

            // 子ノーツの振る舞い
            var childPlan = noteController.Properties.Plan.ChildPlan;

            // スライド開始時刻
            startTime = noteController.Properties.Plan.BeatTime;

            // 初期位置の設定
            startPosX = currentPos.x;

            // 終了時刻は子ノーツのbeat時間
            goalTime = childPlan.BeatTime;

            // 終了位置のx座標は、子ノーツの位置
            goalPosX = Field.Instance.GetSpawnPos(childPlan.Block).x;
        }

        public override void OnUpdate(NoteController noteController)
        {
            // 現在時刻
            var currentTime = TimeCalculator.Instance.GetTime();

            // 現在位置
            var currentPos = noteController.Trn.position;

            // 指がノーツ上に存在していることの確認
            var fingerExist = false;
            noteController.Properties.Fingers.RemoveWhere(finger =>
            {
                // fingerが非アクティブ(=指を離した)の時は、trueを返す(=Fingersから取り除く)
                if (!finger.isActive) return true;

                // 指とノーツの位置ずれの計算
                var (touchPos, ok) = Field.Instance.ScreenToFieldPosition(finger.lastTouch.screenPosition);

                // fingerが場外なので、trueを返す(=Fingersから取り除く)
                if (!ok) return true;

                // ノーツからのx座標ズレを計算
                var gap = Mathf.Abs(currentPos.x - touchPos.x);

                // 指がゆびがオーツオブジェクト上にあるか (猶予 + オブジェクトサイズ)
                var onNote = gap < (Settings.Instance.NoteSlideTolerance + 1)/2;

                if (onNote)
                {
                    // ノーツをホールドしている指が存在している
                    fingerExist = true;

                    // オブジェクト上ならfalseを返す(=Fingersに残す)
                    return false;
                }

                // 指がゆびがオーツオブジェクト上にないためtrueを返す(=Fingersから取り除く)

                return true;
            });

            if (!fingerExist)
            {
                // このノーツをホールドする指がないため、スライドStateを終了
                noteController.Judged(false);
                return;
            }

            if (currentTime > goalTime)
            {
                // スライド終了時間に到達
                noteController.Judged(false);
                return;
            }

            // ノーツを正しい位置に配置
            var newX = GetPosXByTime(currentTime);
            noteController.Trn.position = new Vector3(newX, currentPos.y, 0);
        }

        public override void OnExit(NoteController noteController, NoteController_State nextState)
        {
        }
    }
}