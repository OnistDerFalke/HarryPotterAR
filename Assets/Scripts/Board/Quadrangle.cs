using UnityEngine;

namespace Assets.Scripts
{
    public class Quadrangle : IFigure
    {
        public Vector2 leftUpper;
        public Vector2 rightUpper;
        public Vector2 rightBottom;
        public Vector2 leftBottom;

        public Vector2 CenterPosition { get => (leftUpper + rightUpper + rightBottom + leftBottom) / 4; }

        public Quadrangle(Vector2 leftUpper, Vector2 rightUpper, Vector2 rightBottom, Vector2 leftBottom)
        {
            this.leftUpper = leftUpper;
            this.rightUpper = rightUpper;
            this.rightBottom = rightBottom;
            this.leftBottom = leftBottom;
        }

        private int CalculateWindingNumber(Vector2 point, Vector2 cornerA, Vector2 cornerB)
        {
            float crossProduct = (cornerB.x - cornerA.x) * (point.y - cornerA.y) - (cornerB.y - cornerA.y) * (point.x - cornerA.x);

            if (crossProduct > 0f)
                return 1;
            if (crossProduct < 0f)
                return -1;

            return 0;
        }

        public bool ContainsPosition(Vector2 pos)
        {
            int windingNumber = 0;

            windingNumber += CalculateWindingNumber(pos, leftUpper, rightUpper);
            windingNumber += CalculateWindingNumber(pos, rightUpper, rightBottom);
            windingNumber += CalculateWindingNumber(pos, rightBottom, leftBottom);
            windingNumber += CalculateWindingNumber(pos, leftBottom, leftUpper);

            return windingNumber != 0;
        }
    }
}