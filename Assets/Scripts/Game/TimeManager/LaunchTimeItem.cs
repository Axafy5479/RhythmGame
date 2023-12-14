using System;
using UnityEngine;

namespace Game.TimeManager
{
    [Serializable]
    public class LaunchTimeItem
    {
        public LaunchTimeItem(int noteId, int launchTime)
        {
            this.noteId = noteId;
            this.launchTime = launchTime;
        }

        [SerializeField] private int noteId;
        [SerializeField] private int launchTime;
        [SerializeField] private bool hasLaunched = false;
        public int NoteId => noteId;
        public int LaunchTime => launchTime;
        public bool HasLaunched
        {
            get => hasLaunched;
            set => hasLaunched = value;
        }
    }
}