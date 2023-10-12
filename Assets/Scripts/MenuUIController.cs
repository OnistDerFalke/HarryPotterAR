using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    public void OnStartContextPlayButtonClick()
    {
        SceneManager.LoadScene("Scenes/Beta", LoadSceneMode.Single);
    }
}