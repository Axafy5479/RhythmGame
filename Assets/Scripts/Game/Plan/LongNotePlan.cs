namespace Game.Plan
{
    /// <summary>
    ///     ロングノーツの中で、先頭に位置するノーツのPlan
    /// </summary>
    public class LongHeadNotePlan : NotePlanBase
    {
        public LongHeadNotePlan(int noteId, int[] backNoteIds, int beatTime, int block, float bpm) : base(noteId,
            beatTime, block, bpm)
        {
            BackNoteIds = backNoteIds;
        }

        /// <summary>
        ///     子ノーツのId
        /// </summary>
        public int[] BackNoteIds { get; }

        public override string ToString()
        {
            return $"{base.ToString()} childId:[{string.Join(",", BackNoteIds)}]";
        }
    }

    /// <summary>
    ///     ロングノーツの中で、先頭以外のノーツのPlan
    /// </summary>
    public class LongBackNotePlan : NotePlanBase
    {
        public LongBackNotePlan(int noteId, int headId, int beatTime, int block, float bpm) : base(noteId, beatTime,
            block, bpm)
        {
            HeadId = headId;
        }

        /// <summary>
        ///     ロングtopノーツのId
        /// </summary>
        public int HeadId { get; }

        public override string ToString()
        {
            return $"{base.ToString()} headId:{HeadId}";
        }
    }
}