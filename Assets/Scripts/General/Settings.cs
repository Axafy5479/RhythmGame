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

    [SerializeField] private int perfectThreshold = 40;
    [SerializeField] private int goodThreshold = 100;
    [SerializeField] private int missThreshold = 150;

    /// <summary>
    /// 各判定の閾値を取得
    /// </summary>
    /// <param name="judge"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public int JudgeTime(JudgeEnum judge)
    {
        return judge switch
        {
            JudgeEnum.Perfect => perfectThreshold,
            JudgeEnum.Good => goodThreshold,
            JudgeEnum.Miss => missThreshold,
            _ => throw new ArgumentOutOfRangeException(nameof(judge), judge, null)
        };
    }

    /// <summary>
    /// ズレ時間から判定を算出
    /// </summary>
    /// <param name="gapTime"></param>
    /// <returns></returns>
    public JudgeEnum GetJudge(int gapTime)
    {
        gapTime = Mathf.Abs(gapTime);
        if (gapTime < perfectThreshold) return JudgeEnum.Perfect;
        else if (gapTime < goodThreshold) return JudgeEnum.Good;
        else if (gapTime < missThreshold) return JudgeEnum.Miss;
        else return JudgeEnum.None;
    }
}
