using Game.Plan;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    public class NoteController : MonoBehaviour
    {
        private Transform trn;

        /// <summary>
        ///     NoteControllerのプロパティを集めたクラス
        ///     Poolに返された時、全てのプロパティをリセットする
        /// </summary>
        public NoteControllerProperty Properties { get; } = new();


        /// <summary>
        ///     ノーツが流れている状態
        ///     (z移動のアニメーションを行う)
        /// </summary>
        private NoteController_Move MoveState { get; } = new();

        /// <summary>
        ///     ノーツがスライドしている状態
        ///     (x移動のアニメーションをこなう)
        /// </summary>
        private NoteController_Slide MoveSlide { get; } = new();


        /// <summary>
        ///     ノーツのTransformを取得するプロパティ
        ///     (transformプロパティを用いるのを避ける)
        /// </summary>
        public Transform Trn
        {
            get
            {
                if (trn == null) trn = transform;
                return trn;
            }
        }

        private void Update()
        {
            // 状態のOnUpdateを呼ぶ
            Properties.State?.OnUpdate(this);
        }


        /// <summary>
        ///     NoteControllerにPlanを登録し、操作可能な状態にする
        /// </summary>
        /// <param name="plan"></param>
        public void Activated(NotePlan plan)
        {
            Properties.Activated(plan);
        }

        /// <summary>
        ///     ノーツを流す、MoveStateにする
        /// </summary>
        public void Launch()
        {
            // Activateされていることを確認
            if (!Properties.IsValid)
            {
                Debug.LogError($"このノーツコントローラーはActivateされていません。\n先に{nameof(Activated)}を呼んでください");
                return;
            }

            // 状態を変更
            Properties.ChangeState(this, MoveState);
        }

        /// <summary>
        ///     ノーツが触れられた際に呼ばれるメソッド
        ///     判定を行う
        ///     子ノーツが存在している時は、子ノーツの位置まで移動
        /// </summary>
        /// <param name="finger"></param>
        public void AddFinger(Finger finger)
        {
            // 指の追加
            Properties.Fingers.Add(finger);

            // 子ノーツの有無で場合わけ
            if (Properties.Plan.ChildNote != null)
            {
                // 存在している時はスライド状態
                Properties.ChangeState(this, MoveSlide);
            }
            else
            {
                Debug.Log("ForDebugging");
                OnReturn();
                Debug.Log("デバッグのため、OnReturn");
            }
        }

        /// <summary>
        ///     スライドが終了した際に呼ばれる
        /// </summary>
        /// <param name="ok">スライドノーツを適切にHoldし、終了した</param>
        public void OnSlideEnd()
        {
            Debug.Log("スライド終了");
            Debug.Log("デバッグのため、OnReturn");
            OnReturn();
        }

        /// <summary>
        ///     Poolに返すメソッド
        /// </summary>
        public void OnReturn()
        {
            Properties.Clear(this);
        }
    }
}