using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject[] menuContext = new GameObject[3];
        [SerializeField] private PlayersSwipe playerSwipe;
        [SerializeField] private Button nextPlayerButton;
        [SerializeField] private Button previousPlayerButton;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button nextCharacterButton;
        [SerializeField] private Button previousCharacterButton;
        
        [SerializeField] private Sprite[] vuMarksSprites = new Sprite[9];
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
        private Characters[] chosenCharacters;
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
            return chosenCharacters.All(character => character != Characters.None);
        }
        
        private void CheckButtonsActivity()
        {
            nextPlayerButton.interactable = chosenCharacterIndex != 0;
            previousPlayerButton.interactable = chosenCharacterIndex != 0;
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
            playerName.text = $"Gracz{chosenPlayerIndex + 1}";
            characterName.text = chosenCharacterIndex == 0 ? 
                "Nie wybrano" : ((Characters)Enum.ToObject(typeof(Characters), chosenCharacterIndex)).ToString();
            if(chosenCharacterIndex < charactersNumber)
                vuMarkImage.sprite = vuMarksSprites[chosenCharacterIndex];
        }

        private void ChangeContext(Contexts context)
        {
            foreach (var c in menuContext)
                c.SetActive(false);
            menuContext[(int)context].SetActive(true);
        }

        public void OnStartContextPlayButtonClick()
        {
            ChangeContext(Contexts.PlayersContext);
        }
        
        public void OnPlayersContextNextButtonClick()
        {
            playersNumber = playerSwipe.currentOptionID+1;
            ChangeContext(Contexts.CharactersContext);
            chosenCharacters = new Characters[playersNumber];
            takenCharacters = new bool[charactersNumber+1];
            chosenCharacterIndex = 0;
            nextPlayerButton.gameObject.SetActive(true);
            previousPlayerButton.gameObject.SetActive(true);
            startGameButton.gameObject.SetActive(false);
            UpdateCharactersContext();
        }

        public void OnCharactersContextNextButtonClick()
        {
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
            chosenCharacters[chosenPlayerIndex] = (Characters)Enum.ToObject(typeof(Characters), chosenCharacterIndex);
            takenCharacters[chosenCharacterIndex] = true;
            if (chosenPlayerIndex == playersNumber - 1 && IfAllCharactersChosen())
            {
                nextPlayerButton.gameObject.SetActive(false);
                previousPlayerButton.gameObject.SetActive(false);
                startGameButton.gameObject.SetActive(true);
                nextCharacterButton.gameObject.SetActive(false);
                previousCharacterButton.gameObject.SetActive(false);
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
            chosenCharacters[chosenPlayerIndex] = (Characters)Enum.ToObject(typeof(Characters), chosenCharacterIndex);
            takenCharacters[(int)chosenCharacters[chosenPlayerIndex]] = false;
            chosenPlayerIndex = chosenPlayerIndex == 0 ? playersNumber-1 : chosenPlayerIndex-1;
            chosenCharacterIndex = (int)chosenCharacters[chosenPlayerIndex];
            UpdateCharactersContext();
        }
    }
}
