using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace InputSystemUtility.InputSystemTest
{
    public class InputSystemUtilityTest : InputReceiver
    {
        protected override void OnFingerDown(Finger finger)
        {
            Debug.Log($"Down : pos={finger.lastTouch.screenPosition} time={finger.lastTouch.time}");
        }

        protected override void OnFingerMove(Finger finger)
        {
            Debug.Log($"Move : pos={finger.lastTouch.screenPosition} time={finger.lastTouch.time}");
        }

        protected override void OnFingerUp(Finger finger)
        {
            Debug.Log($"Up : pos={finger.lastTouch.screenPosition} time={finger.lastTouch.time}");
        }
    }
}