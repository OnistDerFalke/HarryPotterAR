using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoardVisulalizer : MonoBehaviour
    {
        [SerializeField] private GameObject[] corners;
        [SerializeField] private CoordinatesConverter converter;
        [SerializeField] private Board board;

        private void TrackCorners()
        {
            corners[0].transform.position = converter.ConvertCoordinates(Vector2.zero);
            corners[1].transform.position = converter.ConvertCoordinates(Vector2.right * board.Width);
            corners[2].transform.position = converter.ConvertCoordinates(Vector2.up * board.Heigth);
            corners[3].transform.position = converter.ConvertCoordinates(Vector2.one * board.Size);
        }

        private void Awake()
        {

        }

        private void Update()
        {
            if (converter.IsTrackingBoard())
            {
                Debug.Log("there are corners somewhere");
                TrackCorners();
            }
        }
    }
}