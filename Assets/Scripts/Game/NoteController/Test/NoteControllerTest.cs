using Chart;
using Game.Plan;
using InputSystemUtility;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game.Test
{
    public class NoteControllerTest : InputReceiver
    {
        [SerializeField] private int chartId, spawnIndex, blockSize, noteId;
        [SerializeField] private NoteController noteController;

        [SerializeField] private AudioSource audioSource;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // 譜面の取得
            var chart = ChartDataUtility.GetChartById(chartId);
            
            // Data -> Plan 変換
            var notePlans = new NotePlanConverter(chart, Course.EASY).DataToPlan();

            // 楽曲の設定
            audioSource.clip = chart.Clip;
            
            // 楽曲開始
            TimeCalculator.Instance.Play();
            
            // フィールドの設定
            Field.Instance.Initialize(blockSize);

            // ノーツの初期位置の設定
            noteController.Trn.position = Field.Instance.GetSpawnPos(spawnIndex);
            
            // ノーツの振る舞い(Plan)の設定
            noteController.Activated(notePlans[noteId]);
            
            // 発射
            noteController.Launch();
        }

        protected override void OnFingerDown(Finger finger)
        {
            // 叩いた指をノーツに設定
            noteController.AddFinger(finger);
        }

        protected override void OnFingerMove(Finger finger)
        {
        }

        protected override void OnFingerUp(Finger finger)
        {
        }
    }
}