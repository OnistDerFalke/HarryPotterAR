using System.Collections.Generic;
using Vuforia;

public static class GameManager
{
    
    public static List<string> DebugLogs = new();
    public static Dictionary<string, VuMarkBehaviour> CurrentTrackedObjects = new();
  
    public static void DebugLog(string log)
    {
        DebugLogs.Add(log);
    }
}