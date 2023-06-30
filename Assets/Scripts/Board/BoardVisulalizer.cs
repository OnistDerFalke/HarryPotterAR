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
        [SerializeField] private Material fieldMaterial;
        [SerializeField] private float particleRadiusFactor;

        public GameObject highlightPrefab;

        private List<(Field, GameObject)> fieldHighlights = new();

        private Quaternion referenceRotation = Quaternion.identity;

        private void TrackCorners()
        {
            corners[0].transform.position = converter.ConvertCoordinates(Vector2.zero);
            corners[1].transform.position = converter.ConvertCoordinates(Vector2.right * boardMono.Board.Width);
            corners[2].transform.position = converter.ConvertCoordinates(Vector2.up * boardMono.Board.Heigth);
            corners[3].transform.position = converter.ConvertCoordinates(Vector2.one * boardMono.Board.Size);
            corners[0].transform.rotation = referenceRotation;
            corners[1].transform.rotation = referenceRotation;
            corners[2].transform.rotation = referenceRotation;
            corners[3].transform.rotation = referenceRotation;
        }

        private void TrackHighlights()
        {
            foreach((Field f, GameObject g) highlight in fieldHighlights)
            {
                highlight.g.transform.position = converter.ConvertCoordinates(highlight.f.Figure.CenterPosition);
                highlight.g.transform.rotation = referenceRotation;
            }
        }

        private void MakeQuadrangle(MeshFilter meshFilter, Quadrangle q, Vector2 position)
        {
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(q.leftBottom.x - position.x, 0f, q.leftBottom.y - position.y);
            vertices[1] = new Vector3(q.leftUpper.x - position.x, 0f, q.leftUpper.y - position.y);
            vertices[2] = new Vector3(q.rightUpper.x - position.x, 0f, q.rightUpper.y - position.y);
            vertices[3] = new Vector3(q.rightBottom.x - position.x, 0f, q.rightBottom.y - position.y);
            int[] triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        private void AdjustParticleEffectSize(Field field, ParticleSystem ps)
        {
            //var shape = ps.shape;
            if (field.Figure is Quadrangle)
            {
                Quadrangle q = field.Figure as Quadrangle;
                float shortestSide = Mathf.Min(
                Vector2.Distance(q.leftUpper, q.leftBottom),
                Vector2.Distance(q.leftUpper, q.rightUpper),
                Vector2.Distance(q.rightUpper, q.rightBottom),
                Vector2.Distance(q.leftBottom, q.rightBottom));
                //shape.radius = shortestSide * particleRadiusFactor;
            }
            else if (field.Figure is Circle)
            {
                Circle c = field.Figure as Circle;
                //shape.radius = c.Radius * particleRadiusFactor;
            }
        }

        private void MakeCircle(MeshFilter meshFilter, int numOfPoints, Circle c, Vector2 position)
        {
            float angleStep = 360.0f / (float)numOfPoints;
            List<Vector3> vertexList = new List<Vector3>();
            List<int> triangleList = new List<int>();
            Quaternion quaternion = Quaternion.Euler(0.0f, angleStep, 0.0f);
            vertexList.Add(new Vector3(0.0f, 0.0f, 0.0f));  
            vertexList.Add(new Vector3(0.0f, 0.0f, c.Radius));  
            vertexList.Add(quaternion * vertexList[1]);    
                                                          
            triangleList.Add(0);
            triangleList.Add(1);
            triangleList.Add(2);
            for (int i = 0; i < numOfPoints - 1; i++)
            {
                triangleList.Add(0);                     
                triangleList.Add(vertexList.Count - 1);
                triangleList.Add(vertexList.Count);
                vertexList.Add(quaternion * vertexList[vertexList.Count - 1]);
            }
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;
            mesh.vertices = vertexList.ToArray();
            mesh.triangles = triangleList.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        private void AssignFieldMesh(GameObject highlight, Field field)
        {
            MeshFilter meshFilter = highlight.GetComponent<MeshFilter>();
            if (field.Figure is Quadrangle)
            {
                Quadrangle q = field.Figure as Quadrangle;
                MakeQuadrangle(meshFilter, q, field.Figure.CenterPosition);
            }
            else if (field.Figure is Circle)
            {
                Circle c = field.Figure as Circle;
                MakeCircle(meshFilter, 20, c, field.Figure.CenterPosition);
            }
            AdjustParticleEffectSize(field, highlight.GetComponent<ParticleSystem>());
        }

        public void HighlightField(Field f)
        {
            Debug.Log($"highlighting {f.Index}");
            GameObject highlight = Instantiate(highlightPrefab, transform);
            AssignFieldMesh(highlight, f);
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
                    referenceRotation = converter.ReferenceRotation();
                    TrackCorners();
                    TrackHighlights();
                }
            }
        }
    }
}