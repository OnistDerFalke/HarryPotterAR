using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DebugLog : MonoBehaviour
{
    [SerializeField] private Button hideLogsButton;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Text debugText;
    [SerializeField] private int logsVisible = 35;
    private bool logsSeen = true;
    private int offset = 0;

    void Update()
    {
        debugText.text = "";
        if (!logsSeen) { }
        else
        {
            for (var i = GameManager.DebugLogs.Count - logsVisible - offset; i < GameManager.DebugLogs.Count - offset; i++)
            {
                if (i >= 0) debugText.text += GameManager.DebugLogs[i] + "\n";
            }
        }
    }
    
    private void OnLogsButton()
    {
        logsSeen = !logsSeen;
    }

    private void OnUpButton()
    {
        offset++;
    }

    private void OnDownButton()
    {
        if (offset > 0)
            offset--;
    }
}
