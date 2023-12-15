using UnityEngine;

namespace Game
{
    public class NoteController_Move : NoteController_State
    {
        // 判定位置
        private readonly float goalPosZ = 0;

        // 初期位置
        private float startPosZ;

        // 開始 & 終了時刻
        private int startTime, goalTime;

        /// <summary>
        ///     引数の時間にどこに位置すべきか
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private float GetPosZByTime(int time)
        {
            return startPosZ + (goalPosZ - startPosZ) * (time - startTime) / (goalTime - startTime);
        }


        public override void OnEnter(NoteController noteController, NoteController_State previousState)
        {
            // 移動開始時刻はPlanのLaunchTime
            startTime = noteController.Properties.Plan.LaunchTime;

            // 移動終了時刻はbeat時間
            goalTime = noteController.Properties.Plan.BeatTime;

            // 移動開始位置は今の位置
            startPosZ = noteController.Trn.position.z;
        }

        public override void OnUpdate(NoteController noteController)
        {
            var currentTime = TimeCalculator.Instance.GetTime();
            
            // 現在の位置を取得
            var currentPos = noteController.Trn.position;

            // 移動先の計算
            var newZ = GetPosZByTime(currentTime);

            // 移動
            noteController.Trn.position = new Vector3(currentPos.x, currentPos.y, newZ);

            CheckOutOfField(noteController, currentTime);
        }

        private void CheckOutOfField(NoteController noteController, int time)
        {
            // 本来叩くべき時間からのズレを計算 (このプロジェクトでは、遅い場合に負の数になるよう統一している)
            var delta = noteController.Properties.Plan.BeatTime - time;

            // deltaは遅いときに負なので、マイナスをつけて正にする。
            // Missの限界値より小さい時は削除しない
            if (Settings.Instance.JudgeTime(JudgeEnum.Miss) > -delta) return;

            // 画面内の場合は削除しない
            if (noteController.NoteSprite.isVisible) return;

            // 画面外かつMissの限界も超えたため削除対象
            noteController.Judged(false);
        }

        public override void OnExit(NoteController noteController, NoteController_State nextState)
        {
        }
    }
}