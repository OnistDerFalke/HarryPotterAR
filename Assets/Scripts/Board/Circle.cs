using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : IFigure
{
    [SerializeField] private Vector2 center;
    [SerializeField] private float radius;

    public Vector2 Position { get => center; }
}
