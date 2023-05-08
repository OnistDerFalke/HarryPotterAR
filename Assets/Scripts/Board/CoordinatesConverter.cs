using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesConverter : MonoBehaviour
{
    // marks that identify the position and orientation of a board, sticked to the board
    [SerializeField] private List<Vector2> boardMarks;

    private float scale;
    private Vector2 boardDirX;
    private Vector2 boardDirY;
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

        // A_world + scale * ( xdir * (B_board_x - A_board_x) + ydir * (B_board_y - A_board_y)) = B_world
        //  A_world_x + scale * ( xdir_x * (B_board_x - A_board_x) + ydir_x * (B_board_y - A_board_y)) = B_world_x
        //  A_world_y + scale * ( xdir_y * (B_board_x - A_board_x) + ydir_y * (B_board_y - A_board_y)) = B_world_y
        // A_world + scale * ( xdir * (C_board_x - A_board_x) + ydir * (C_board_y - A_board_y)) = C_world
        //  A_world_x + scale * ( xdir_x * (C_board_x - A_board_x) + ydir_x * (C_board_y - A_board_y)) = C_world_x
        //  A_world_y + scale * ( xdir_y * (C_board_x - A_board_x) + ydir_y * (C_board_y - A_board_y)) = C_world_y

        // xdir_x * (B_board_x - A_board_x) + ydir_x * (B_board_y - A_board_y) = (B_world_x - A_world_x) / scale
        // xdir_y * (B_board_x - A_board_x) + ydir_y * (B_board_y - A_board_y) = (B_world_y - A_world_x) / scale
        // xdir_x * (C_board_x - A_board_x) + ydir_x * (C_board_y - A_board_y) = (C_world_x - A_world_x) / scale
        // xdir_y * (C_board_x - A_board_x) + ydir_y * (C_board_y - A_board_y) = (C_world_y - A_world_x) / scale

        // xdir_x = ((B_world_x - A_world_x) / scale - ydir_x * (B_board_y - A_board_y)) / (B_board_x - A_board_x)
        // xdir_y = ((B_world_y - A_world_x) / scale - ydir_y * (B_board_y - A_board_y)) / (B_board_x - A_board_x)
        // ydir_x = ((C_world_x - A_world_x) / scale - xdir_x * (C_board_x - A_board_x)) / (C_board_y - A_board_y)
        // ydir_y = ((C_world_y - A_world_x) / scale - xdir_y * (C_board_x - A_board_x)) / (C_board_y - A_board_y)

        // xdir_x = ((B_world_x - A_world_x) / scale - ydir_x * (B_board_y - A_board_y)) / (B_board_x - A_board_x)
        // ydir_x = ((C_world_x - A_world_x) / scale - ((B_world_x - A_world_x) / scale - ydir_x * (B_board_y - A_board_y)) / (B_board_x - A_board_x) * (C_board_x - A_board_x)) / (C_board_y - A_board_y)

        // xdir_y = ((B_world_y - A_world_x) / scale - ydir_y * (B_board_y - A_board_y)) / (B_board_x - A_board_x)
        // ydir_y = ((C_world_y - A_world_x) / scale - ((B_world_y - A_world_x) / scale - ydir_y * (B_board_y - A_board_y)) / (B_board_x - A_board_x) * (C_board_x - A_board_x)) / (C_board_y - A_board_y)

        // ydir_x = ((C_world_x - A_world_x) / scale - ((B_world_x - A_world_x) / scale - ydir_x * (B_board_y - A_board_y)) / (B_board_x - A_board_x) * (C_board_x - A_board_x)) / (C_board_y - A_board_y)
        // ... some transformations written in paper
        // E = (B_board_x - A_board_x) * (C_board_x - A_board_x)
        // ydir_x = (E * (C_world_x - A_world_x) - B_world_x + A_world_x) / (E * (C_board_y - A_board_y) - B_board_y + A_board_y)
        // and by analogy:
        // ydir_y = (E * (C_world_y - A_world_x) - B_world_y + A_world_x) / (E * (C_board_y - A_board_y) - B_board_y + A_board_y)

        (Vector2 board, Vector2 world) a = markers[0];
        (Vector2 board, Vector2 world) b = markers[1];
        (Vector2 board, Vector2 world) c = markers[2];

        // czy wolimy na odwrót @OnistDerFalke?
        Debug.Log($"bw {b.world}, aw {a.world}, bb {b.board}, ab {a.board}");
        scale = Vector2.Distance(b.world, a.world) / Vector2.Distance(b.board, a.board);
        Debug.Log($"scale = {scale}");
        float e = (b.board.x - a.board.x) * (c.board.x - a.board.x); // zeruje się
        float denominator = e * (c.board.y - a.board.y) - b.board.y + a.board.y;
        float ydir_x = (e * (c.world.x - a.world.x) - b.world.x + a.world.x) / denominator; // should be 0
        float ydir_y = (e * (c.world.y - a.world.x) - b.world.y + a.world.x) / denominator; // should be 1
        float f = (b.world.x - a.world.x) / scale;
        float dx = b.board.x - a.board.x;
        float dy = b.board.y - a.board.y;
        float xdir_x = (f - ydir_x * dy) / dx;
        float xdir_y = (f - ydir_y * dy) / dx;
        boardDirX = new Vector2(xdir_x, xdir_y).normalized;
        boardDirY = new Vector2(ydir_x, ydir_y).normalized;
        boardOrigin = a.world - a.board.x * scale * boardDirX - a.board.y * scale * boardDirY;
    }

    public Vector2 GetFieldPosition_World2D(Field field)
    {
        return GetPointPosition_World2D(field.Position2D);
    }

    public Vector3 GetFieldPosition_World3D(Field field)
    {
        return GetPointPosition_World3D(field.Position2D);
    }

    public Vector2 GetPointPosition_World2D(Vector2 point)
    {
        Debug.Log($"origin: {BoardOrigin2D}");
        Debug.Log($"directions: {boardDirX}, {boardDirY}");
        return BoardOrigin2D + point.x * scale * boardDirX + boardDirY * point.y;
    }

    public Vector3 GetPointPosition_World3D(Vector2 point)
    {
        return BoardOrigin3D + point.x * scale * FromV2(boardDirX) + FromV2(boardDirY) * point.y;
    }

    private void Awake()
    {
        boardDirX = Vector2.right;
        boardDirY = Vector2.up;
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
