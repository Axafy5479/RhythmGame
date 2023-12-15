using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UniRx;
using UniRx.Toolkit;


public class NotePool : ObjectPool<NoteController>
{
    public NotePool(int number)
    {
        PreloadAsync(number, 10).Subscribe();
    }

    protected override NoteController CreateInstance()
    {
        return Object.Instantiate(Resources.Load<NoteController>("Game/note"));
    }
}
