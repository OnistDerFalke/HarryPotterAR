using UnityEngine;

namespace Assets.Scripts.Board
{
    public interface IFigure
    {
        Vector2 CenterPosition { get; }
    }
}