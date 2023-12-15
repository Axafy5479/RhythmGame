using System.Collections;
using System.Collections.Generic;
using Game.Plan;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    public class NoteController : MonoBehaviour
    {
        public class NoteControllerProperty
        {
            public JudgeEnum Judge { get; set; }
            public NotePlan Plan { get; private set; }
            public HashSet<Finger> Fingers { get; } = new();
            public NoteController_State State { get; private set; }
            public bool IsValid { get; private set; }

            public void Clear()
            {
                IsValid = false;
                Judge = JudgeEnum.None;
                Plan = null;
                Fingers.Clear();
            }

            public void Activated(NotePlan plan)
            {
                Plan = plan;
                IsValid = true;
            }

            public void ChangeState(NoteController controller,NoteController_State newState)
            {
                State?.OnExit(controller,newState);
                newState.OnEnter(controller,State);
                State = newState;
            }
        }
        
        

        private NoteController_Move MoveState { get; } = new();
        private NoteController_Slide MoveSlide { get; } = new();
        public NoteControllerProperty Properties { get; } = new();

        private Transform trn;
        public Transform Trn
        {
            get
            {
                if (trn == null) trn = this.transform;
                return trn;
            }
        }


    }


}
