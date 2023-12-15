using UnityEngine;

namespace Game.Test
{
    public class LongNoteShearTest : MonoBehaviour
    {
        [SerializeField] private NoteController back;

        [SerializeField] private LongBand band;

        // Start is called before the first frame update
        private void Start()
        {
            band.SetChildNote(back);
        }
    }
}