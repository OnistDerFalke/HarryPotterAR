using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    [SerializeField] public Button MainButton;
    [SerializeField] public InputField FileNameInput;
    [SerializeField] public Text Status;

    void Start()
    {
        TestManager.Setup();

        if (TestManager.sceneReloaded)
        {
            FileNameInput.text = TestManager.FileName;
            TestManager.DetectingStarted = true;
            TestManager.sceneReloaded = false;
        }
    }

    void Update()
    {
        MainButton.GetComponentInChildren<Text>().text = TestManager.DetectingStarted ?
            $"Zatrzymaj wykrywanie\nCzas wykrywania: {Math.Round(TestManager.Time, 3)}" : "Rozpocznij wykrywanie";
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
        if (!TestManager.DetectingStarted)
        {
            TestManager.SetFilePath(FileNameInput.text);
            TestManager.FileName = FileNameInput.text;
            TestManager.sceneReloaded = true;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        else
        {
            TestManager.DetectingStarted = false;
            TestManager.Time = 0;
            FileNameInput.text = "";
        }
    }
}