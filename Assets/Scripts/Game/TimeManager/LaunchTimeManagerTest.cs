using System.Linq;
using Chart;
using Game.Plan;
using UniRx;
using UnityEngine;

namespace Game.Test
{
    public class LaunchTimeManagerTest : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private LaunchTimeManager timeManager;

        /// <summary>
        ///     再生する楽曲Id
        /// </summary>
        [SerializeField] private int songId;

        // Start is called before the first frame update
        private void Start()
        {
            // 指定した楽譜を取得
            var chart = ChartDataUtility.GetChartById(songId);

            // Easy譜面をPlanに変換
            var plan = new NotePlanConverter(chart, Course.EASY).DataToPlan();

            // timeManagerの初期化、購読
            timeManager.OnLaunchTime.Subscribe(OnLaunch).AddTo(this);
            timeManager.OnAllNotesLaunched.Subscribe(_ => Debug.Log("全ノーツを射出し終えました")).AddTo(this);

            // AudioClipを設定、再生
            audioSource.clip = chart.Clip;
            audioSource.Play();
        }

        private void OnLaunch(int id)
        {
            Debug.Log($"Launch! noteId={id}");
        }
    }
}