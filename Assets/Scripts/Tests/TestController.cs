using System;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    [SerializeField] public Button MainButton;
    [SerializeField] public InputField FileNameInput;
    [SerializeField] public Text Status;

    void Start()
    {
        TestManager.Setup();
    }

    void Update()
    {
        MainButton.GetComponentInChildren<Text>().text = TestManager.DetectingStarted ? $"Zatrzymaj wykrywanie\nCzas wykrywania: {Math.Round(TestManager.Time, 3)}" : "Rozpocznij wykrywanie";
        Status.text = $"Obecnie wykrytych: {TestManager.CurrentTrackedObjects.Count}";

        if (TestManager.DetectingStarted)
            TestManager.Time += Time.deltaTime;
        if (TestManager.Time >= TestManager.MaxTime && TestManager.DetectingStarted)
        {
            TestManager.DetectingStarted = false;
            TestManager.Time = 0;
            FileNameInput.text = "";
        }

        FileNameInput.interactable = !TestManager.DetectingStarted;
        MainButton.interactable = FileNameInput.text != "" || TestManager.DetectingStarted;
    }

    public void OnMainButtonClick()
    {
        TestManager.DetectingStarted = !TestManager.DetectingStarted;
        if (!TestManager.DetectingStarted)
        {
            TestManager.Time = 0;
            FileNameInput.text = "";
        }
        else
            TestManager.SetFilePath(FileNameInput.text);
    }
}
