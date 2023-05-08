using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : IFigure
{
    [SerializeField] private Vector2 rect;
    [SerializeField] private Vector2 leftUpper;
    [SerializeField] private Vector2 rightBottom;


    public Vector2 Position { get => (leftUpper + rightBottom)/2; }
}
