using UnityEngine;

namespace Assets.Scripts
{  
    public class Player
    {
        [SerializeField] private int index;
        [SerializeField] private Character character;
        [SerializeField] private bool isDuringMove;
        [SerializeField] private bool myPlayer;
        [SerializeField] private int lastFieldId;
        [SerializeField] private Vector2 lastPosition;
        //[SerializeField] private GameObject characterObject;

        public int Index { get => index; }
        public Character Character { get => character; }
        public bool IsDuringMove { get => isDuringMove; }
        public bool IsMyPlayer { get => myPlayer; }
        public int LastFieldId { get => lastFieldId; }
        public Vector2 LastPosition { get => lastPosition; }

        public Player(int index)
        {
            this.index = index;
            if (this.index == 0) 
                this.myPlayer = true;
            else 
                this.myPlayer = false;
            this.character = Character.None;
            this.isDuringMove = false;
            this.lastFieldId = 0;
            this.lastPosition = GameManager.BoardManager.GetFieldById(0).Position2D;
        }

        public void SetCharacter(Character character)
        {
            this.character = character;
        }

        // TODO: delete function
        public void ChangeField(int fieldId)
        {
            lastFieldId = fieldId;
        }
    }
}
