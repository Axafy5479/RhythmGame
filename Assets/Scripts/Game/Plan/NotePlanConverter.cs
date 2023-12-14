using System.Collections.Generic;
using System.Linq;
using Chart;

namespace Game.Plan
{
    public class NotePlanConverter
    {
        public NotePlanConverter(ChartDTO chartData, Course course)
        {
            var chart = chartData.GetChartData();
            AllNoteData = chart.notes[(int)course];
            Bpms.Add(new BPMData { bpm = chart.bpm, num = 0, lpb = -1 });
            Bpms.AddRange(chartData.GetChartData().bpms);
        }

        public IdGenerator IdGenerator { get; } = new();
        public NoteData[] AllNoteData { get; }
        private List<BPMData> Bpms { get; } = new();


        /// <summary>
        ///     譜面データ.json の情報はlpbやnumを元にタイミングが記述されているため、
        ///     時刻ベースの情報に変換する
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, NotePlanBase> DataToPlan()
        {
            // 変換後のインスタンスを保持する
            // noteId, Plan  のマップ
            var noteMap = new Dictionary<int, NotePlanBase>();

            // 譜面上の全てのノーツ情報ででループを回し、全て変換
            foreach (var note in AllNoteData)
            {
                if (!note.notes.Any())
                {
                    // 子ノーツがない = 通常のーつ

                    // ノーツのタイミングを milli sec に変換
                    var (time, bpm) = LPBtoMilliSec(note);

                    // Planを生成
                    var plan = new NormalNotePlan(IdGenerator.GetId(), time, bpm, note.block);

                    // mapに挿入
                    noteMap.Add(plan.NoteId, plan);
                }
                else
                {
                    // ロングノーツ

                    // このロングノーツの中に存在する判定ポイント(節)の数をカウント
                    var noteNumber = note.notes.Count + 1; // トップ + 子ノーツ

                    // ノーツの数だけidを発行
                    var ids = IdGenerator.GetIds(noteNumber);

                    /***************** ロングtopノーツに関して変換 ***************/
                    var (time, bpm) = LPBtoMilliSec(note);
                    var headPlan = new LongHeadNotePlan(ids[0], ids[1..], time, note.block, bpm);
                    noteMap.Add(headPlan.NoteId, headPlan);

                    /************** ロング子ノーツに関して変換 *******************/
                    for (var i = 0; i < note.notes.Count; i++)
                    {
                        var backNote = note.notes[i];
                        var (childTime, childBpm) = LPBtoMilliSec(backNote);
                        var backPlan = new LongBackNotePlan(ids[i + 1], ids[0], childTime, backNote.block, childBpm);
                        noteMap.Add(backPlan.NoteId, backPlan);
                    }
                    /*********************************************************/
                }
            }

            return noteMap;
        }

        public (int time, float bpm) LPBtoMilliSec(NoteData noteData)
        {
            /***  知りたい時刻  ***/
            var lpb = noteData.lpb;
            var num = noteData.num;
            /********************/

            // 再帰計算
            var (ans, bpm) = _LPBtoSec(0);

            // milli sec に変換
            return ((int)(ans * 1000), bpm);


            // inner recursive method
            //
            // Bpmsを元に、再帰計算
            // i-1番目のbpm領域の終了時間を元に、i番目のbpm領域の終了時間を計算する
            // 計算式は次の通り (i-1番目を 1   i 番目を 2 と表現する)
            //
            //    t2 = t1 + (n2 - n1 * (l2 / l1) * Δt )
            //
            //    Δt = 60 / ( b1 * l2 )
            //
            //    (    b ... bpm   t ... 時間    n ... num    l ... lpb   Δt ... lineを1つ進むのにかかる時間  )
            //
            //
            //  次の1 or 2が満たされた時、再帰終了
            //  1 ... indexがBpmsの外にでた
            //  2 ... t_2が求めたい時刻以降となった 
            (float, float) _LPBtoSec(float t1, int i = 1)
            {
                /******* i-1番目のbpm領域の情報 ********/
                var n1 = Bpms[i - 1].num;
                float l1 = Bpms[i - 1].lpb;
                var b1 = Bpms[i - 1].bpm;
                /***********************************/


                /****** i 番目のbpm領域の情報 ********/


                // i 番目の情報は終了条件を満たしたか否かで変化する
                //
                // 終了条件は、
                // 1. i番目がBpmsの範囲外となった
                //
                //    ||   BPM1    ||   BPM2   ||   BPM3   ||   BPM4 = i-1番目        (iは範囲外)
                //  
                // 最後に到達するまで、目標位置が見つからなかった = 目標位置は最後のBPM変化以降(BPM4の領域)にある。
                // t1 (i-1番目の時刻) を元に目標の時刻を導く
                //
                //
                // 2. 求めたい位置 < i番目の開始時刻
                //
                //    ||   BPM1    ||   BPM2 = i-1番目      ⭐︎求めたい位置⭐︎   ||   BPM3 = i番目     ||   BPM4      
                //
                //  t1 を元に目標の時刻を導く
                // 
                //  上記1,2以外の場合
                //
                //   ||   BPM1    ||   BPM2 = i-1番目   ||   BPM3 = i番目     ||   BPM4  ⭐︎求めたい位置⭐︎
                //
                //   
                // t1を元にt2 (BPM3) の開始時刻を計算する

                int n2;
                float l2;

                // 終了条件1の確認
                var isEnd1 = i == Bpms.Count;
                var isEnd2 = false;
                if (isEnd1)
                {
                    // indexがBpmsの範囲外となった、
                    // つまり求めたい時刻は、最後のBPM変化以降となった

                    // n2やl2を、求めたいタイミングに設定
                    n2 = num;
                    l2 = lpb;
                }
                else
                {
                    // 終了条件2の確認

                    // 求めたい時間が、次のBPM変化の時間より手前の場合は終了 (時間は不明なので、beat数で時間関係を比較している)
                    // (int同士の割り算を避けるため、分母をはらっている)
                    isEnd2 = Bpms[i].num * lpb > num * Bpms[i].lpb;

                    if (isEnd2)
                    {
                        // 求めたい時刻は 次のBPM変化より前となった

                        // n2やl2を、求めたいタイミングに設定
                        n2 = num;
                        l2 = lpb;
                    }
                    else
                    {
                        // まだ終了時刻を満たしていないので、次のBPM領域の開始事故おくを計算する
                        // n2, l2は次のBPM変化に設定
                        n2 = Bpms[i].num;
                        l2 = Bpms[i].lpb;
                    }
                }

                /***********************************/


                // inner methodのコメントに示した計算式を適用
                var deltaT = 60f / b1 / l2;
                var t2 = t1 + (n2 - n1 * (l2 / l1)) * deltaT;

                // 終了条件を満たしている場合はt2が答え
                if (isEnd1 || isEnd2) return (t2, b1);

                // まだ目標に辿り着けていない場合は再帰計算を続ける
                return _LPBtoSec(t2, i + 1);
            }
        }
    }
}