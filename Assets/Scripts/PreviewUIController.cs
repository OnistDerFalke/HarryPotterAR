using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviewUIController : MonoBehaviour
{
    public void OnMeasureButtonClick()
    {
        SceneManager.LoadScene("Scenes/Beta", LoadSceneMode.Single);
    }
}
