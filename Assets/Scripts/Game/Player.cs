using Assets.Scripts;
using UnityEngine;

namespace Game
{  
    public class Player
    {
        private int index;
        private Character character;
        private bool isDuringMove;
        private bool myPlayer;
        private int lastFieldId;
        private Vector2 lastPosition;
        //[SerializeField] private GameObject characterObject;

        public int Index { get => index; }
        public Character Character { get => character; }
        public bool IsDuringMove { get => isDuringMove; }
        public bool IsMyPlayer { get => myPlayer; }
        public int LastFieldId { get => lastFieldId; }
        public Vector2 LastPosition { get => lastPosition; }

        public Player(int index, Character character)
        {
            this.index = index;
            this.character = character;
            myPlayer = this.index == 0;
            isDuringMove = false;
            lastFieldId = 0;
            
            /*
             * null reference when I create new player in menu scene so I add condition over
             * just look if it is ok for you
             */
            if(GameManager.BoardManager.GetFieldById(0) != null)
                lastPosition = GameManager.BoardManager.GetFieldById(0).Position2D;
        }

        /*
         * imo we won't need this, we pass character by constructor
         */
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
