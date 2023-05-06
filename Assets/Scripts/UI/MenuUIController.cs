using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject[] MenuContext = new GameObject[3];

        private enum Contexts
        {
            StartContext,
            PlayersContext,
            CharactersContext
        }

        void Start()
        {
            ChangeContext(Contexts.StartContext);
        }

        void ChangeContext(Contexts context)
        {
            foreach (var c in MenuContext)
                c.SetActive(false);
            MenuContext[(int)context].SetActive(true);
        }

        public void OnStartContextPlayButtonClick()
        {
            ChangeContext(Contexts.PlayersContext);
        }
        
        public void OnPlayersContextNextButtonClick()
        {
            SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        }
    }
}
