using System.Collections.Generic;
using System.Linq;
using Game;
using Vuforia;

namespace Assets.Scripts
{
    public static class GameManager
    {
        public static BoardManager BoardManager = new();
        public static int CurrentDiceThrownNumber;
        public static int PlayerNumber;
        public static List<Player> Players;
        public static Dictionary<string, VuMarkBehaviour> CurrentTrackedObjects;
        public static bool setup = false;
        public static int ChosenIndex;

        public static void Setup()
        {
            if (!setup)
            {
                BoardManager.Setup();
                Players = new();
                CurrentDiceThrownNumber = -1;
                PlayerNumber = 0;
                CurrentTrackedObjects = new();
                setup = true;
            }                
        }

        public static Player GetMyPlayer()
        {
            return Players.Where(player => player.IsMyPlayer).FirstOrDefault();
        }
    }
}
