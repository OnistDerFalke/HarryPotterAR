using System.Collections;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUIController : MonoBehaviour
    { 
        [Header("· Buttons ·")]
        [SerializeField] private Button diceThrowButton;
        [SerializeField] private Text diceThrowButtonText;
        
        //(0, 1)->(left dice, right dice)
        [Space(2)] [Header("· Dices properties ·")]
        [SerializeField] private Image[] dicesGraphics = new Image[2];
        [SerializeField] private GameObject[] dicesGm = new GameObject[2];
        [SerializeField] private Sprite[] dicesGraphicsOptions = new Sprite[6];

        [Space(2)] [Header("· Simulation properties ·")]
        [SerializeField] private float rotationSpeedFactor;
        [SerializeField] private float simulationTime;
        [SerializeField] private float simulationSmooth;
        private int diceResult;
        void Start()
        {
            UpdateContent();
        }

        void Update()
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            if (GameManager.GetMyPlayer().IsDuringMove)
            {
                diceThrowButton.interactable = true;
                diceThrowButtonText.color = Color.white;
                diceThrowButtonText.text = "Zakończ turę";
            }
            else if (GameManager.GetMyPlayer().LastFieldId < 0)
            {
                diceThrowButton.interactable = false;
                diceThrowButtonText.color = new Color(0.345f, 0.212f, 0.208f);
                diceThrowButtonText.text = "Zczytaj pozycję gracza";
            }
            else
            {
                diceThrowButton.interactable = true;
                diceThrowButtonText.color = Color.white;
                diceThrowButtonText.text = "Rzuć kością";
            }

            //dices unseen until simulation changes it
            if (!GameManager.GetMyPlayer().IsDuringMove)
                foreach (var dice in dicesGm)
                    dice.transform.localScale = Vector3.zero;
        }

        private IEnumerator DiceThrowSimulation(float time, float smooth)
        {
            //setting up dices
            var leftDiceResult = Random.Range(1, 7);
            var rightDiceResult = Random.Range(1, 7);
            dicesGraphics[0].sprite = dicesGraphicsOptions[leftDiceResult - 1];
            dicesGraphics[1].sprite = dicesGraphicsOptions[rightDiceResult - 1];
            diceResult = leftDiceResult + rightDiceResult;
            GameManager.CurrentDiceThrownNumber = diceResult;
            
            //calcutating simulation factors
            float currentScale = 0;
            var scalingSpeed = 1 / time;

            diceThrowButton.interactable = false;
            
            //simulation
            while (currentScale < 1)
            {
                dicesGm[0].transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                dicesGm[1].transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                dicesGm[0].transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeedFactor);
                dicesGm[1].transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeedFactor);
                currentScale += scalingSpeed * Time.deltaTime;
                yield return new WaitForSeconds(1/smooth);
            }

            diceThrowButton.interactable = true;
            UpdateContent();
        }

        public void OnDiceThrowButtonClick()
        {
            if (GameManager.GetMyPlayer().IsDuringMove)
            {
                GameManager.GetMyPlayer().IsDuringMove = false;
                GameManager.GetMyPlayer().LastFieldId = -1;
                UpdateContent();
                GameManager.BoardManager.UnhighlightAllFields();
            }
            else
            {
                StartCoroutine(DiceThrowSimulation(simulationTime, simulationSmooth));
                GameManager.GetMyPlayer().IsDuringMove = true;
                GameManager.BoardManager.ShowPossibleMoves();
            }
        }
    }
}
