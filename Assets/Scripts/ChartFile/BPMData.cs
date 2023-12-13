using System;

namespace Chart
{
    /// <summary>
    ///     BPMがいつ、どの値に変化するかを保持するモデルクラス
    ///     譜面エディタの出力.jsonをデシリアライズすることでインスタンス化され、ChartFileのフィールドに格納される
    /// </summary>
    [Serializable]
    public class BPMData
    {
        /// <summary>
        ///     変化後のBPM値
        /// </summary>
        public float bpm;

        /// <summary>
        ///     lines per beat
        ///     1ビート(1/4小節)にいくつlineが引かれているか
        /// </summary>
        public int lpb;


        /// <summary>
        ///     このBPM変化のタイミング
        ///     (何line目かで表現)
        /// </summary>
        public int num;
    }
}