using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesConverter : MonoBehaviour
{
    // marks that identify the position and orientation of a board, sticked to the board
    [SerializeField] private List<Vector2> boardMarks;

    private float scale;
    private float rotation;
    private Vector2 boardOrigin;
    private float originHeight;

    public Vector2 BoardOrigin2D { get => boardOrigin; }
    public Vector3 BoardOrigin3D { get => FromV2(BoardOrigin2D, originHeight); }

    private Vector3 FromV2 (Vector2 v, float height = 0f)
    {
        return new Vector3(v.x, height, v.y);
    }

    private List<(int, Vector3)> ReadMarksPositions()
    {
        // TODO
        return new List<(int, Vector3)> { (0, Vector3.zero), (1, Vector3.left), (2, Vector3.forward) };
    }

    public void EstimateWorldPosition(List<(int i, Vector3 pos)> marksInSight)
    {
        // y position will be ignored
        originHeight = marksInSight[0].pos.y;
        List<(Vector2 board, Vector2 world)> markers = new List<(Vector2 board, Vector2 world)>();
        foreach(var v in marksInSight)
        {
            markers.Add((boardMarks[v.i], new Vector2(v.pos.x, v.pos.z)));
        }
        (Vector2 board, Vector2 world) a = markers[0];
        (Vector2 board, Vector2 world) b = markers[1];

        scale = Vector2.Distance(b.world, a.world) / Vector2.Distance(b.board, a.board);


        Vector2 ab_dir_board = (b.board - a.board).normalized;
        Vector2 ab_dir_world = (b.world - a.world).normalized;

        float board_angle = Mathf.Atan2(ab_dir_board.y, ab_dir_board.x);
        float world_angle = Mathf.Atan2(ab_dir_world.y, ab_dir_world.x);

        rotation = world_angle - board_angle;

        float a_board_point_angle = Mathf.Atan2(a.board.y, a.board.x);
        boardOrigin = a.world - RotatePoint(a.board, Mathf.PI + a_board_point_angle + rotation) * scale;
    }

    public Vector2 GetFieldPosition_World2D(Field field)
    {
        return GetPointPosition_World2D(field.Position2D);
    }

    public Vector3 GetFieldPosition_World3D(Field field)
    {
        return GetPointPosition_World3D(field.Position2D);
    }

    private Vector2 RotatePoint(Vector2 point, float rotation)
    {
        // x' = x*cos(a) - y*sin(a)
        // y' = y*cos(a) + x*sin(a)
        float x1_rotated = point.x * Mathf.Cos(rotation) - point.y * Mathf.Sin(rotation);
        float y1_rotated = point.y * Mathf.Cos(rotation) + point.x * Mathf.Sin(rotation);
        return new Vector2(x1_rotated, y1_rotated);
    }

    public Vector2 GetPointPosition_World2D(Vector2 point)
    {
        return BoardOrigin2D + RotatePoint(point, rotation) * scale;
    }

    public Vector3 GetPointPosition_World3D(Vector2 point)
    {
        return BoardOrigin3D + FromV2(RotatePoint(point, rotation) * scale);
    }

    private void Test()
    {
        // TEST
        boardMarks = new List<Vector2>() { new Vector2(1, 1), new Vector2(2, 1), new Vector2(1, 2) };

        List<(int, Vector3)> marksInSight =
            new List<(int, Vector3)> {
                (0, new Vector3(2.01f, 9.0f, 2.02f)),
                (1, new Vector3(3.01f, 9.0f, 2.03f)),
                (2, new Vector3(2.00f, 9.0f, 3.00f)) };
        EstimateWorldPosition(marksInSight);

        Debug.Log(GetPointPosition_World2D(Vector2.zero) + " assert (1,1)");
        Debug.Log(GetPointPosition_World3D(Vector2.zero) + " assert (1,9,1)");
        Debug.Log(GetPointPosition_World3D(Vector2.one) + " assert (2,9,2)");
    }

    private void Start()
    {
        Test();
    }

    private void Update()
    {
        //List<(int, Vector3)> marksInSight = ReadMarksPositions();
        //if (marksInSight.Count >= 3)
        //{
        //    EstimateWorldPosition(marksInSight);
        //}
    }
}
