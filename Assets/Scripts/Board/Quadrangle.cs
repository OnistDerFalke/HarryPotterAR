using UnityEngine;

namespace Assets.Scripts
{
    public class Quadrangle : IFigure
    {
        [SerializeField] private Vector2 leftUpper;
        [SerializeField] private Vector2 rightUpper;
        [SerializeField] private Vector2 rightBottom;
        [SerializeField] private Vector2 leftBottom;

        public Vector2 CenterPosition { get => (leftUpper + rightUpper + rightBottom + leftBottom) / 4; }

        public Quadrangle(Vector2 leftUpper, Vector2 rightUpper, Vector2 rightBottom, Vector2 leftBottom)
        {
            this.leftUpper = leftUpper;
            this.rightUpper = rightUpper;
            this.rightBottom = rightBottom;
            this.leftBottom = leftBottom;
        }
    }
}