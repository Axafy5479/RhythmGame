using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings")]
public class Settings : ScriptableObject
{
    #region Singleton

    private static Settings instance;

    public static Settings Instance
    {
        get
        {
            if (instance == null)
            {
                var instances = Resources.LoadAll<Settings>("");
                instance = instances.Length switch
                {
                    0 => throw new Exception("ResourcesにSettingsのインスタンスが存在しません"),
                    1 => instances[0],
                    _ => throw new Exception("ResourcesにSettingsのインスタンスが複数存在します"),
                };
            }

            return instance;
        }
    }
    #endregion

    // タップによる判定に対し左右にそれぞれ　(noteTapTolerance / 2) の許容範囲を作る
    public float NoteTapTolerance => noteTapTolerance;
    [SerializeField] private float noteTapTolerance = 0.6f; 
    
    // スライドの判定に対し左右にそれぞれ　(noteSlideTolerance / 2) の許容範囲を作る
    public float NoteSlideTolerance => noteSlideTolerance;
    [SerializeField] private float noteSlideTolerance = 1f; 
}
