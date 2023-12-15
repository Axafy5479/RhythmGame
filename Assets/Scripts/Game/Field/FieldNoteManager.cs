using System.Collections.Generic;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    public class FieldNoteManager : MonoSingleton<FieldNoteManager>
    {
        public HashSet<NoteController> ControllersOnField { get; } = new();


        private void Update()
        {
            RemoveOverlookedNote();
        }

        /// <summary>
        ///     ノーツがMissの時間外かつ画面外に出た時、そのノーツを削除する
        /// </summary>
        private void RemoveOverlookedNote()
        {
            var time = TimeCalculator.Instance.GetTime();

            ControllersOnField.RemoveWhere(noteController =>
            {
                // 本来叩くべき時間からのズレを計算 (このプロジェクトでは、遅い場合に負の数になるよう統一している)
                var delta = noteController.Properties.Plan.BeatTime - time;

                // deltaは遅いときに負なので、マイナスをつけて正にする。
                // Missの限界値より小さい時は削除しない
                if (Settings.Instance.JudgeTime(JudgeEnum.Miss) > -delta) return false;

                // 画面内の場合は削除しない
                if (noteController.NoteSprite.isVisible) return false;

                // 画面外かつMissの限界も超えたため削除対象
                // 判定
                noteController.Overlooked();

                return true;
            });
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