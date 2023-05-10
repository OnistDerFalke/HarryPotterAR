using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Board
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private int boardId;
        [SerializeField] private IFigure figure;
        [SerializeField] private int index;
        [SerializeField] private bool isTower;
        [SerializeField] private bool isQuidditchPitch;
        [SerializeField] private List<Field> neighbors;
        [SerializeField] private Field portalField;
        [SerializeField] private GameObject highlight;

        public int BoardId { get => boardId; }
        public IFigure Figure { get => figure; }
        public int Index { get => index; }
        public bool IsTower { get => isTower; }
        public bool IsQuidditchPitch { get => isQuidditchPitch; }
        public List<Field> Neighbors { get => neighbors; }
        public Field PortalField { get => portalField; }
        public Vector2 Position2D { get => figure.CenterPosition; }

        public Field(int boardId, IFigure figure, int index, bool isTower = false, bool isQuidditchPitch = false)
        {
            this.boardId = boardId;
            this.figure = figure;
            this.index = index;
            this.isTower = isTower;
            this.isTower = isQuidditchPitch;
            this.portalField = null;
            this.neighbors = new List<Field>();
        }

        public void AddNeighbors(List<Field> neighbors)
        {
            this.neighbors.AddRange(neighbors);
        }

        public void SetPortalField(Field portalField)
        {
            this.portalField = portalField;
        }

        public void Highlight()
        {
            highlight.SetActive(true);
        }

        public void Unhighlight()
        {
            highlight.SetActive(false);
        }

        public void ToggleHighlights()
        {
            highlight.SetActive(!highlight.activeInHierarchy);
        }
    }
}
