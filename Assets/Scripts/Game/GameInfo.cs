using System;
using Chart;
using UnityEngine;

/// <summary>
///     ゲームの情報
///     ユーザーはどの曲を選択したか、どの難易度を選択したかなど、
///     ゲームを開始するために必要な情報
///     デバッグなどで、ゲームシーンからプレイを開始したい場合を考慮し、シリアル化可能にしている。
/// </summary>
[Serializable]
public class GameInfo
{
    [SerializeField] private int musicId;
    [SerializeField] private Course course;
    [SerializeField] private bool isAuto, isTutorial;

    /// <summary>
    ///     コンストラクタ
    /// </summary>
    /// <param name="musicId"></param>
    /// <param name="course"></param>
    /// <param name="isAuto"></param>
    /// <param name="isTutorial"></param>
    public GameInfo(int musicId, Course course, bool isAuto, bool isTutorial)
    {
        this.musicId = musicId;
        this.course = course;
        this.isAuto = isAuto;
        this.isTutorial = isTutorial;
    }

    /// <summary>
    ///     楽曲Id
    /// </summary>
    public int MusicId => musicId;

    /// <summary>
    ///     コース (EASY,HARD など)
    /// </summary>
    public Course Course => course;

    /// <summary>
    ///     オート演奏か否か
    /// </summary>
    public bool IsAuto => isAuto;

    /// <summary>
    ///     チュートリアルか否か
    /// </summary>
    public bool IsTutorial => isTutorial;

    /// <summary>
    ///     譜面を取得
    /// </summary>
    /// <returns></returns>
    public ChartDTO GetChart()
    {
        return ChartDataUtility.GetChartById(musicId);
    }
}