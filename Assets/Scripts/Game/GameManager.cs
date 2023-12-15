using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 初期化や終了判定など、ゲーム全体の流れを制御する
    /// </summary>
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private GameInfo sampleGameInfo;
        
        /// <summary>
        /// サンプルゲームの情報
        /// </summary>
        public GameInfo SampleGameInfo => sampleGameInfo;
        
        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            TimeCalculator.Instance.Play();
        }

        /// <summary>
        /// 初期化が必要なオブジェクトを探し、初期化
        /// </summary>
        private void Initialize()
        {
            // 全オブジェクトを確認し、INeedInitializingを探す
            var monos = FindObjectsOfType<MonoBehaviour>();
            foreach (var mono in monos)
            {
                // INeedInitializingが見つかったら実行
                (mono as INeedInitializing)?.Initialize(sampleGameInfo);
            }
        }

    }
}
