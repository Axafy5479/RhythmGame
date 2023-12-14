using System;
using UniRx;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace InputSystemUtility
{
    public class EnhancedTouchManager : MonoSingleton<EnhancedTouchManager>
    {

        /********************* 通知用プロパティ ***********************/
        /// <summary>
        ///     指が触れたことを通知
        /// </summary>
        public IObservable<Finger> OnFingerDown => onFingerDown;

        private Subject<Finger> onFingerDown { get; } = new();

        /// <summary>
        ///     指が動かされたことを通知
        /// </summary>
        public IObservable<Finger> OnFingerMove => onFingerMove;

        private Subject<Finger> onFingerMove { get; } = new();

        /// <summary>
        ///     指が離されたことを通知
        /// </summary>
        public IObservable<Finger> OnFingerUp => onFingerUp;
        
        private Subject<Finger> onFingerUp = new();
        /*************************************************************/


        /// <summary>
        ///     EnhancedTouchを有効にし、
        ///     Touchイベントに通知を発行するアクションを登録
        /// </summary>
        private void Start()
        {
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += onDownAction;
            Touch.onFingerMove += onMoveAction;
            Touch.onFingerUp += onUpAction;
        }


        /******** EnhancedTouchのTouchイベントに追加するデリゲート *********/
        private void onDownAction(Finger f)
        {
            onFingerDown.OnNext(f);
        }

        private void onMoveAction(Finger f)
        {
            onFingerMove.OnNext(f);
        }

        private void onUpAction(Finger f)
        {
            onFingerUp.OnNext(f);
        }
        /*************************************************************/


        /// <summary>
        ///     EnhancedTouchを無効にし、
        ///     Touchイベントに通知を発行するアクションを除去
        /// </summary>
        protected override void BeforeOnDestroy()
        {
            EnhancedTouch.onFingerDown -= onDownAction;
            EnhancedTouch.onFingerMove -= onMoveAction;
            EnhancedTouch.onFingerUp -= onUpAction;
            EnhancedTouchSupport.Disable();
        }
    }
}