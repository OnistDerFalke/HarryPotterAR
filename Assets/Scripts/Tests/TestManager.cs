using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Vuforia;

public static class TestManager
{
    public static Dictionary<string, VuMarkBehaviour> CurrentTrackedObjects;
    public static bool DetectingStarted;
    public static float Time;
    public static float MaxTime = 10f;
    private static string FilePath;
    public static string Logs;
    public static string FileName;

    public static bool sceneReloaded;

    public static void Setup()
    {
        DetectingStarted = false;
        CurrentTrackedObjects = new();
        Time = 0;
    }

    public static void SetFilePath(string fileName)
    {
        FilePath = Application.persistentDataPath + $"/{fileName}.txt";
        File.Create(FilePath);
    }

    public static void UpdateDetected(string id, bool detected, VuMarkBehaviour vmb = null)
    {
        if (detected && !CurrentTrackedObjects.ContainsKey(id))
        {
            CurrentTrackedObjects.Add(id, vmb);
            SaveToFile($"{Time}: Wykryto znacznik {id} \t Obecnie wykrytych: {CurrentTrackedObjects.Count()}");
            Debug.Log("Wykryto znacznik " + id + $" \t Obecnie wykrytych: {CurrentTrackedObjects.Count()}");
        }
        else if (!detected && CurrentTrackedObjects.ContainsKey(id))
        {
            CurrentTrackedObjects.Remove(id);
            SaveToFile($"{Time}: Zgubiono znacznik {id} \t Obecnie wykrytych: {CurrentTrackedObjects.Count()}");
            Debug.Log("Zgubiono znacznik " + id + $" \t Obecnie wykrytych: {CurrentTrackedObjects.Count()}");
        }
    }

    private static void SaveToFile(string data)
    {
        File.AppendAllText(FilePath, data + "\n");
    }
}
