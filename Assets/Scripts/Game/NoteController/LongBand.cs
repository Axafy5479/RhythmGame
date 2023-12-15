using UnityEngine;

namespace Game
{
    /// <summary>
    ///     ロングノーツ同士を結ぶ帯
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class LongBand : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [SerializeField] private SpriteRenderer sprite;
        private Transform back;

        /// <summary>
        ///     シェーダーのプロパティにアクセスするId
        /// </summary>
        private int shearId;


        /// <summary>
        ///     長さを変更する画像画像コンポーネント
        /// </summary>
        private Transform trn;

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            trn = transform;
            shearId = Shader.PropertyToID("_ShearXY");
        }

        private void Update()
        {
            // 個ノーツがない場合は何もしない
            if (back == null) return;

            // 親子のノーツ位置
            // これらを結ぶように伸縮させる
            var headPos = head.position;
            var backPos = back.position;

            // 位置は両者の中央に配置
            trn.position = (headPos + backPos) / 2;

            // 両者の位置の差分
            var delta = headPos - backPos;

            // dividing by zeroエラーが怖いので確認
            if (delta.z == 0) return;

            // シェーダーのプロパティを更新
            sprite.material.SetVector(shearId, new Vector4(delta.x / delta.z, 0, 0, 0));

            // サイズの変更
            sprite.size = new Vector2(sprite.size.x, delta.z);
        }


        /// <summary>
        ///     個ノーツの設定
        /// </summary>
        /// <param name="note"></param>
        public void SetChildNote(NoteController note)
        {
            if (note == null)
            {
                sprite.color = new Color(0, 0, 0, 0);
                return;
            }

            sprite.color = Color.white;
            back = note.Trn;
        }
    }
}