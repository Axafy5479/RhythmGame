using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chart
{
    /// <summary>
    /// 譜面データを取得するためのUtilityクラス
    /// </summary>
    public static class ChartDataUtility
    {
        /// <summary>
        /// IDを指定して譜面データを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ChartDTO GetChartById(int id)
        {
            return GetAllChartData().Find(chart => chart.SongId == id);
        }

        /// <summary>
        /// 全ての譜面データを取得する
        /// </summary>
        /// <returns></returns>
        public static List<ChartDTO> GetAllChartData()
        {
            return Resources.LoadAll<ChartDTO>("Songs/Models").ToList();
        }
    }
}