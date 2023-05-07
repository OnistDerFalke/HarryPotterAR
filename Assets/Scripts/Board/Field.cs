using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private int boardId;
    [SerializeField] private IFigure figure;
    [SerializeField] private int index;
    [SerializeField] private bool isTower;
    [SerializeField] private List<Field> neighbours;
    [SerializeField] private Field portalField;
    [SerializeField] private GameObject highlight;


    public int BoardId { get => boardId; }
    public IFigure Figure { get => figure; }
    public int Index { get => index; }
    public bool IsTower { get => isTower; }
    public List<Field> Neighbours { get => neighbours; }
    public Field PortalField { get => portalField; }
    public Vector2 Position2D { get => figure.Position; }

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
