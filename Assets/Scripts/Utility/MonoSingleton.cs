using System;
using UnityEngine;

public abstract class MonoSingleton<T> :MonoBehaviour where T: MonoSingleton<T>
{
    /// <summary>
        /// 唯一のインスタンス
        /// </summary>
        private static T instance;

        /// <summary>
        /// シングルトンなインスタンスを取得
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // instanceがnullの場合、唯一のインスタンスを探す

                    // シーンに存在する全てのTのインスタンスを探す
                    var instances = FindObjectsOfType<T>();
                    
                    // 見つけたTのインスタンスの数で場合わけ
                    instance = instances.Length switch
                    {
                        // インスタンスが存在しない場合、Resources内のプレハブを探す
                        0 => throw new Exception($"{typeof(T)}のインスタンスがシーン上に存在しません。"),
                        
                        // 唯一である場合、それを返す
                        1 => instances[0],
                        
                        // 複数ある場合、シングルトンの制約に反しているため例外を投げる
                        _ => throw new Exception($"{typeof(T)}のインスタンスがシーン上に複数存在します。")
                    };

                    // 見つけた唯一のインスタンスを返す
                    instance.BeforeFirstGetterCalling();
                }

                return instance;
            }
        }

        /// <summary>
        /// 初めてインスタンスが呼ばれたことのコールバックメソッド
        /// </summary>
        protected virtual void BeforeFirstGetterCalling()
        {
        }

        private void OnDestroy()
        {
            // baseクラスでOnDestroy()を使用した場合、子クラスで使えなくなってしまうため
            // OnDestroyと同時に呼ばれる代替メソッドを呼ぶ
            BeforeOnDestroy();
            
            // シングルトンパターンでは、インスタンスの寿命は自分で管理する必要がある。
            // ゲームオブジェクトが削除された時、シングルトンコンポーネントのインスタンスも自分で消す
            instance = null;
        }
        
        /// <summary>
        /// OnDestroyの代替メソッド
        /// </summary>
        protected abstract void BeforeOnDestroy();
}
