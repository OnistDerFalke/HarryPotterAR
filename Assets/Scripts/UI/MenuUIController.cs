using System;
using System.Linq;
using Assets.Scripts;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject[] menuContext = new GameObject[2];
        [SerializeField] private GameObject characterBox;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button nextCharacterButton;
        [SerializeField] private Button previousCharacterButton;
        [SerializeField] private Sprite[] vuMarksSprites = new Sprite[9];
        [SerializeField] private Sprite vuMarksSpriteNotSet;
        [SerializeField] private Text characterName;
        [SerializeField] private Image vuMarkImage;
        
        private enum Contexts
        {
            StartContext,
            CharactersContext
        }

        private enum SwitchDirection
        {
            Left,
            Right
        }

        private int playersNumber;
        private int charactersNumber = 9;
        private int chosenCharacterIndex;
        private Character[] chosenCharacters;
        private bool[] takenCharacters;

        private void Start()
        {
            ChangeContext(Contexts.StartContext);
            playersNumber = 1;
        }

        private void Update()
        {
            startGameButton.interactable = chosenCharacterIndex != 0;
        }

        private bool IfAllCharactersChosen()
        {
            return chosenCharacters.All(character => character != Character.None);
        }
                
        private void SwitchCharacter(SwitchDirection direction)
        {
            do
            {
                switch (direction)
                {
                    case SwitchDirection.Left:
                        chosenCharacterIndex =
                            chosenCharacterIndex == 0 ? charactersNumber - 1 : chosenCharacterIndex - 1;
                        break;
                    case SwitchDirection.Right:
                        chosenCharacterIndex = (chosenCharacterIndex + 1) % (charactersNumber + 1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            } while (takenCharacters[chosenCharacterIndex]);

            UpdateCharactersContext();
        }

        private void UpdateCharactersContext()
        {
            characterName.text = chosenCharacterIndex == 0 ? 
                "Nie wybrano" : ((Character)Enum.ToObject(typeof(Character), chosenCharacterIndex)).ToString();
            if (chosenCharacterIndex <= charactersNumber)
            {
                vuMarkImage.sprite = chosenCharacterIndex == 0
                    ? vuMarksSpriteNotSet : vuMarksSprites[chosenCharacterIndex - 1];
            }
        }

        private void ChangeContext(Contexts context)
        {
            foreach (var c in menuContext)
                c.SetActive(false);
            menuContext[(int)context].SetActive(true);
        }

        private void LoadDataToGameManager()
        {
            GameManager.Setup();
            GameManager.PlayerNumber = playersNumber;
            for(var i = 0; i < playersNumber; i++)
                GameManager.Players.Add(new Player(i, chosenCharacters[i]));
        }

        public void OnStartContextPlayButtonClick()
        {
            ChangeContext(Contexts.CharactersContext);
            chosenCharacters = new Character[playersNumber];
            takenCharacters = new bool[charactersNumber + 1];
            chosenCharacterIndex = 0;

            startGameButton.gameObject.SetActive(true);
            characterBox.SetActive(true);
            nextCharacterButton.gameObject.SetActive(true);
            previousCharacterButton.gameObject.SetActive(true);

            UpdateCharactersContext();
        }

        public void OnBackButtonClick()
        {
            ChangeContext(Contexts.StartContext);
        }

        public void OnStartGameButtonClick()
        {
            LoadDataToGameManager();
            SceneManager.LoadScene("Scenes/Beta", LoadSceneMode.Single);
        }

        public void OnNextCharacterButtonClick()
        {
            SwitchCharacter(SwitchDirection.Right);
        }

        public void OnPreviousCharacterButtonClick()
        {
            SwitchCharacter(SwitchDirection.Left);
        }
    }
}
