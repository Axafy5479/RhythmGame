using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace InputSystemUtility
{
    public class EnhancedTouchManager : MonoBehaviour
    {
        #region MonoSingleton

        private static EnhancedTouchManager instance;

        public static EnhancedTouchManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var instances = FindObjectsOfType<EnhancedTouchManager>();
                    instance = instances.Length switch
                    {
                        0 => instance = new GameObject().AddComponent<EnhancedTouchManager>(),
                        1 => instances[0],
                        _ => throw new Exception("シーン上にTouchInputHandlerが複数存在します")
                    };

                }

                return instance;
            }
        }

        #endregion

        private HashSet<IPointerEventReceiver> EventReceivers { get; } = new();

        private void Start()
        {
            // EnhancedTouchの有効化
            EnhancedTouchSupport.Enable();

            EnhancedTouch.onFingerDown += OnFingerDown;
            EnhancedTouch.onFingerMove += OnFingerMove;
            EnhancedTouch.onFingerUp += OnFingerUp;
        }

        private void OnFingerDown(Finger finger)
        {
            foreach (var receiver in EventReceivers)
            {
                receiver.OnPointerDown(finger.lastTouch);
            }
        }
        
        private void OnFingerMove(Finger finger)
        {
            foreach (var receiver in EventReceivers)
            {
                receiver.OnPointerMove(finger.lastTouch);
            }
        }
        
        private void OnFingerUp(Finger finger)
        {
            foreach (var receiver in EventReceivers)
            {
                receiver.OnPointerUp(finger.lastTouch);
            }
        }

        public bool EndReceiving(IPointerEventReceiver receiver)
        {
            return EventReceivers.Remove(receiver);
        }

        public void AddReceiver(IPointerEventReceiver receiver)
        {
            EventReceivers.Add(receiver);
        }

        private void OnDestroy()
        {
            EnhancedTouch.onFingerDown -= OnFingerDown;
            EnhancedTouch.onFingerMove -= OnFingerMove;
            EnhancedTouch.onFingerUp -= OnFingerUp;
            EnhancedTouchSupport.Disable();
            
        }
    }
}