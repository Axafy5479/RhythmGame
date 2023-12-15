using UnityEngine;

namespace Game
{
    /// <summary>
    ///     盤面上のサイズ調節や、ノーツの生成位置を取得するクラス
    /// </summary>
    public class Field : MonoSingleton<Field>,INeedInitializing
    {
        [SerializeField] private Transform fieldTrn, judgeLine, spawnTrn;

        private Camera mainCamera;
        private LayerMask mask;

        /// <summary>
        ///     レーン数
        /// </summary>
        private int BlockNum { get; set; }

        /// <summary>
        ///     フィールドの初期化
        /// </summary>
        /// <param name="blockNum">レーン数</param>
        public void Initialize(GameInfo info)
        {
            // レーン数
            BlockNum = info.BlockNumber;

            // フィールドのサイズを調節する
            var scaleField = fieldTrn.localScale;
            fieldTrn.localScale = new Vector3(BlockNum, scaleField.y, scaleField.z);

            // 判定線のサイズを調節する
            var scaleLine = judgeLine.localScale;
            judgeLine.localScale = new Vector3(BlockNum, scaleLine.y, scaleLine.z);

            // プレイヤーのタッチ位置から、フィールドのどこがタッチされたかを計算する際に
            // Rayを飛ばして計算する。
            // この計算速度を向上させるためにLayerMaskを用いる
            mask = LayerMask.GetMask("Field");

            // スクリーン位置からRayを飛ばすため、カメラのインスタンスを保持しておく
            mainCamera = Camera.main;
        }

        /// <summary>
        ///     ノーツ生成位置を取得する
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public Vector3 GetSpawnPos(int block)
        {
            float y = 0;
            var z = spawnTrn.position.z;
            var x = -BlockNum / 2f + block + 0.5f;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     タッチされたスクリーン位置から、判定ライン上の位置に変換する
        /// </summary>
        /// <param name="screenPos">タッチされたスクリーン位置</param>
        /// <returns>(pos 判定ライン上のいち, ok 正しく値が得られたか (Field以外の場所だった場合にfalseとなる) )</returns>
        public (Vector2 pos, bool ok) ScreenToFieldPosition(Vector2 screenPos)
        {
            // クリックした位置から飛ばすRayを作成
            var r = mainCamera.ScreenPointToRay(screenPos);

            // Rayを飛ばす ()
            if (Physics.Raycast(r, out var hit, 100, mask))
            {
                //  hit は RayとLineの交点
                var posOnLine = new Vector3(hit.point.x, hit.point.y, 0);

                return (posOnLine, true);
            }

            return (Vector3.zero, false);
        }
    }
}