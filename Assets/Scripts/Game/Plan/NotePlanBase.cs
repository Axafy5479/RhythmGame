namespace Game.Plan
{
    /// <summary>
    ///     milli secで表現したノーツ情報
    ///     譜面エディタ出力のNoteDataは時刻をlpb, numベースで表現しており、
    ///     ゲームを作り上げる上で使いにくい
    /// </summary>
    public abstract class NotePlanBase
    {
        public NotePlanBase(int noteId, int beatTime, int block, float bpm)
        {
            BeatTime = beatTime;
            Block = block;
            LaunchTime = beatTime - (int)(240 * 1000 / bpm);
            NoteId = noteId;
        }

        /// <summary>
        ///     ノーツId
        /// </summary>
        public int NoteId { get; }

        /// <summary>
        ///     判定時刻 milli sec
        /// </summary>
        public int BeatTime { get; }

        /// <summary>
        ///     射出時刻 milli sec
        /// </summary>
        public int LaunchTime { get; }

        /// <summary>
        ///     ノーツが流れるレーン
        /// </summary>
        public int Block { get; }

        public override string ToString()
        {
            return $"type:{GetType()} noteId:{NoteId} beatTime:{BeatTime} launchTime:{LaunchTime} block:{Block}";
        }
    }
}