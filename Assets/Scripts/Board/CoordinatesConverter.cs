using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class CoordinatesConverter : MonoBehaviour
    {
        // marks that identify the position and orientation of a board, sticked to the board
        [SerializeField] private List<Vector2> boardMarkPositions;
        [SerializeField] private List<string> boardMarkIds;
        [SerializeField] private MultiVuMarkHandler vuMarkHandler;

        [SerializeField] private float scale = 1/1.75f;

        public List<string> BoardMarkIds => boardMarkIds;
        public float Scale => scale;

        private Dictionary<string, Vector2> boardMarks;
        private BoardMono board;


        public bool IsTrackingBoard()
        {
            return board.CurrentTrackedBoardMarks.Count != 0;
        }

        /// <summary>
        /// Use this method to obtain scene coordinates of a point on the board plane
        /// </summary>
        /// <param name="boardCoordinates">the point to map to world space</param>
        /// <returns>scene coordinates of a given point or (0,0,0) if the board is not tracked</returns>
        public Vector3 ConvertCoordinates(Vector2 boardCoordinates)
        {
            if (!IsTrackingBoard())
                return Vector3.zero;

            var result = Vector3.zero;
            foreach (var refMarkerId in board.CurrentTrackedBoardMarks)
            {
                var vuMarkBehaviourPosiiton = new Vector3(0, 0, 0);
                if (GameManager.CurrentTrackedObjects.ContainsKey(refMarkerId))
                {
                    var vuMarkBehaviour = GameManager.CurrentTrackedObjects[refMarkerId];
                    vuMarkBehaviourPosiiton = vuMarkBehaviour.transform.position - vuMarkHandler.transform.position;
                }

                GameObject refMarker = vuMarkHandler.FindModelById(refMarkerId);

                result = result + vuMarkBehaviourPosiiton + refMarker.transform.position +
                    (boardCoordinates.x - boardMarks[refMarkerId].x) * refMarker.transform.right.normalized * scale +
                    (boardCoordinates.y - boardMarks[refMarkerId].y) * refMarker.transform.forward.normalized * scale;
            }

            return result / board.CurrentTrackedBoardMarks.Count;
        }

        public Vector3 ConvertCoordinates(Vector2 boardCoordinates, string referenceMarkerId)
        {
            GameObject marker = vuMarkHandler.FindModelById(referenceMarkerId);

            return marker.transform.position +
                    (boardCoordinates.x - boardMarks[referenceMarkerId].x) * marker.transform.right.normalized * scale +
                    (boardCoordinates.y - boardMarks[referenceMarkerId].y) * marker.transform.forward.normalized * scale;
        }

        public Quaternion ReferenceRotation()
        {
            if (!IsTrackingBoard())
                return Quaternion.identity;

            Quaternion totalRotation = Quaternion.identity;
            foreach (var refMarkerId in board.CurrentTrackedBoardMarks)
            {
                GameObject refMarker = vuMarkHandler.FindModelById(refMarkerId);
                totalRotation *= refMarker.transform.rotation;
            }
            totalRotation.Normalize();
            return Quaternion.Slerp(Quaternion.identity, totalRotation, 1.0f / board.CurrentTrackedBoardMarks.Count);
        }

        public Vector2 WorldToBoard(Vector3 worldPos)
        {
            if (!IsTrackingBoard())
                return -Vector2.one;

            var boardPos = Vector2.zero;
            foreach (var refMarkerId in board.CurrentTrackedBoardMarks)
            {
                GameObject refMarker = vuMarkHandler.FindModelById(refMarkerId);

                var vuMarkBehaviourPosiiton = new Vector3(0, 0, 0);
                if (GameManager.CurrentTrackedObjects.ContainsKey(refMarkerId))
                {
                    var vuMarkBehaviour = GameManager.CurrentTrackedObjects[refMarkerId];
                    vuMarkBehaviourPosiiton = vuMarkBehaviour.transform.position - vuMarkHandler.transform.position;
                }

                Vector3 referencePosition = refMarker.transform.position - vuMarkBehaviourPosiiton;

                // direction down the pawn
                Vector3 normalizedDirection = -1 * refMarker.transform.up.normalized;

                // line from given position to reference marker in 3d space
                Vector3 lineToBoard = referencePosition - worldPos;

                // distance of the pawn from the board
                float projection = Vector3.Dot(lineToBoard, normalizedDirection);
                projection /= scale;

                // pawn projected onto the board
                Vector3 intersection = worldPos + projection * normalizedDirection;

                // offset from reference transform to projected pawn position
                Vector3 offset = intersection - referencePosition;
                float y_dist_board = Vector3.Dot(offset, refMarker.transform.forward);
                float x_dist_board = Vector3.Dot(offset, refMarker.transform.right);
                boardPos = boardPos + boardMarks[refMarkerId]
                    - Vector2.right * x_dist_board / scale
                    - Vector2.up * y_dist_board / scale;
            }                

            return boardPos / board.CurrentTrackedBoardMarks.Count;
        }

        private void Awake()
        {
            boardMarks = new Dictionary<string, Vector2>();
            for (int i = 0; i < boardMarkPositions.Count; i++)
                boardMarks[boardMarkIds[i]] = boardMarkPositions[i];
            board = GetComponentInParent<BoardMono>();
        }
    }
}