namespace Game
{
    public class NoteController_Move : NoteController_State
    {
        private int startTime, goalTime;
        private float initialPosZ;
        private float goalPosZ = 0;
        
        public override void OnEnter(NoteController noteController, NoteController_State previousState)
        {
            startTime = TimeCalculator.Instance.GetTime()
            noteController.
        }

        public override void OnUpdate(NoteController noteController)
        {
            noteController.Trn
        }

        public override void OnExit(NoteController noteController, NoteController_State nextState)
        {
            
        }
    }
}