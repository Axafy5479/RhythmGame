using System;
using System.Collections.Generic;

namespace ChartFile
{
    /// <summary>
    ///     楽曲情報と、難易度ごとの難易度ごとの譜面を保持するモデルクラス
    ///     譜面エディタの出力.jsonをデシリアライズすることでインスタンス化される
    /// </summary>
    [Serializable]
    public class ChartData
    {
        /// <summary>
        ///     楽曲ファイル名 (拡張子なし)
        /// </summary>
        public string name;

        /// <summary>
        ///     楽曲情報
        /// </summary>
        public SongData songInfo;

        /// <summary>
        ///     最大レーン数
        ///     最大7レーン
        /// </summary>
        public int maxBlock;

        /// <summary>
        ///     初期BPM
        /// </summary>
        public float bpm;


        /// <summary>
        ///     譜面と音楽のタイミング合わせ。
        ///     単位はサンプル(44100サンプル=1秒)
        ///     SongInfoとは、単位も正負も違うので注意
        ///     オフセットが正の場合、楽曲に対してノーツが遅れて到達する
        /// </summary>
        public int offset;

        /// <summary>
        ///     BPM変化のタイミングとその値
        ///     一般に楽曲のBPMは途中で変化する。
        ///     いつ、どの値に変化をするかを表すリスト
        /// </summary>
        public List<BPMInfo> bpms;

        /// <summary>
        ///     譜面データ
        ///     Note[難易度][ノーツ]
        /// </summary>
        public NoteData[][] notes;

        /// <summary>
        ///     難易度ごとのbpm変化情報
        ///     (上に定義したbpmフィールドの内容を、難易度別に定義した場合に使用する)
        /// </summary>
        public BPMInfo[][] splitBpms;

        ////////////////////////////////////////////////////////
        ///// jsonファイルには存在するが、使用予定のないフィールド /////
        ////////////////////////////////////////////////////////
        //
        // speeds  // おそらくノーツのスクロール速度
        // bgChanges  // おそらくゲーム背景の変化
        //
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
    }
}