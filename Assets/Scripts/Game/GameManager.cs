using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class GameManager
    {
        public static BoardManager BoardManager = new();
        public static int CurrentDiceThrownNumber;
        public static int PlayerNumber;
        public static List<Player> Players = new List<Player>();
        public static Player MyPlayer;
    }
}
