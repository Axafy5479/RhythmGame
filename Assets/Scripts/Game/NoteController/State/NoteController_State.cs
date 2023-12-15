namespace Game
{
    public abstract class NoteController_State
    {
        
        protected NoteController Controller { get; }

        public abstract void OnEnter(NoteController noteController, NoteController_State previousState);

        public abstract void OnUpdate(NoteController noteController);
        
        public abstract void OnExit(NoteController noteController,NoteController_State nextState);
    }
}