using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public static class GameManager
{
    
    public static List<string> DebugLogs = new();
    public static Dictionary<string, VuMarkBehaviour> CurrentTrackedObjects = new();
    public static float MeasureTimeStart;
    public static float[] MarkersDetectionTimes = new float[9];

    public static void DebugLog(string log)
    {
        DebugLogs.Add(log);
    }

    public static void ClearLogs()
    {
        DebugLogs = new();
    }
}