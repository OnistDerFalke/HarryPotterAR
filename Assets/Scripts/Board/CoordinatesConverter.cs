using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesConverter : MonoBehaviour
{
    public GameObject BottomLeft;
    public GameObject UpperLeft;
    public GameObject BottomRight;
    public GameObject UpperRight;
    public float boardSize = 25f;


    // marks that identify the position and orientation of a board, sticked to the board
    [SerializeField] private List<Vector2> boardMarkPositions;
    [SerializeField] private MultiVuMarkHandler vuMarkHandler;

    private Dictionary<string, Vector2> boardMarks;
    private Transform referenceTransform;
    private string referenceMarkerId;


    //private float scale;
    //private float boardRotationZ;
    //private Vector2 boardOrigin;


    //public void EstimateWorldPosition()
    //{
    //    if(vuMarkHandler.CurrentTrackedObjects.Count >= 2)
    //    {
    //        string id1 = vuMarkHandler.CurrentTrackedObjects[0];
    //        string id2 = vuMarkHandler.CurrentTrackedObjects[1];
    //        GameObject g1 = vuMarkHandler.FindModelById(id1);
    //        GameObject g2 = vuMarkHandler.FindModelById(id2);

    //        if(g1 != null && g2 != null)
    //        {
    //            float worldDist = Vector2.Distance(V3toV2(g2.transform.position), V3toV2(g1.transform.position));
    //            float boardDist = Vector2.Distance(boardMarks[id1], boardMarks[id2]);
    //            scale =  worldDist / boardDist;
    //        }
    //    }

    //    if(vuMarkHandler.CurrentTrackedObjects.Count > 0)
    //    {
    //        string id = vuMarkHandler.CurrentTrackedObjects[0];
    //        GameObject marker = vuMarkHandler.FindModelById(id);
    //        if(marker != null)
    //        {
    //            Transform t = marker.transform;
    //            boardRotationZ = t.rotation.eulerAngles.y;

    //        }
    //    }
    //}

    //public Vector2 GetFieldPosition_World2D(Field field)
    //{
    //    return GetPointPosition_World2D(field.Position2D);
    //}

    //private Vector2 RotatePoint(Vector2 point, float rotation)
    //{
    //    // x' = x*cos(a) - y*sin(a)
    //    // y' = y*cos(a) + x*sin(a)
    //    float x1_rotated = point.x * Mathf.Cos(rotation) - point.y * Mathf.Sin(rotation);
    //    float y1_rotated = point.y * Mathf.Cos(rotation) + point.x * Mathf.Sin(rotation);
    //    return new Vector2(x1_rotated, y1_rotated);
    //}

    //public Vector2 GetPointPosition_World2D(Vector2 point)
    //{
    //    return boardOrigin + RotatePoint(point, boardRotationZ) * scale;
    //}

    public Vector2 GetPointPosition_World2D(Vector2 point)
    {
        return V3toV2(referenceTransform.position) + 
            (point.x - boardMarks[referenceMarkerId].x) * V3toV2(referenceTransform.right) +
            (point.y - boardMarks[referenceMarkerId].y) * V3toV2(referenceTransform.up);
    }

    public Vector2 V3toV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public Vector3 V2toV3(Vector2 v2, float height = 0f)
    {
        return new Vector3(v2.x, height, v2.y);
    }

    private void Awake()
    {
        boardMarks = new Dictionary<string, Vector2>();
        for (int i = 0; i < boardMarkPositions.Count; i++)
        {
            boardMarks[vuMarkHandler.availableIds[i]] = boardMarkPositions[i];
        }
    }

    private void Update()
    {
        if (vuMarkHandler.CurrentTrackedObjects.Count > 0)
        {
            string id = vuMarkHandler.CurrentTrackedObjects[0];
            if(id!=referenceMarkerId)
            {
                referenceMarkerId = id;
                GameObject marker = vuMarkHandler.FindModelById(id);
                if (marker != null)
                {
                    referenceTransform = marker.transform;
                }
            }
        }

        BottomLeft.transform.position = V2toV3(GetPointPosition_World2D(Vector2.zero));
        BottomRight.transform.position = V2toV3(GetPointPosition_World2D(Vector2.right * boardSize));
        UpperLeft.transform.position = V2toV3(GetPointPosition_World2D(Vector2.up * boardSize));
        UpperRight.transform.position = V2toV3(GetPointPosition_World2D(Vector2.one * boardSize));
    }
}
