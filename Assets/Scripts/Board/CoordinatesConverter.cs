using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CoordinatesConverter : MonoBehaviour
    {
        public GameObject BottomLeft;
        public GameObject UpperLeft;
        public GameObject BottomRight;
        public GameObject UpperRight;
        public float boardSize = 25f;
        public float heightOffset = 3f;


        // marks that identify the position and orientation of a board, sticked to the board
        [SerializeField] private List<Vector2> boardMarkPositions;
        [SerializeField] private MultiVuMarkHandler vuMarkHandler;

        private Dictionary<string, Vector2> boardMarks;
        private Transform referenceTransform;
        private string referenceMarkerId;

        public Vector2 GetPointPosition_World2D(Vector2 point)
        {
            return V3toV2(referenceTransform.position) +
                (point.x - boardMarks[referenceMarkerId].x) * V3toV2(referenceTransform.right) +
                (point.y - boardMarks[referenceMarkerId].y) * V3toV2(referenceTransform.forward);
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
                Debug.Log("tracking something!");
                string id = vuMarkHandler.CurrentTrackedObjects[0];
                if (id != referenceMarkerId)
                {
                    referenceMarkerId = id;
                    GameObject marker = vuMarkHandler.FindModelById(id);
                    if (marker != null)
                    {
                        referenceTransform = marker.transform;
                    }
                }
            }

            if(referenceTransform!=null)
            {
                BottomLeft.transform.position = V2toV3(GetPointPosition_World2D(Vector2.zero), heightOffset);
                BottomRight.transform.position = V2toV3(GetPointPosition_World2D(Vector2.right * boardSize), heightOffset);
                UpperLeft.transform.position = V2toV3(GetPointPosition_World2D(Vector2.up * boardSize), heightOffset);
                UpperRight.transform.position = V2toV3(GetPointPosition_World2D(Vector2.one * boardSize), heightOffset);
            }
        }
    }
}