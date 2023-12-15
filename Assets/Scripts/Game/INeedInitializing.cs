using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 初期化が必要なコンポーネントが実装する
    /// GameManagerがこのインターフェースを実装しているコンポーネントを探し、
    /// GameInfoを配る
    /// </summary>
    public interface INeedInitializing
    {
        /// <summary>
        /// 初期化メソッド
        /// ゲーム開始時にGameManagerから呼ばれる
        /// </summary>
        /// <param name="gameInfo"></param>
        void Initialize(GameInfo gameInfo);
    }
}
