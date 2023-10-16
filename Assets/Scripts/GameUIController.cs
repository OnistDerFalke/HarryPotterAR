using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{ 
    [Header("Buttons")]
    [SerializeField] private Text measureButtonText;

    private bool _isMeasuring, _isMeasured;

    public void OnMeasureButtonClick()
    {
        if (_isMeasured)
        {
            GameManager.ClearLogs();
            SceneManager.LoadScene("Scenes/Menu");
        }
        
        if (_isMeasuring)
        {
            GameManager.ClearLogs();
            SceneManager.LoadScene("Scenes/Menu");
        }

        if (_isMeasuring) return;
        GameManager.MeasureTimeStart = Time.time;
        GameManager.DebugLog("Measurement started.");
        _isMeasuring = true;
    }

    void Start()
    {
        GameManager.CurrentTrackedObjects = new();
        GameManager.MarkersDetectionTimes = new float[9];
        GameManager.MeasureTimeStart = 0f;
        OnMeasureButtonClick();
    }
    
    void Update()
    {
        if (_isMeasured) return;
        if (!_isMeasuring) return;
        measureButtonText.text = (Time.time - GameManager.MeasureTimeStart).ToString(CultureInfo.CurrentCulture);
        if (Time.time - GameManager.MeasureTimeStart >= GameManager.MeasureTime)
        {
            _isMeasuring = false;
            _isMeasured = true;
            measureButtonText.text = "LEAVE";
            GameManager.DebugLog("Marker detection time:");
            float averageDetectionTime = 0f;
            int detections = 0;
            for (var i = 0; i < 9; i++)
            {
                if(GameManager.MarkersDetectionTimes[i] <= 0f)
                    GameManager.DebugLog($"Marker {i + 1} : Not detected or lost.");
                else
                {
                    GameManager.DebugLog($"Marker {i + 1} : {GameManager.MarkersDetectionTimes[i]}s.");
                    detections++;
                    averageDetectionTime += GameManager.MarkersDetectionTimes[i];
                }
            }

            averageDetectionTime /= detections;
            
            GameManager.DebugLog($"{GameManager.CurrentTrackedObjects.Count}/9 markers detected " +
                                 $"({100f*GameManager.CurrentTrackedObjects.Count/9f}%).");
            GameManager.DebugLog($"Average detection time is {averageDetectionTime}s (in {detections} detections).");
            GameManager.DebugLog("Measurement complete!");
        }
    }
}