using System;
using System.Collections;
using System.Collections.Generic;
using Chart;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 盤面上のサイズ調節や、ノーツの生成位置を取得するクラス
    /// </summary>
    public class Field : MonoSingleton<Field>
    {
        [SerializeField] private Transform fieldTrn,judgeLine,spawnTrn;
        
        /// <summary>
        /// レーン数
        /// </summary>
        private int MaxBlock { get; set; }
        
        /// <summary>
        /// レーン数を設定
        /// </summary>
        /// <param name="maxBlock"></param>
        public void Initialize(int maxBlock)
        {
            MaxBlock = maxBlock;
            var scaleField = fieldTrn.localScale;
            fieldTrn.localScale = new Vector3(maxBlock, scaleField.y, scaleField.z);
            
            var scaleLine = judgeLine.localScale;
            judgeLine.localScale = new Vector3(maxBlock, scaleLine.y, scaleLine.z);
        }

        /// <summary>
        /// ノーツ生成位置を取得する
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public Vector3 GetSpawnPos(int block)
        {
            float y = 0;
            float z = spawnTrn.position.z;
            float x = -MaxBlock / 2f + block + 0.5f;
            return new Vector3(x, y, z);
        }
        
    }
}
