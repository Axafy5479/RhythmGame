using System;
using System.Collections.Generic;

namespace ChartFile
{
    /// <summary>
    ///     ノートの挙動を表現するパラメーターを保持するモデルクラス
    ///     譜面エディタの出力.jsonをデシリアライズすることでインスタンス化され、ChartFileのフィールドに格納される
    /// </summary>
    [Serializable]
    public class NoteData
    {
        /// <summary>
        ///     どのレーンを流れるか
        ///     左が0
        /// </summary>
        public int block;


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

        /// <summary>
        ///     ノーツの種類
        ///     1 ... オレンジ 通常
        ///     2 ... オレンジ 連結
        ///     3 ... 緑 通常
        ///     4 ... 緑 連結
        /// </summary>
        public int type;

        /// <summary>
        ///     このノーツの後ろに続くノーツ (ロングノーツの節となるノーツ) 全て
        ///     type が 2 or 4 (連結) であり、かつ先頭の時のみ要素が入る
        ///     (節となるノーツの場合、要素は入らない。先頭のノーツだけがその後に続く全ての節ノーツの情報を保持する)
        /// </summary>
        public List<NoteData> notes;
    }
}