namespace Game.Plan
{
    /// <summary>
    ///     ノーツIdの生成クラス
    /// </summary>
    public class IdGenerator
    {
        /// <summary>
        ///     使用済みのId
        /// </summary>
        private int id;

        /// <summary>
        ///     Idを一つ生成する
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            id++;
            return id;
        }

        /// <summary>
        ///     Idを複数生成
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int[] GetIds(int number)
        {
            var ids = new int[number];
            for (var i = 0; i < number; i++)
            {
                ids[i] = GetId();
            }

            return ids;
        }
    }
}