using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace InputSystemUtility.InputSystemTest
{
    public class InputSystemUtilityTest : InputReceiver
    {
        protected override void Start()
        {
            Debug.Log("強制的に 1 fpsに変更しても、素早いクリックの時刻が通常の精度で取得できていることを確認する");
            Application.targetFrameRate = 1;
            base.Start();
        }

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