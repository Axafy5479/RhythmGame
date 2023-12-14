namespace Game
{
    public abstract class NoteController_State
    {
        public NoteController_State(NoteController noteController)
        {
            Controller = noteController;
        }
        
        protected NoteController Controller { get; }

        public abstract void OnEnter();

        public abstract void OnUpdate();
    }
}