using UnityEngine;

namespace Assets.Scripts
{  
    public class Player
    {
        [SerializeField] private int index;
        [SerializeField] private Character character;
        [SerializeField] private bool isDuringMove;
        [SerializeField] private int lastFieldId;
        [SerializeField] private Vector2 lastPosition;
        //[SerializeField] private GameObject characterObject;

        public int Index { get => index; }
        public Character Character { get => character; }
        public bool IsDuringMove { get => isDuringMove; }
        public int LastFieldId { get => lastFieldId; }
        public Vector2 LastPosition { get => lastPosition; }

        public Player(int index, bool isDuringMove=false)
        {
            this.index = index;
            this.isDuringMove = isDuringMove;
        }

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
    }
}
