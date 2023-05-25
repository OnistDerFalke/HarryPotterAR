using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private Vector2 size;
        [SerializeField] private List<Field> fields = new List<Field>();
        [SerializeField] private CoordinatesConverter coordinatesConverter;
        [SerializeField] private MultiVuMarkHandler vuMarkHandler;
        [SerializeField] private BoardVisulalizer visualizer;

        public int Id { get => id; }
        public float Width { get => size.x; }
        public float Heigth { get => size.y; }
        public Vector2 Size { get => size; }
        public List<Field> Fields { get => fields; }
        public CoordinatesConverter CoordinatesConverter { get => coordinatesConverter; }

        public Board(int id, float width, float height)
        {
            this.id = id;
            size = new Vector2(width, height);
        }
    }
}