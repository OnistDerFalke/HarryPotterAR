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
        public bool IsDuringMove { get => isDuringMove;  set => isDuringMove = value;}
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

        public string GetCurrentFieldName()
        {
            if (lastFieldId < 0)
                return GameManager.BoardManager.GetFieldById(lastFieldId).Name;
            Debug.Log("Aktualne pole gracza: " + lastFieldId);
            return "";
        }

        public string GetCurrentFieldActions()
        {
            if (lastFieldId < 0)
                return GameManager.BoardManager.GetFieldById(lastFieldId).GetActionsInfo();
            Debug.Log("Aktualne pole gracza: " + lastFieldId);
            return "";
        }

        public static Character CharacterFromString(string s)
        {
            switch (s)
            {
                case "harr":
                    return Character.Harry;
                case "herm":
                    return Character.Hermiona;
                case "luna":
                    return Character.Luna;
                case "pete":
                    return Character.Peter;
                case "drac":
                    return Character.Draco;
                case "rono":
                    return Character.Ron;
                case "cedr":
                    return Character.Cedrik;
                case "nevi":
                    return Character.Neville;
                case "ginn":
                    return Character.Ginny;
                default:
                    return Character.None;
            }
        }
    }
}
