using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Board
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

        public void AddField(Field field)
        {
            fields.Add(field);
        }

        public Field GetFieldById(int id)
        {
            return fields.Where(field => field.Index == id).FirstOrDefault();
        }

        public List<Field> GetFieldsByIds(List<int> ids)
        {
            return fields.Where(field => ids.Contains(field.Index)).ToList();
        }

        public List<Field> GetTowerFields()
        {
            return fields.Where(field => field.IsTower).ToList();
        }

        public Field GetQuidditchPitch()
        {
            return fields.Where(field => field.IsQuidditchPitch).FirstOrDefault();
        }
    }
}