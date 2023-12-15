using System.Collections;
using System.Collections.Generic;
using Chart;
using Game.Plan;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Test
{
    public class NoteControllerTest : MonoBehaviour
    {
        [SerializeField] private int chartId, spawnIndex,blockSize;
        [SerializeField] private NoteController noteController;
        [SerializeField] private AudioSource audioSource;
        // Start is called before the first frame update
        void Start()
        {
            var chart = ChartDataUtility.GetChartById(chartId);
            var notePlans = new NotePlanConverter(chart, Course.EASY).DataToPlan();

            audioSource.clip = chart.Clip;
            Field.Instance.Initialize(blockSize);
            
            noteController.Trn.position = Field.Instance.GetSpawnPos(spawnIndex);
            noteController.Activated(notePlans[1]);
            noteController.Launch();
            
            TimeCalculator.Instance.Play();
        }

    }
}