using System.Collections.Generic;
using UniRx;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    public class FieldNoteManager : MonoSingleton<FieldNoteManager>
    {
        /// <summary>
        /// フィールド上に存在するノーツ
        /// </summary>
        private HashSet<NoteController> ControllersOnField { get; } = new();

        private void Update()
        {
            RemoveOverlookedNote();
        }

        /// <summary>
        ///フィールド上に存在するノーツとして追加
        /// 
        ///     noteControllerの寿命を監視
        ///     寿命が切れたらControllersOnFieldから削除
        /// </summary>
        /// <param name="noteController"></param>
        public void AddNote(NoteController noteController)
        {
            noteController.Properties.IsValid
                .Where(b => !b) // 寿命が切れた通知のみ監視
                .First() // 一度だけでOK
                .Subscribe(_ => ControllersOnField.Remove(noteController))
                .AddTo(this);

            // コレクションに追加
            ControllersOnField.Add(noteController);
        }

        /// <summary>
        ///     ノーツがMissの時間外かつ画面外に出た時、そのノーツを削除する
        /// </summary>
        private void RemoveOverlookedNote()
        {
            var time = TimeCalculator.Instance.GetTime();

            foreach (var noteController in ControllersOnField)
            {

            }
        }

        /// <summary>
        ///     プレイヤーInputがあった時、位置と時間的に最も適切なノーツを見つけ、Fingerを渡す
        /// </summary>
        /// <param name="finger"></param>
        public void SelectNoteForInput(Finger finger)
        {
            // タッチの位置からWorldPosに変換
            var (beatPos, ok) = Field.Instance.ScreenToFieldPosition(finger.lastTouch.screenPosition);

            // タッチ位置がField上でない場合は何もしない
            if (!ok) return;

            // 判定すべきNoteControllerを探す
            var target = TargetNoteFinder.Find(ControllersOnField, beatPos.x, (int)(finger.lastTouch.time * 1000));

            // 見つかればFingerを渡す
            if (target != null) target.AddFinger(finger);
        }
    }
}