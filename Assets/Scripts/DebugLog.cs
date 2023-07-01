using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class DebugLog : MonoBehaviour
{
    [SerializeField] private Text debugText;
    [SerializeField] private int logsVisible = 35;
    
    void Update()
    {
        debugText.text = "";
        for (var i = GameManager.DebugLogs.Count - logsVisible; i < GameManager.DebugLogs.Count; i++)
        {
            if (i >= 0)
                debugText.text += GameManager.DebugLogs[i] + "\n";
        }
    }
}
