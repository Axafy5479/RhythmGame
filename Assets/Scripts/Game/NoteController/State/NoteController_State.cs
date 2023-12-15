namespace Game
{
    /// <summary>
    ///     NoteControllerのState
    /// </summary>
    public abstract class NoteController_State
    {
        /// <summary>
        ///     この状態になった時に元に呼ばれる
        ///     OnUpdate() より先に呼ばれる
        /// </summary>
        /// <param name="noteController">この状態を持つNoteController</param>
        /// <param name="previousState">この状態に遷移する前の状態</param>
        public abstract void OnEnter(NoteController noteController, NoteController_State previousState);

        /// <summary>
        ///     マイフレーム呼ばれるメソッド
        ///     OnEnter()より後に呼ばれる
        /// </summary>
        /// <param name="noteController">この状態を持つNoteController</param>
        public abstract void OnUpdate(NoteController noteController);

        /// <summary>
        ///     この状態から他の状態に遷移する時に呼ばれる
        /// </summary>
        /// <param name="noteController">この状態を持つNoteController</param>
        /// <param name="nextState">遷移先の状態</param>
        public abstract void OnExit(NoteController noteController, NoteController_State nextState);
    }
}