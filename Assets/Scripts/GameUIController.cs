using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{ 
    [Header("Buttons")]
    [SerializeField] private Button measureButton;
    [SerializeField] private Text measureButtonText;

    private bool _isMeasuring, _isMeasured;

    public void OnMeasureButtonClick()
    {
        Debug.Log("MEASURE");
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
        GameManager.DebugLog("Rozpoczeto pomiary.");
        _isMeasuring = true;
    }

    void Start()
    {
        measureButtonText.text = "ROZPOCZNIJ POMIARY";
    }
    
    void Update()
    {
        if (_isMeasured) return;
        if (!(_isMeasuring || _isMeasured)) return;
        measureButtonText.text = (Time.time - GameManager.MeasureTimeStart).ToString(CultureInfo.CurrentCulture);
        if (Time.time - GameManager.MeasureTimeStart >= 10f)
        {
            _isMeasuring = false;
            _isMeasured = true;
            measureButtonText.text = "ZAKONCZ POMIARY";
            GameManager.DebugLog("Czasy wykrycia znaczników:");
            for (var i = 0; i < 9; i++)
            {
                GameManager.DebugLog($"Marker {i + 1} : {GameManager.MarkersDetectionTimes[i]}");
            }
            GameManager.DebugLog($"Wykryto {GameManager.CurrentTrackedObjects.Count}/9 znaczników " +
                                 $"({100f*GameManager.CurrentTrackedObjects.Count/9f}%).");
            GameManager.DebugLog("Pomiary zakonczone sukcesem.");
        }
    }
}