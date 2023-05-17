using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts
{
    public class BoardManager
    {
        [SerializeField] 
        private List<Board> boards = new List<Board>() {
            new Board(0, 50.0f, 50.0f),
            new Board(1, 25.0f, 25.0f),
            new Board(2, 25.0f, 25.0f)
        };
        [SerializeField] private List<Field> allFields = new();

        public List<Board> Boards { get => boards; }
        public List<Field> AllFields { get => allFields; }

        public BoardManager()
        {
            BoardInitializator.InitBoard();
            this.allFields.AddRange(boards[0].Fields);
            this.allFields.AddRange(boards[1].Fields);
            this.allFields.AddRange(boards[2].Fields);
        }

        /// <summary>
        /// Player can end his move on fields:
        /// - that are towers (he can go to the board id = 2)
        /// - that are mission fields
        /// - according to the dice thrown number
        /// Player can't appear on the same field twice during one move
        /// </summary>
        /// <returns></returns>
        public void ShowPossibleMoves()
        {
            Field myField = this.allFields.Where(field => field.Index == GameManager.MyPlayer.LastFieldId).FirstOrDefault();

            myField.Neighbors.ForEach(delegate (Field neighbor)
            {
                FindFieldsToHighlightRecursive(new List<Field>() { myField, neighbor }, 1);
            });
        } 

        public void UnhighlightAllFields()
        {
            boards[0].Fields.ForEach(delegate(Field field)
            {
                field.Unhighlight();
            });
            boards[1].Fields.ForEach(delegate (Field field)
            {
                field.Unhighlight();
            });
            boards[2].Fields.ForEach(delegate (Field field)
            {
                field.Unhighlight();
            });
        }

        private void FindFieldsToHighlightRecursive(List<Field> path, int steps)
        {
            Field currentField = path.Last();
            bool fiuuFieldsHighlighted = false;

            if (steps == GameManager.CurrentDiceThrownNumber)
            {
                currentField.Highlight();
            }
            else if (steps < GameManager.CurrentDiceThrownNumber)
            {
                if (currentField.IsTower)
                {
                    currentField.Highlight();
                    if (fiuuFieldsHighlighted)
                    {
                        boards[2].Fields.Where(field => field.IsFiuuField).ToList().ForEach(delegate (Field fiuuField)
                        {
                            fiuuField.Highlight();
                        });
                        fiuuFieldsHighlighted = true;
                    }
                }
                else if (currentField.IsMissionField)
                {
                    currentField.Highlight(true);
                }

                // go to neighbors of current field
                currentField.Neighbors.ForEach(delegate (Field neighbor)
                {
                    if (!path.Contains(neighbor))
                    {
                        path.Add(neighbor);
                        FindFieldsToHighlightRecursive(path, steps+1);
                        path.Remove(neighbor);
                    }
                });

                // teleport by portalField to portalField
                if (currentField.PortalField != null)
                {
                    path.Add(currentField.PortalField);
                    FindFieldsToHighlightRecursive(path, steps);
                }
            }
        }
    }
}
