using UnityEngine;

namespace Game
{
    public class NoteController_Move : NoteController_State
    {
        private int startTime, goalTime;
        private float initialPosZ;
        private float goalPosZ = 0;

        private float GetPosZByTime(int time)
        {
            return initialPosZ + (goalPosZ - initialPosZ) * (time - startTime) / (goalTime - startTime);
        }
        
        
        public override void OnEnter(NoteController noteController, NoteController_State previousState)
        {
            startTime = TimeCalculator.Instance.GetTime();
            goalTime = noteController.Properties.Plan.BeatTime;
            initialPosZ = noteController.Trn.position.z;
        }

        public override void OnUpdate(NoteController noteController)
        {
            var currentPos = noteController.Trn.position;
            var newZ = GetPosZByTime(TimeCalculator.Instance.GetTime());
            noteController.Trn.position = new Vector3(currentPos.x, currentPos.y,newZ);
        }

        public override void OnExit(NoteController noteController, NoteController_State nextState)
        {
        }
    }
}