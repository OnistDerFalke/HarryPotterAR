using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CoordinatesConverter : MonoBehaviour
    {
        public float scale;
        public float heightOffset;


        // marks that identify the position and orientation of a board, sticked to the board
        [SerializeField] private List<Vector2> boardMarkPositions;
        [SerializeField] private MultiVuMarkHandler vuMarkHandler;

        private Dictionary<string, Vector2> boardMarks;
        private (string id, Transform transform) referenceMarker;

        /// <summary>
        /// Use this method to make sure that the result of converting coordinates is valid
        /// </summary>
        /// <returns>true if at least one board marker is tracked, false otherwise</returns>
        public bool IsTrackingBoard()
        {
            return referenceMarker.transform != null;
        }

        /// <summary>
        /// Use this method to obtain scene coordinates of a point on the board plane
        /// </summary>
        /// <param name="boardCoordinates">the point to map to world space</param>
        /// <returns>scene coordinates of a given point or (0,0,0) if the board is not tracked</returns>
        public Vector3 ConvertCoordinates(Vector2 boardCoordinates)
        {
            if (IsTrackingBoard())
            {
                return V2toV3(GetPointPosition_World2D(boardCoordinates, referenceMarker) * scale, heightOffset);
            }
            else
            {
                return Vector3.zero;
            }
        }

        public Vector3 ConvertCoordinates(Vector2 boardCoordinates, string referenceMarkerId)
        {
            GameObject marker = vuMarkHandler.FindModelById(referenceMarkerId);
            (string, Transform) referenceMarker = (referenceMarkerId, marker.transform);
            return V2toV3(GetPointPosition_World2D(boardCoordinates, referenceMarker) * scale, heightOffset);
        }

        private Vector2 GetPointPosition_World2D(Vector2 point, (string id, Transform transform) referenceMarker)
        {
            return V3toV2(referenceMarker.transform.position) +
                (point.x - boardMarks[referenceMarker.id].x) * V3toV2(referenceMarker.transform.right) +
                (point.y - boardMarks[referenceMarker.id].y) * V3toV2(referenceMarker.transform.forward);
        }

        private Vector2 V3toV2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        private Vector3 V2toV3(Vector2 v2, float height = 0f)
        {
            return new Vector3(v2.x, height, v2.y);
        }

        private float CalculateErrorRate(string markerId)
        {
            float err = 0f;
            foreach(string otherId in vuMarkHandler.CurrentTrackedObjects)
            {
                if(otherId == markerId)
                {
                    continue;
                }
                Vector3 expectedPosition = ConvertCoordinates(boardMarks[otherId], markerId);
                Vector3 actualPosition = vuMarkHandler.FindModelById(otherId).transform.position;
                err += Vector3.SqrMagnitude(expectedPosition - actualPosition);
            }
            return err;
        }

        private string ChooseReferenceMarker()
        {
            if (vuMarkHandler.CurrentTrackedObjects.Count == 0)
            {
                return null;
            }
            else
            {
                string bestId = null;
                float minErrorRate = float.MaxValue;

                foreach(string markerId in vuMarkHandler.CurrentTrackedObjects)
                {
                    float err = CalculateErrorRate(markerId);
                    if(err < minErrorRate)
                    {
                        minErrorRate = err;
                        bestId = markerId;
                    }
                }
                return bestId;
            }
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
                string id = ChooseReferenceMarker();
                if (id != referenceMarker.id)
                {
                    referenceMarker.id = id;
                    GameObject marker = vuMarkHandler.FindModelById(id);
                    if (marker != null)
                    {
                        referenceMarker.transform = marker.transform;
                    }
                }
            }
            else
            {
                referenceMarker = (null, null);
            }
        }
    }
}