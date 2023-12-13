using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace InputSystemUtility
{
    /// <summary>
    ///     このクラスを継承したオブジェクトにユーザーのインプットがあった時
    ///     コールバックメソッドが呼ばれる
    /// </summary>
    public abstract class InputReceiver : MonoBehaviour
    {
        private void Start()
        {
            // インプットイベントを受け取る
            EnhancedTouchManager.Instance.OnFingerDown.Subscribe(OnFingerDown).AddTo(this);
            EnhancedTouchManager.Instance.OnFingerMove.Subscribe(OnFingerMove).AddTo(this);
            EnhancedTouchManager.Instance.OnFingerUp.Subscribe(OnFingerUp).AddTo(this);
        }

        /// <summary>
        ///     指が触れた時に呼ばれるコールバックメソッド
        /// </summary>
        /// <param name="finger"></param>
        protected abstract void OnFingerDown(Finger finger);

        /// <summary>
        ///     指が動いた時に呼ばれるコールバックメソッド
        /// </summary>
        /// <param name="finger"></param>
        protected abstract void OnFingerMove(Finger finger);

        /// <summary>
        ///     指が離された時に呼ばれるコールバックメソッド
        /// </summary>
        /// <param name="finger"></param>
        protected abstract void OnFingerUp(Finger finger);
    }
}