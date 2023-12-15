using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Game
{
    public class LaunchTimeManager : MonoBehaviour, INeedInitializing
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] [TextArea(5, 200)] private string log;

        /// <summary>
        ///     射出すべきノーツを保持したキュー
        /// </summary>
        private Queue<(int time, int noteId)> Queue { get; set; }

        /// <summary>
        ///     ノーツを射出すべきタイミングを通知する
        /// </summary>
        public Subject<int> OnLaunchTime { get; } = new();

        /// <summary>
        ///     全ノーツを射出し切ったら通知する
        /// </summary>
        public Subject<Unit> OnAllNotesLaunched { get; } = new();

        private void Update()
        {
            // キューの中身がある時にループ
            while (Queue != null && Queue.Any())
            {
                // すぐに射出すべきノーツがあるか確認
                if (Queue.Peek().time > audioSource.time * 1000) return;

                // ある場合、それをキューから取り出す
                var launchingItem = Queue.Dequeue();

                // 通知する
                OnLaunchTime.OnNext(launchingItem.noteId);

                // ノーツがなくなったら、そのことを通知
                if (!Queue.Any()) OnAllNotesLaunched.OnNext(Unit.Default);

                // ログ出力
#if UNITY_EDITOR
                var realTime = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
                var audioTime = TimeSpan.FromSeconds(audioSource.time);
                log +=
                    $"id:{launchingItem.noteId}\t\taudioTime : {audioTime:mm':'ss'.'ff}\t\trealTime : {realTime:mm':'ss'.'ff}\n";
#endif
            }
        }

        /// <summary>
        ///     初期化
        /// </summary>
        /// <param name="info">ゲーム設定</param>
        public void Initialize(GameInfo info)
        {
            // 射出時間とnoteIdのペアを作りたい
            List<(int time, int noteId)> timesToNotify =
                info.GetPlanMap() // NoteId NotePlanの辞書
                    .Select(pair => pair.Value) // Valueだけ取り出す
                    .Where(plan => plan.ParentPlan == null) // 親を持たないノーツに限定
                    .Select(n => (n.LaunchTime, n.NoteId)) // time, id のペアに変換
                    .ToList(); // Listに変換

            // 射出時間順に並べ替え
            timesToNotify.Sort((a, b) => a.time - b.time);

            // キューの作成
            Queue = new Queue<(int time, int noteId)>(timesToNotify);
        }
    }
}