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
        public static List<Player> Players = new List<Player>();
        public static List<string> DebugLogs = new();
        public static Dictionary<string, VuMarkBehaviour> CurrentTrackedObjects = new();
        public static bool setup = false;

        public static void Setup()
        {
            BoardManager.Setup();
            setup = true;
        }

        public static Player GetMyPlayer()
        {
            return Players.Where(player => player.IsMyPlayer).FirstOrDefault();
        }

        public static void DebugLog(string log)
        {
            DebugLogs.Add(log);
        }
    }
}
