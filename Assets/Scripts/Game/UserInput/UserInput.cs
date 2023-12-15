using System.Collections;
using System.Collections.Generic;
using InputSystemUtility;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    public class UserInput : InputReceiver
    {
        protected override void OnFingerDown(Finger finger)
        {
            FieldNoteManager.Instance.SelectNoteForInput(finger);
        }

        protected override void OnFingerMove(Finger finger)
        {
        }

        protected override void OnFingerUp(Finger finger)
        {
        }
    }
}
