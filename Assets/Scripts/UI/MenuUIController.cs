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
        [SerializeField] private GameObject[] menuContext = new GameObject[3];
        [SerializeField] private Text[] summaryCharactersNames = new Text[8];
        [SerializeField] private GameObject characterBox;
        [SerializeField] private GameObject summaryBox;
        [SerializeField] private PlayersSwipe playerSwipe;
        [SerializeField] private Button nextPlayerButton;
        [SerializeField] private Button previousPlayerButton;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button editCharactersButton;
        [SerializeField] private Button nextCharacterButton;
        [SerializeField] private Button previousCharacterButton;
        [SerializeField] private Sprite[] vuMarksSprites = new Sprite[9];
        [SerializeField] private Sprite vuMarksSpriteNotSet;
        [SerializeField] private Text playerName;
        [SerializeField] private Text characterName;
        [SerializeField] private Image vuMarkImage;
        
        private enum Contexts
        {
            StartContext,
            PlayersContext,
            CharactersContext
        }

        private enum SwitchDirection
        {
            Left,
            Right
        }

        private int playersNumber, charactersNumber = 9;
        private int chosenPlayerIndex = 0, chosenCharacterIndex;
        private Character[] chosenCharacters;
        private bool[] takenCharacters;

        private void Start()
        {
            ChangeContext(Contexts.StartContext);
        }

        private void Update()
        {
            CheckButtonsActivity();
        }

        private bool IfAllCharactersChosen()
        {
            return chosenCharacters.All(character => character != Character.None);
        }
        
        private void CheckButtonsActivity()
        {
            nextPlayerButton.interactable = chosenCharacterIndex != 0;
            if (chosenPlayerIndex == 0)
                previousPlayerButton.interactable = false;
            else previousPlayerButton.interactable = chosenCharacterIndex != 0;

            nextPlayerButton.GetComponentInChildren<Text>().text = 
                chosenPlayerIndex == playersNumber - 1 ? "Podsumowanie" : "Nastepny";
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
            playerName.text = chosenPlayerIndex == 0 ? "Ja" : $"Gracz{chosenPlayerIndex + 1}";
            characterName.text = chosenCharacterIndex == 0 ? 
                "Nie wybrano" : ((Character)Enum.ToObject(typeof(Character), chosenCharacterIndex)).ToString();
            if (chosenCharacterIndex < charactersNumber)
                vuMarkImage.sprite = chosenCharacterIndex == 0 
                    ? vuMarksSpriteNotSet : vuMarksSprites[chosenCharacterIndex - 1];
        }

        private void ChangeContext(Contexts context)
        {
            foreach (var c in menuContext)
                c.SetActive(false);
            menuContext[(int)context].SetActive(true);
        }

        private void ChangeCharacterBoxContext(bool allChosen)
        {
            if (allChosen)
            {
                for (var i = 0; i < 8; i++)
                {
                    summaryCharactersNames[i].text = i < chosenCharacters.Length ? 
                        chosenCharacters[i].ToString() : "(Nieaktywny)";
                }
            }
            nextPlayerButton.gameObject.SetActive(!allChosen);
            previousPlayerButton.gameObject.SetActive(!allChosen);
            startGameButton.gameObject.SetActive(allChosen);
            editCharactersButton.gameObject.SetActive(allChosen);
            characterBox.SetActive(!allChosen);
            summaryBox.SetActive(allChosen);
            nextCharacterButton.gameObject.SetActive(!allChosen);
            previousCharacterButton.gameObject.SetActive(!allChosen);
        }

        private void LoadDataToGameManager()
        {
            GameManager.PlayerNumber = playersNumber;
            for(var i=0; i<playersNumber; i++)
                GameManager.Players.Add(new Player(i, chosenCharacters[i]));
        }

        public void OnStartContextPlayButtonClick()
        {
            ChangeContext(Contexts.PlayersContext);
        }
        
        public void OnPlayersContextNextButtonClick()
        {
            playersNumber = playerSwipe.currentOptionID+1;
            ChangeContext(Contexts.CharactersContext);
            chosenCharacters = new Character[playersNumber];
            takenCharacters = new bool[charactersNumber+1];
            chosenCharacterIndex = 0;
            nextPlayerButton.gameObject.SetActive(true);
            previousPlayerButton.gameObject.SetActive(true);
            startGameButton.gameObject.SetActive(false);
            ChangeCharacterBoxContext(false);
            UpdateCharactersContext();
        }

        public void OnCharactersContextNextButtonClick()
        {
            LoadDataToGameManager();
            SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        }

        public void OnNextCharacterButtonClick()
        {
            SwitchCharacter(SwitchDirection.Right);
        }

        public void OnPreviousCharacterButtonClick()
        {
            SwitchCharacter(SwitchDirection.Left);
        }

        public void OnNextPlayerButtonClick()
        {
            chosenCharacters[chosenPlayerIndex] = (Character)Enum.ToObject(typeof(Character), chosenCharacterIndex);
            takenCharacters[chosenCharacterIndex] = true;
            if (chosenPlayerIndex == playersNumber - 1 && IfAllCharactersChosen())
            {
                ChangeCharacterBoxContext(true);
            }
            else
            {
                chosenPlayerIndex = (chosenPlayerIndex + 1) % playersNumber;
                chosenCharacterIndex = 0;
            }
            UpdateCharactersContext();
        }

        public void OnPreviousPlayerButtonClick()
        {
            chosenCharacters[chosenPlayerIndex] = (Character)Enum.ToObject(typeof(Character), chosenCharacterIndex);
            takenCharacters[(int)chosenCharacters[chosenPlayerIndex]] = false;
            chosenPlayerIndex = chosenPlayerIndex == 0 ? playersNumber-1 : chosenPlayerIndex-1;
            chosenCharacterIndex = (int)chosenCharacters[chosenPlayerIndex];
            UpdateCharactersContext();
        }

        public void OnEditCharactersButtonClick()
        {
            ChangeCharacterBoxContext(false);
        }
    }
}
