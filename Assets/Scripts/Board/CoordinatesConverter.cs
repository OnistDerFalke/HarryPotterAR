﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CoordinatesConverter : MonoBehaviour
    {
        // marks that identify the position and orientation of a board, sticked to the board
        [SerializeField] private List<Vector2> boardMarkPositions;
        [SerializeField] private List<string> boardMarkIds;
        [SerializeField] private MultiVuMarkHandler vuMarkHandler;

        private Dictionary<string, Vector2> boardMarks;
        private (string id, GameObject marker) referenceMarker;

        private List<string> currentTrackedBoardMarks;

        /// <summary>
        /// Use this method to make sure that the result of converting coordinates is valid
        /// </summary>
        /// <returns>true if at least one board marker is tracked, false otherwise</returns>
        public bool IsTrackingBoard()
        {
            return referenceMarker.marker != null;
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
                 return  referenceMarker.marker.transform.position +
                    (boardCoordinates.x - boardMarks[referenceMarker.id].x) * referenceMarker.marker.transform.right.normalized +
                    (boardCoordinates.y - boardMarks[referenceMarker.id].y) * referenceMarker.marker.transform.forward.normalized;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public Vector3 ConvertCoordinates(Vector2 boardCoordinates, string referenceMarkerId)
        {
            GameObject marker = vuMarkHandler.FindModelById(referenceMarkerId);
            (string id, GameObject marker) referenceMarker = (referenceMarkerId, marker);
            return referenceMarker.marker.transform.position +
                    (boardCoordinates.x - boardMarks[referenceMarker.id].x) * referenceMarker.marker.transform.right.normalized +
                    (boardCoordinates.y - boardMarks[referenceMarker.id].y) * referenceMarker.marker.transform.forward.normalized;
        }

        public Quaternion ReferenceRotation()
        {
            if(IsTrackingBoard())
            {
                return referenceMarker.marker.transform.rotation;
            }
            else
            {
                return Quaternion.identity;
            }
        }

        public Vector2 WorldToBoard(Vector3 worldPos)
        {
            if(referenceMarker.marker == null)
            {
                return Vector2.one * -1;
            }

            Vector3 normalizedDirection = -1 * referenceMarker.marker.transform.up.normalized;
            Vector3 lineToBoard = referenceMarker.marker.transform.position - worldPos;
            float projection = Vector3.Dot(lineToBoard, normalizedDirection);
            Vector3 intersection = worldPos + projection * normalizedDirection;

            Vector3 offset = intersection - referenceMarker.marker.transform.position;
            float y_dist_board = Vector3.Dot(offset, referenceMarker.marker.transform.forward);
            float x_dist_board = Vector3.Dot(offset, referenceMarker.marker.transform.right);
            Vector2 boardPos = boardMarks[referenceMarker.id] 
                - Vector2.right * x_dist_board
                - Vector2.up * y_dist_board;

            return boardPos;
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
            if (currentTrackedBoardMarks.Count == 0)
            {
                return null;
            }
            else
            {
                string bestId = null;
                float minErrorRate = float.MaxValue;

                foreach(string markerId in currentTrackedBoardMarks)
                {
                    float err = CalculateErrorRate(markerId);
                    if (err < minErrorRate)
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
            currentTrackedBoardMarks = new List<string>();
            for (int i = 0; i < boardMarkPositions.Count; i++)
            {
                boardMarks[boardMarkIds[i]] = boardMarkPositions[i];
            }
        }

        private void OnMarkerDetected(string id)
        {
            Debug.Log($"converter: {name} marker {id} detected");
        }

        private void OnMarkerLost(string id)
        {
            Debug.Log($"converter {name}: marker {id} lost");
        }

        private void OnEnable()
        {
            EventBroadcaster.OnMarkDetected += OnMarkerDetected;
            EventBroadcaster.OnMarkLost += OnMarkerLost;
        }

        private void OnDisable()
        {
            EventBroadcaster.OnMarkDetected -= OnMarkerDetected;
            EventBroadcaster.OnMarkLost -= OnMarkerLost;
        }

        private void Update()
        {
            currentTrackedBoardMarks = vuMarkHandler.CurrentTrackedObjects.FindAll((e) => boardMarkIds.Contains(e));
            if (currentTrackedBoardMarks.Count > 0)
            {
                string id = ChooseReferenceMarker();
                if (id != referenceMarker.id)
                {
                    GameObject marker = vuMarkHandler.FindModelById(id);
                    if (marker != null)
                    {
                        referenceMarker = (id, marker);
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