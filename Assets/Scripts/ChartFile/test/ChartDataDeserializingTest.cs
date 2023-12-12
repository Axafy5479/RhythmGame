using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChartFile.Test
{
    /// <summary>
    /// 作成したモデルクラスが正しく動作していることを確認するためのクラス
    /// </summary>
    public class ChartDataDeserializingTest : MonoBehaviour
    {
        void Start()
        {
            // jsonファイルを読み込み
            var chartText = Resources.Load<TextAsset>("Songs/chart_sample/chart_sample");
            
            // jsonデータをデシリアライズ
            var chart = LitJson.JsonMapper.ToObject<ChartData>(chartText.text);
            
            // 再度jsonデータにシリアライズ
            var json = LitJson.JsonMapper.ToJson(chart);
            
            // ログ出力
            Debug.Log(json);
        }

    }
}