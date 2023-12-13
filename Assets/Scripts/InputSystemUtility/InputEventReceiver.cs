using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystemUtility
{
    public class InputEventReceiver : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            private void Start()
            {
                Application.targetFrameRate = 30;
                EnhancedTouchManager.Instance.AddReceiver(this);
            }

            public void Dispose()
            {
                EnhancedTouchManager.Instance.EndReceiving(this);
            }

            public void OnPointerDown(Touch touch)
            {
                //  Debug.Log($"Down : {touch.time}");
                Debug.Log($"Down");
            }

            public void OnPointerMove(Touch touch)
            {
                // Debug.Log($"Move : {touch.time}");
            }

            public void OnPointerUp(Touch touch)
            {
                // Debug.Log($"Up : {touch.time}");
                //Debug.Log($"Up");
            }
        }
    }
}
