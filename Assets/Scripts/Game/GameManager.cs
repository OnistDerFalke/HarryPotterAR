using System.Collections.Generic;
using System.Linq;
using Game;

namespace Assets.Scripts
{
    public static class GameManager
    {
        public static BoardManager BoardManager = new();
        public static int CurrentDiceThrownNumber;
        public static int PlayerNumber;
        public static List<Player> Players = new List<Player>();

        public static void Setup()
        {
            BoardManager.Setup();
        }

        public static Player GetMyPlayer()
        {
            return Players.Where(player => player.IsMyPlayer).FirstOrDefault();
        }
    }
}
