using System.Collections.Generic;
using Game.Plan;
using UniRx;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// LaunchTimeManagerの通知を受けて、ノーツを生成するクラス
    /// </summary>
    [RequireComponent(typeof(LaunchTimeManager))]
    public class NoteGenerator : MonoBehaviour, INeedInitializing
    {
        /// <summary>
        /// NoteControllerのオブジェクトプール
        /// </summary>
        private NotePool noteControllerPool;
        
        /// <summary>
        /// idとNotePlanの辞書
        /// (LaunchTimeManagerからidが通知されるため、これに紐づくPlanを用いてノーツを初期化する)
        /// </summary>
        private Dictionary<int, NotePlan> NotePlanMap { get; set; }

        private void Awake()
        {
            // プールを作成
            noteControllerPool = new NotePool(50);
        }

        public void Initialize(GameInfo gameInfo)
        {
            // id_Plan の辞書を作成
            NotePlanMap = gameInfo.GetPlanMap();
            
            // LaunchTimeManagerの通知を購読
            var launchTime = GetComponent<LaunchTimeManager>();
            launchTime.OnLaunchTime.Subscribe(noteId=>Launch(NotePlanMap[noteId]));
        }

        /// <summary>
        /// LaunchTimeManagerの通知を受けて実行するメソッド
        /// </summary>
        /// <param name="plan"></param>
        private void Launch(NotePlan plan)
        {
            // プールからNoteControllerを取り出す
            var controller = noteControllerPool.Rent();
                
            // NoteControllerにPlanを設定
            controller.Activated(plan);
                
            // Spawn位置に設定
            controller.Trn.position = Field.Instance.GetSpawnPos(plan.Block);

            // 発射
            controller.Launch();
            
#if UNITY_EDITOR
            // デバッグしやすいように、オブジェクトの名前としてPlanを設定
            controller.name = $"{plan}";
#endif
            
            // 子ノーツがあれば、それも射出する
            if(plan.ChildNote is { } childPlan) Launch(childPlan);
        }
    }
}