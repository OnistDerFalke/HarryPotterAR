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

        public GameObject highlightPrefab;

        private List<(Field, GameObject)> fieldHighlights = new();

        private void TrackCorners()
        {
            corners[0].transform.position = converter.ConvertCoordinates(Vector2.zero);
            corners[1].transform.position = converter.ConvertCoordinates(Vector2.right * boardMono.Board.Width);
            corners[2].transform.position = converter.ConvertCoordinates(Vector2.up * boardMono.Board.Heigth);
            corners[3].transform.position = converter.ConvertCoordinates(Vector2.one * boardMono.Board.Size);
        }

        private void TrackHighlights()
        {
            foreach((Field f, GameObject g) highlight in fieldHighlights)
            {
                highlight.g.transform.position = converter.ConvertCoordinates(highlight.f.Figure.CenterPosition);
            }
        }

        public void HighlightField(Field f)
        {
            GameObject highlight = Instantiate(highlightPrefab, transform);
            fieldHighlights.Add((f, highlight));
        }

        public void UnhighlightField(Field f)
        {
            (Field, GameObject)? highlight = fieldHighlights.Find((e) => e.Item1 == f);
            if (highlight.HasValue)
            {
                Destroy(highlight.Value.Item2);
                fieldHighlights.Remove(highlight.Value);
            }
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
                    TrackHighlights();
                }
            }
        }
    }
}