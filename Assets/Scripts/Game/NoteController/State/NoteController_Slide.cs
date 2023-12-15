using UnityEngine;

namespace Game
{
    public class NoteController_Slide : NoteController_State
    {
        private int startTime, goalTime;
        private float initialPosZ;
        private float goalPosZ = 0;

        public override void OnEnter(NoteController noteController, NoteController_State previousState)
        {
            startTime = noteController.Properties.Plan.BeatTime;
           // goalTime = noteController.Properties.Plan.ChildNote
            var currentPos = noteController.Trn.position;
            noteController.Trn.position = new Vector3(currentPos.x, currentPos.y, 0);
            
        }

        public override void OnUpdate(NoteController noteController)
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit(NoteController noteController, NoteController_State nextState)
        {
            throw new System.NotImplementedException();
        }
    }
}