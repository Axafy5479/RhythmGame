using System;
using System.Collections.Generic;
using System.Linq;
using Chart;
using UnityEngine;

namespace Game.Plan.ConverterTest
{
    public class NotePlanConverterTest : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            var converter = new NotePlanConverter(ChartDataUtility.GetChartById(-1), Course.EASY);
            Test1(converter);
            Test2(converter);
        }

        private void Test1(NotePlanConverter converter)
        {
            var ok = true;
            var correctList = new List<int>
                { 247, 490, 1051, 1320, 1699, 2199, 2365, 2865, 3240, 3594, 3931, 4255, 4471, 4688 };
            var correctBPMList = new List<float>
                { 121, 252, 318, 117, 60, 90, 90, 120, 120, 65.5f, 138.92f, 138.92f, 138.73f, 138.73f };

            for (var i = 0; i < converter.AllNoteData.Length; i++)
            {
                var note = converter.AllNoteData[i];
                var testValue = converter.LPBtoMilliSec(note);
                var correctTime = correctList[i];
                var correctBPM = correctBPMList[i];

                if (Math.Abs(testValue.time - correctTime) > 3)
                {
                    Debug.LogError($"{i}番目の時間が誤っています¥n計算結果={testValue} 正しい値={correctTime}");
                    ok = false;
                }

                if (Math.Abs(testValue.bpm - correctBPM) > 0.00001f)
                {
                    Debug.LogError($"{i}番目の時間が誤っています¥n計算結果={testValue} 正しい値={correctTime}");
                    ok = false;
                }
            }

            if (ok)
            {
                Debug.Log("Test1 OK!");
            }
        }

        private void Test2(NotePlanConverter converter)
        {
            var correct =
                "type:Game.Plan.NormalNotePlan noteId:1 beatTime:247 launchTime:-1736 block:4\ntype:Game.Plan.LongHeadNotePlan noteId:2 beatTime:490 launchTime:-462 block:3 childId:[3,4]\ntype:Game.Plan.LongBackNotePlan noteId:3 beatTime:1051 launchTime:297 block:2 headId:2\ntype:Game.Plan.LongBackNotePlan noteId:4 beatTime:1320 launchTime:-731 block:0 headId:2\ntype:Game.Plan.NormalNotePlan noteId:5 beatTime:1051 launchTime:297 block:4\ntype:Game.Plan.NormalNotePlan noteId:6 beatTime:1320 launchTime:-731 block:4\ntype:Game.Plan.NormalNotePlan noteId:7 beatTime:1699 launchTime:-2301 block:4\ntype:Game.Plan.LongHeadNotePlan noteId:8 beatTime:2199 launchTime:-467 block:0 childId:[9,10,11]\ntype:Game.Plan.LongBackNotePlan noteId:9 beatTime:2532 launchTime:-134 block:3 headId:8\ntype:Game.Plan.LongBackNotePlan noteId:10 beatTime:2699 launchTime:33 block:1 headId:8\ntype:Game.Plan.LongBackNotePlan noteId:11 beatTime:3115 launchTime:1115 block:4 headId:8\ntype:Game.Plan.NormalNotePlan noteId:12 beatTime:2365 launchTime:-301 block:4\ntype:Game.Plan.NormalNotePlan noteId:13 beatTime:2865 launchTime:865 block:4\ntype:Game.Plan.NormalNotePlan noteId:14 beatTime:3240 launchTime:1240 block:4\ntype:Game.Plan.NormalNotePlan noteId:15 beatTime:3594 launchTime:-70 block:4\ntype:Game.Plan.NormalNotePlan noteId:16 beatTime:3931 launchTime:2204 block:4\ntype:Game.Plan.NormalNotePlan noteId:17 beatTime:4255 launchTime:2528 block:4\ntype:Game.Plan.NormalNotePlan noteId:18 beatTime:4471 launchTime:2742 block:4\ntype:Game.Plan.NormalNotePlan noteId:19 beatTime:4688 launchTime:2959 block:4";

            var dic = converter.DataToPlan().Values.ToList();
            dic.Sort((a, b) => a.NoteId - b.NoteId);
            if (string.Join("\n", dic) == correct)
            {
                Debug.Log("Test2 OK!");
            }
            else
            {
                Debug.LogError("Test2 NG");
            }
        }
    }
}