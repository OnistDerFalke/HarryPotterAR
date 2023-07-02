using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Field
    {
        [SerializeField] private int boardId;
        [SerializeField] private IFigure figure;
        [SerializeField] private int index;
        [SerializeField] private bool isTower;
        [SerializeField] private bool isQuidditchPitch;
        [SerializeField] private bool isMissionField;
        [SerializeField] private bool isFiuuField;
        [SerializeField] private bool isPortkeyField;
        [SerializeField] private List<Field> neighbors;
        [SerializeField] private Field portalField;

        // TODO: delete variable
        [SerializeField] private bool isHighlighted = false;

        public int BoardId { get => boardId; }
        public IFigure Figure { get => figure; }
        public int Index { get => index; }
        public bool IsTower { get => isTower; }
        public bool IsQuidditchPitch { get => isQuidditchPitch; }
        public bool IsMissionField { get => isMissionField; }
        public bool IsFiuuField { get => isFiuuField; }
        public List<Field> Neighbors { get => neighbors; }
        public Field PortalField { get => portalField; }
        public Vector2 Position2D { get => figure.CenterPosition; }
        public bool IsHighlighted { get => isHighlighted; }

        public Field(int boardId, IFigure figure, int index, 
            bool isTower=false, bool isMissionField=false, bool isFiuuField=false, bool isPortkeyField=false, bool isQuidditchPitch=false)
        {
            this.boardId = boardId;
            this.figure = figure;
            this.index = index;
            this.isTower = isTower;
            this.isMissionField = isMissionField;
            this.isFiuuField = isFiuuField;
            this.isPortkeyField = isPortkeyField;
            this.isQuidditchPitch = isQuidditchPitch;
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

        // TODO: Inaczej podkreœlaæ pola z trzeciej planszy, gdzie boardId = 2
        // Fajnie by by³o jakby mo¿na by³o przekazaæ jak¹œ wartoœæ, ¿e nale¿y podkreœliæ pole jako MissionField, 
        // czyli warunkowe - mo¿esz siê zatrzymaæ, jeœli masz misjê w danym miejscu i chcesz j¹ wykonaæ
        public void Highlight(bool missionLight=false)
        {
            Debug.Log($"field {index} with hash {this.GetHashCode()}, board {boardId}");
            if(!isHighlighted)
            {
                RequestBroker.requests.Add(new HighlightFieldRequest(this));
                isHighlighted = true;
            }
        }

        public void Unhighlight()
        {
            RequestBroker.requests.Add(new UnhighlightFieldRequest(this));
            isHighlighted = false;
        }
    }
}
