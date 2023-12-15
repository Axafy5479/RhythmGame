using System;
using System.Collections;
using System.Collections.Generic;
using Game.Plan;
using UnityEngine;
using UniRx;
using UniRx.Toolkit;

namespace Game
{
    [RequireComponent(typeof(LaunchTimeManager))]
    public class NoteGenerator : MonoBehaviour,INeedInitializing
    {
        private Dictionary<int,NotePlan> NotePlanMap { get; set; }
        private NotePool noteControllerPool;

        private void Awake()
        {
            noteControllerPool = new(50);
        }

        public void Initialize(GameInfo gameInfo)
        {
            NotePlanMap = gameInfo.GetPlanMap();
            var launchTime = this.GetComponent<LaunchTimeManager>();
            launchTime.OnLaunchTime.Subscribe(Launch);
        }

        private void Launch(int noteId)
        {
            var plan = NotePlanMap[noteId];
            var controller = noteControllerPool.Rent();
            controller.Activated(plan);
            controller.Trn.position = Field.Instance.GetSpawnPos(plan.Block);
            controller.Launch();
            
#if UNITY_EDITOR
            controller.name = $"{plan}";
#endif
        }


    }
}