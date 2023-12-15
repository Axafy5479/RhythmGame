using UniRx;
using UniRx.Toolkit;
using UnityEngine;

namespace Game
{
    public class NotePool : ObjectPool<NoteController>
    {
        public NotePool(int number)
        {
            PreloadAsync(number, 10).Subscribe();
            NoteParent = new GameObject("_notepool").transform;
        }

        private Transform NoteParent { get; }

        /// <summary>
        ///     インスタンスの生成
        /// </summary>
        /// <returns></returns>
        protected override NoteController CreateInstance()
        {
            var prefab = Resources.Load<NoteController>("Game/note");
            var controller = Object.Instantiate(prefab, NoteParent);

            // 自身をプールに返すメソッドを設定
            controller.ReturnToPool = () => Return(controller);
            return controller;
        }
    }
}