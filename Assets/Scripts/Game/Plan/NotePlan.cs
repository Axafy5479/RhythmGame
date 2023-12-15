namespace Game.Plan
{
    /// <summary>
    ///     milli secで表現したノーツ情報
    ///     譜面エディタ出力のNoteDataは時刻をlpb, numベースで表現しており、
    ///     ゲームを作り上げる上で使いにくい
    /// </summary>
    public class NotePlan
    {
        public NotePlan(int noteId, int beatTime, int block, float bpm)
        {
            BeatTime = beatTime;
            Block = block;
            LaunchTime = beatTime - (int)(240 * 1000 / bpm);
            NoteId = noteId;
            Bpm = bpm;
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

        /// <summary>
        ///     このノーツの判定時刻でのBPM
        /// </summary>
        public float Bpm { get; }

        /// <summary>
        ///     子ノーツのPlan
        ///     nullの時は通常のーつ
        /// </summary>
        public NotePlan ChildNote { get; private set; }

        /// <summary>
        ///     子ノーツを設定する
        /// </summary>
        /// <param name="childId"></param>
        public void SetChild(NotePlan childId)
        {
            ChildNote = childId;
        }

        public override string ToString()
        {
            return
                $"noteId:{NoteId,4}\tbeatTime:{BeatTime,6}\tlaunchTime:{LaunchTime,6}\tblock:{Block}\tchildNote:{ChildNote,1}\tbpm:{Bpm}";
        }
    }
}