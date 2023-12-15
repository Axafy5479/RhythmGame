using InputSystemUtility;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game.Test
{
    public class NoteControllerTest : InputReceiver
    {
        [SerializeField] private int spawnIndex, noteId;
        [SerializeField] private NoteController noteController;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // 楽曲開始
            TimeCalculator.Instance.Play();

            // ノーツの初期位置の設定
            noteController.Trn.position = Field.Instance.GetSpawnPos(spawnIndex);

            // ノーツの振る舞い(Plan)の設定
            noteController.Activated(GameManager.Instance.SampleGameInfo.GetPlanMap()[noteId]);

            // 発射
            noteController.Launch();
        }

        protected override void OnFingerDown(Finger finger)
        {
            // 叩いた指をノーツに設定
            noteController.AddFinger(finger);
        }

        protected override void OnFingerMove(Finger finger)
        {
        }

        protected override void OnFingerUp(Finger finger)
        {
        }
    }
}