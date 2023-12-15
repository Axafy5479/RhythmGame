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
            // 移動開始時刻は現在時刻
            startTime = TimeCalculator.Instance.GetTime();

            // 移動終了時刻はbeat時間
            goalTime = noteController.Properties.Plan.BeatTime;

            // 移動開始位置は今の位置
            startPosZ = noteController.Trn.position.z;
        }

        public override void OnUpdate(NoteController noteController)
        {
            // 現在の位置を取得
            var currentPos = noteController.Trn.position;

            // 移動先の計算
            var newZ = GetPosZByTime(TimeCalculator.Instance.GetTime());

            // 移動
            noteController.Trn.position = new Vector3(currentPos.x, currentPos.y, newZ);
        }

        public override void OnExit(NoteController noteController, NoteController_State nextState)
        {
        }
    }
}