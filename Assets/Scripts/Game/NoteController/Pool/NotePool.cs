using Game;
using UniRx;
using UniRx.Toolkit;
using UnityEngine;

public class NotePool : ObjectPool<NoteController>
{
    public NotePool(int number)
    {
        PreloadAsync(number, 10).Subscribe();
        NoteParent = new GameObject("_noteroot").transform;
    }

    private Transform NoteParent { get; }

    /// <summary>
    ///     インスタンスの生成
    /// </summary>
    /// <returns></returns>
    protected override NoteController CreateInstance()
    {
        var prefab = Resources.Load<NoteController>("Game/note");
        return Object.Instantiate(prefab, NoteParent);
    }
}