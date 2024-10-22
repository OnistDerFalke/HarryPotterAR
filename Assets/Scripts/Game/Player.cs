﻿using Assets.Scripts;
using System.Collections.Generic;
using Scripts;

namespace Game
{  
    public class Player
    {
        private int index;
        private Character character;
        private SpecialPower specialPower;
        private bool isDuringMove;
        private bool myPlayer;
        private int lastFieldId;

        public int Index { get => index; }
        public Character Character { get => character; }
        public SpecialPower SpecialPower { get => specialPower; }
        public bool IsDuringMove { get => isDuringMove; set => isDuringMove = value; }
        public bool IsMyPlayer { get => myPlayer; }
        public int LastFieldId { get => lastFieldId; set => lastFieldId = value; }

        public Player(int index, Character character)
        {
            this.index = index;
            this.character = character;
            myPlayer = this.index == 0;
            isDuringMove = false;
            lastFieldId = 0;
            this.specialPower = GetSpecialPower();
        }

        public string GetCurrentFieldName()
        {
            if (lastFieldId >= 0)
                return GameManager.BoardManager.GetFieldById(lastFieldId).Name;
            return "Sczytaj pozycję gracza";
        }

        public string GetCurrentFieldActions()
        {
            if (lastFieldId >= 0)
                return GameManager.BoardManager.GetFieldById(lastFieldId).GetActionsInfo();
            return "Sczytaj pozycję gracza poprzez nakierowanie kamery telefonu na znacznik gracza. " +
                    "Zrób to tak, aby w obrębie ekranu był widoczny przynajmniej jeden znacznik planszy";
        }

        public List<Instruction.InstructionInfo> GetCurrentInstructionParts()
        {
            return GameManager.BoardManager.GetFieldById(lastFieldId).InstructionParts;
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

        private SpecialPower GetSpecialPower()
        {
            return CharacterSpecialPower.GetSpecialPower(this.character);
        }
    }
}
