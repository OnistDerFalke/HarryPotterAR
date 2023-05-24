using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private float width;
        [SerializeField] private float height;
        [SerializeField] private List<Field> fields = new List<Field>();
        [SerializeField] private CoordinatesConverter coordinatesConverter;
        //TODO: dodaæ info o znacznikach

        public int Id { get => id; }
        public float Width { get => width; }
        public float Heigth { get => height; }
        public List<Field> Fields { get => fields; }
        public CoordinatesConverter CoordinatesConverter { get => coordinatesConverter; }

        public Board(int id, float width, float height)
        {
            this.id = id;
            this.width = width;
            this.height = height;
        }
    }
}