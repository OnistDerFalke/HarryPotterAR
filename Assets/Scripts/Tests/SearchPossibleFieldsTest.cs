using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class SearchPossibleFieldsTest : MonoBehaviour
    {
        private void PrintHighlightedFieldsId()
        {
            var log = "Current field: " + GameManager.GetMyPlayer().LastFieldId + "\tDice: " + GameManager.CurrentDiceThrownNumber + "\t\t";
            foreach (var field in GameManager.BoardManager.AllFields.Where(field => field.IsHighlighted)) 
            {
                log += field.Index + ", ";
            }
            Debug.Log(log);
        }

        private void Start()
        {
            GameManager.Setup();

            GameManager.PlayerNumber = 2;
            GameManager.Players.Add(new Player(0));
            GameManager.Players.Add(new Player(1));

            for (int i = 2; i <= 12; i++)
            {
                GameManager.CurrentDiceThrownNumber = i;
                GameManager.BoardManager.ShowPossibleMoves();
                PrintHighlightedFieldsId();
                GameManager.BoardManager.UnhighlightAllFields();
            }

            GameManager.GetMyPlayer().ChangeField(87);
            GameManager.CurrentDiceThrownNumber = 4;
            GameManager.BoardManager.ShowPossibleMoves();
            PrintHighlightedFieldsId();
            GameManager.BoardManager.UnhighlightAllFields();
        }
    }
}
