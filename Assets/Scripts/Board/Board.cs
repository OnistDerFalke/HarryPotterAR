using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Field> fields;

    // marks that identify the position and orientation of a board, sticked to the board
    [SerializeField] private List<Vector2> boardMarks;

    public List<Field> Fields { get => fields; }

    private Vector3 boardOrigin; // world position of the left bottom corner of the board
    private Vector3 boardXdir; // normalized vector (1,0) mapped to world coordinates
    private Vector3 boardYdir; // normalized vector (0,1) mapped to world coordinates


    /// <summary>
    /// A function that reads the 3d positions of some of the boardMarks
    /// returns a list of those positions paired with boardMark indices from boardMarks list
    /// </summary>
    /// <returns></returns>
    private List<(int, Vector3)> ReadMarksPositions()
    {
        // TODO
        return new List<(int, Vector3)> { (0, Vector3.zero), (1, Vector3.left), (2, Vector3.forward)};
    }

    public void EstimateWorldPosition(List<(int i, Vector3 pos)> marksInSight)
    {
        Vector3 dir1 = (marksInSight[1].pos - marksInSight[0].pos).normalized;
        Vector3 dir2 = (marksInSight[2].pos - marksInSight[0].pos).normalized;

        Vector2 dir12d = (boardMarks[marksInSight[1].i] - boardMarks[marksInSight[0].i]).normalized;
        Vector2 dir22d = (boardMarks[marksInSight[2].i] - boardMarks[marksInSight[0].i]).normalized;

        // 

        Vector2 mark0board = boardMarks[0];
        boardOrigin = marksInSight[0].pos - boardXdir * mark0board.x - boardYdir * mark0board.y;
    }

    public Vector3 GetFieldPosition3D(Field field)
    {
        Vector2 fieldPos2D = field.Position2D;
        return boardOrigin + boardXdir * fieldPos2D.x + boardYdir * fieldPos2D.y;
    }

    void Update()
    {
        List<(int, Vector3)> marksInSight = ReadMarksPositions();
        if (marksInSight.Count >= 3)
        {
            EstimateWorldPosition(marksInSight);
        }
    }
}
