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
                "noteId:   1\tbeatTime:   247\tlaunchTime: -1736\tblock:4\tchildNote: \tbpm:121\nnoteId:   2\tbeatTime:   490\tlaunchTime:  -462\tblock:3\tchildNote:3\tbpm:252\nnoteId:   3\tbeatTime:  1051\tlaunchTime:   297\tblock:2\tchildNote:4\tbpm:318\nnoteId:   4\tbeatTime:  1320\tlaunchTime:  -731\tblock:0\tchildNote: \tbpm:117\nnoteId:   5\tbeatTime:  1051\tlaunchTime:   297\tblock:4\tchildNote: \tbpm:318\nnoteId:   6\tbeatTime:  1320\tlaunchTime:  -731\tblock:4\tchildNote: \tbpm:117\nnoteId:   7\tbeatTime:  1699\tlaunchTime: -2301\tblock:4\tchildNote: \tbpm:60\nnoteId:   8\tbeatTime:  2199\tlaunchTime:  -467\tblock:0\tchildNote:9\tbpm:90\nnoteId:   9\tbeatTime:  2532\tlaunchTime:  -134\tblock:3\tchildNote:10\tbpm:90\nnoteId:  10\tbeatTime:  2699\tlaunchTime:    33\tblock:1\tchildNote:11\tbpm:90\nnoteId:  11\tbeatTime:  3115\tlaunchTime:  1115\tblock:4\tchildNote: \tbpm:120\nnoteId:  12\tbeatTime:  2365\tlaunchTime:  -301\tblock:4\tchildNote: \tbpm:90\nnoteId:  13\tbeatTime:  2865\tlaunchTime:   865\tblock:4\tchildNote: \tbpm:120\nnoteId:  14\tbeatTime:  3240\tlaunchTime:  1240\tblock:4\tchildNote: \tbpm:120\nnoteId:  15\tbeatTime:  3594\tlaunchTime:   -70\tblock:4\tchildNote: \tbpm:65.5\nnoteId:  16\tbeatTime:  3931\tlaunchTime:  2204\tblock:4\tchildNote: \tbpm:138.92\nnoteId:  17\tbeatTime:  4255\tlaunchTime:  2528\tblock:4\tchildNote: \tbpm:138.92\nnoteId:  18\tbeatTime:  4471\tlaunchTime:  2742\tblock:4\tchildNote: \tbpm:138.73\nnoteId:  19\tbeatTime:  4688\tlaunchTime:  2959\tblock:4\tchildNote: \tbpm:138.73";

            var dic = converter.DataToPlan().Values.ToList();
            dic.Sort((a, b) => a.NoteId - b.NoteId);
            if (string.Join("\n", dic) == correct)
            {
                Debug.Log("Test2 OK!");
            }
            else
            {
                Debug.LogError("Test2 NG");
                Debug.LogError(string.Join("\n", dic));
            }
        }
    }
}