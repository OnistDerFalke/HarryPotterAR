using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoardVisulalizer : MonoBehaviour
    {
        [SerializeField] private GameObject[] corners;
        [SerializeField] private CoordinatesConverter converter;
        [SerializeField] private BoardMono boardMono;

        private void TrackCorners()
        {

            corners[0].transform.position = converter.ConvertCoordinates(Vector2.zero);
            corners[1].transform.position = converter.ConvertCoordinates(Vector2.right * boardMono.Board.Width);
            corners[2].transform.position = converter.ConvertCoordinates(Vector2.up * boardMono.Board.Heigth);
            corners[3].transform.position = converter.ConvertCoordinates(Vector2.one * boardMono.Board.Size);
        }

        private void Awake()
        {

        }

        private void Update()
        {
            if (boardMono.Board != null)
            {
                if (converter.IsTrackingBoard())
                {
                    TrackCorners();
                }
            }
        }
    }
}