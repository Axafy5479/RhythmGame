using LitJson;
using UnityEngine;

namespace Chart.Test
{
    /// <summary>
    ///     作成したモデルクラスが正しく動作していることを確認するためのクラス
    /// </summary>
    public class ChartDataDeserializingTest : MonoBehaviour
    {
        private void Start()
        {
            LoadAllTest();
            LoadByIdTest();
        }

        private void LoadAllTest()
        {
            Debug.Log("-----------LoadAllTest-----------");

            var charts = ChartDataUtility.GetAllChartData();
            foreach (var chart in charts)
            {
                Debug.Log($"id={chart.SongId} の楽曲名は {chart.name} です");
            }
            
            Debug.Log("----------------------------------");
        }
        
        
        private void LoadByIdTest()
        {
            Debug.Log("-----------LoadByIdTest-----------");

            var chart = ChartDataUtility.GetChartById(0).GetChartData();
            Debug.Log(JsonMapper.ToJson(chart));
            
            Debug.Log("----------------------------------");
        }
    }
}