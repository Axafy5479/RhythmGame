using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace ChartFile
{
    /// <summary>
    /// 譜面ファイル.jsonの情報を利用ためのDTOクラス
    /// 必ず Assets/Resources/Songs/chart_sample の中に専用フォルダを作成して配置する
    /// </summary>
    [CreateAssetMenu(menuName = "SongData")]
    public class ChartDTO : ScriptableObject
    {
        [SerializeField] private int songId;
        [SerializeField] private TextAsset chartFile;
        [SerializeField] private AudioClip audioClip;

        /// <summary>
        /// 譜面ファイル.jsonをデシリアライズしたインスタンスのキャッシュ
        /// </summary>
        private ChartData ChartDataCache { get; set; }

        /// <summary>
        /// 楽曲に付与されるId
        /// </summary>
        public int SongId => songId;
        
        /// <summary>
        /// この譜面の楽曲
        /// </summary>
        public AudioClip Clip => audioClip;
        
        /// <summary>
        /// 譜面データを取得する
        /// (jsonデータをデシリアライズし、キャッシュを残しつつreturnする)
        /// </summary>
        /// <returns></returns>
        public ChartData GetChartData()
        {
            return ChartDataCache ??= JsonMapper.ToObject<ChartData>(chartFile.text);
        }
    }
}