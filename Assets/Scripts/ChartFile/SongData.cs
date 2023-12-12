using System;
using System.Collections.Generic;

namespace ChartFile
{
    /// <summary>
    ///     楽曲の情報をまとめたモデルクラス
    ///     譜面エディタの出力.jsonをデシリアライズすることでインスタンス化され、ChartFileのフィールドに格納される
    /// </summary>
    [Serializable]
    public class SongData
    {
        /// <summary>
        ///     曲名
        /// </summary>
        public string title;

        /// <summary>
        ///     作曲者名
        /// </summary>
        public string artist;

        /// <summary>
        ///     曲選択時に表示する画像
        ///     必ずこのファイルと同じディレクトリに保存すること
        /// </summary>
        public string banner;

        /// <summary>
        ///     難易度
        /// </summary>
        public List<string> difficulties;

        /// <summary>
        ///     曲選択時に何秒から曲を流し始めるか
        /// </summary>
        public string sampleStart;

        /// <summary>
        ///     曲選択時に何秒間曲をすか
        /// </summary>
        public string sampleLength;

        /// <summary>
        ///     初期BPM
        /// </summary>
        public string baseBpm;


        // BPM変化
        // おそらく使われていない?
        // public List<BPMInfo> bpms;


        ////////////////////////////////////////////////////////
        ///// jsonファイルには存在するが、使用予定のないフィールド /////
        ////////////////////////////////////////////////////////
        //
        // "speed"  // スクロール速度
        // "bgChanges"  // 多分背景の変化
        // "lastSecondHint" // 不明
        // "cmod" // スクロール速度を固定するか
        // "luaScript // 組み込みスクリプト
        //
        // 
        //
        // 譜面と音楽のタイミング合わせ(秒)
        // Chartとは、単位も正負も違うので使用しない
        // "offset"
        //
        //
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
    }
}