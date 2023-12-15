using System;
using System.Collections.Generic;
using System.Linq;
using Game.Plan;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    public class NoteController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer noteSprite;
        [SerializeField] private LongBand longBand;

        private Transform trn;

        /// <summary>
        ///     NoteControllerの画像コンポーネント
        /// </summary>
        public SpriteRenderer NoteSprite => noteSprite;

        /// <summary>
        ///     Longノーツの節同士を結ぶ帯
        /// </summary>
        public LongBand LongBand => longBand;

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

        /// <summary>
        ///     このノーツをPoolに返却する
        /// </summary>
        public Action ReturnToPool { get; set; }

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
        ///     このノーツに子ノーツを設定する
        /// </summary>
        /// <param name="backNote"></param>
        public void SetChildNote(NoteController backNote)
        {
            Properties.ChildNote = backNote;
            longBand.SetChildNote(backNote);
        }

        private void TakeOverFingers(HashSet<Finger> fingers)
        {
            if (fingers.Any())
            {
                foreach (var finger in fingers) AddFinger(finger);
            }
            else
            {
                Properties.SetJudge(null);
                ReturnNote(true);
            }
        }

        /// <summary>
        ///     ノーツを流す、MoveStateにする
        /// </summary>
        public void Launch()
        {
            // Activateされていることを確認
            if (!Properties.IsValid.Value)
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

            // 今の状態がMoveStateではない場合、状態遷移はしない
            if (Properties.State is not NoteController_Move) return;
            
            Properties.SetJudge(finger);

            // 子ノーツの有無で場合わけ
            if (Properties.Plan.ChildPlan != null)
            {
                // 存在している時はスライド状態
                Properties.ChangeState(this, MoveSlide);
            }
            else
            {
                ReturnNote(false);
            }
        }

        public void ForceMiss()
        {
            Properties.SetJudge(null);
            ReturnNote(false);
        }

        /// <summary>
        ///     判定が決まった際に呼ばれるメソッド
        ///     判定を記録し、ノーツをプールに返す
        /// </summary>
        /// <param name="byChain">ロングノーツでミスをすると、1個後ろまで連鎖的にミスとなる。この「連鎖」で判定されたか否か</param>
        public void ReturnNote(bool byChain)
        {
            // 個ノーツがあり、チェーンによる判定でない場合、fingersを引き継ぐ
            if (Properties.ChildNote is { } child && !byChain) child.TakeOverFingers(Properties.Fingers);

            // プロパティをリセット
            Properties.Clear(this);

            // poolに返す
            ReturnToPool();
        }
    }
}