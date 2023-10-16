using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreviewUIController : MonoBehaviour
{
    public Text measureTime;

    public void Start()
    {
        measureTime.text = $"{GameManager.MeasureTime.ToString(CultureInfo.CurrentCulture)}s";
    }

    public void OnMeasureButtonClick()
    {
        SceneManager.LoadScene("Scenes/Beta", LoadSceneMode.Single);
    }

    public void OnMeasureTimeChangeButtonClick()
    {
        GameManager.MeasureTime += 1f;
        if (GameManager.MeasureTime > GameManager.MeasureTimeLimit)
            GameManager.MeasureTime = 1f;
        measureTime.text = $"{GameManager.MeasureTime.ToString(CultureInfo.CurrentCulture)}s";
    }
}
