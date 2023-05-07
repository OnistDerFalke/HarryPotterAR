using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : IFigure
{
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 rect;

    public Vector2 Position { get => center; }
}
