using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class GameUIController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button diceThrowButton;
        [SerializeField] private Text diceThrowButtonText;

        [Header("Position Info properties")]
        [SerializeField] private Button infoButton;
        [SerializeField] private GameObject currentFieldBox;
        [SerializeField] private Text currentFieldText;

        [Header("Action Info properties")]
        [SerializeField] private Button actionInfoButton;
        [SerializeField] private GameObject actionInfoBox;
        [SerializeField] private Text actionInfoText;

        [Header("Field Info properties")]
        [SerializeField] private GameObject fieldInfoBox;
        [SerializeField] private Text fieldInfoText;
        [SerializeField] private Text fieldNameText;

        [Header("Instruction properties")]
        [SerializeField] private GameObject instructionBox;
        [SerializeField] private Button instructionShowButton;
        [SerializeField] private Button nextInstructionButton;
        [SerializeField] private Button previousInstructionButton;
        [SerializeField] private Text instructionTitle;
        [SerializeField] private Text instructionText;

        //(0, 1)->(left dice, right dice)
        [Space(2)] [Header("Dices properties")]
        [SerializeField] private Image[] dicesGraphics = new Image[2];
        [SerializeField] private GameObject[] dicesGm = new GameObject[2];
        [SerializeField] private Sprite[] dicesGraphicsOptions = new Sprite[6];

        [Space(2)] [Header("Simulation properties")]
        [SerializeField] private float rotationSpeedFactor;
        [SerializeField] private float simulationTime;
        [SerializeField] private float simulationSmooth;

        private int diceResult;
        private bool currentFieldShow;
        private bool actionInfoShow;
        private bool fieldInfoShow;

        //instruction variables
        private bool instructionShow;
        private List<Instruction.InstructionInfo> instructionParts;
        private Instruction baseInstruction;
        private int currentInstructionPartId;
        private int instructionPartNumber;

        void Start()
        {
            currentFieldShow = true;
            actionInfoShow = false;
            fieldInfoShow = false;
            instructionShow = false;
            baseInstruction = new();
            UpdateContent();
        }

        void Update()
        {
            UpdateContent();
            CheckColliders();
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

            UpdateCurrentFieldContent();
            UpdateActionInfoBox();
            UpdateFieldInfoBox();
            UpdateInstructionBox();

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

        public void UpdateCurrentFieldContent()
        {
            currentFieldBox.SetActive(currentFieldShow);
            if (currentFieldShow)
                currentFieldText.text = GameManager.GetMyPlayer().GetCurrentFieldName();
        }

        public void UpdateActionInfoBox()
        {
            actionInfoBox.SetActive(actionInfoShow);
            if (actionInfoShow)
                actionInfoText.text = GameManager.GetMyPlayer().GetCurrentFieldActions();
        }

        public void UpdateInstructionBox()
        {
            instructionBox.SetActive(instructionShow);

            if (instructionShow)
            {
                var info = instructionParts[currentInstructionPartId];
                instructionTitle.text = baseInstruction.GetInstructionInfoString(info);
                instructionText.text = baseInstruction.instructions[info];
            }
        }

        public void UpdateFieldInfoBox()
        {
            fieldInfoBox.SetActive(fieldInfoShow);
        }

        public void CheckColliders()
        {
            Ray ray = new();
            bool read = false;

            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                read = true;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Input.GetTouch(0).position;
                ray = Camera.main.ScreenPointToRay(touchPosition);
                read = true;
            }

            if (!read) return;
            if (!Physics.Raycast(ray, out var hit)) return;

            if (hit.collider.CompareTag("FieldHighlight"))
            {
                fieldInfoBox.SetActive(true);
                int fieldId = hit.collider.GetComponent<InteractiveElement>().Index;
                var field = GameManager.BoardManager.GetFieldById(fieldId);
                fieldNameText.text = "Pole: " + field.Name;
                fieldInfoText.text = field.GetActionsShortInfo();
                fieldInfoShow = true;
            }
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
        
        public void OnShowCurrentFieldClick()
        {
            currentFieldShow = !currentFieldShow;
            UpdateCurrentFieldContent();
        }

        public void OnShowActionInfoClick()
        {
            actionInfoShow = !actionInfoShow;
            UpdateActionInfoBox();
        }

        public void OnCloseFieldInfoButton()
        {
            fieldInfoShow = false;
            UpdateFieldInfoBox();
        }

        public void OnShowInstructionButtonClick()
        {
            instructionShow = !instructionShow;
            if (instructionShow)
            {
                currentInstructionPartId = 0;
                instructionParts = GameManager.GetMyPlayer().GetCurrentInstructionParts();
                instructionPartNumber = instructionParts.Count;
            }
            UpdateInstructionBox();
        }

        public void OnNextInstructionButtonClick()
        {
            currentInstructionPartId = (currentInstructionPartId + 1) % instructionPartNumber;
            UpdateInstructionBox();
        }

        public void OnPreviousInstructionButtonClick()
        {
            currentInstructionPartId = (currentInstructionPartId - 1) < 0 ? instructionPartNumber - 1 : currentInstructionPartId - 1;
            UpdateInstructionBox();
        }
    }
}
