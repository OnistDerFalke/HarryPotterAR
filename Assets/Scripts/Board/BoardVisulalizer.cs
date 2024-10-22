using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoardVisulalizer : MonoBehaviour
    {
        //[SerializeField] private GameObject[] corners;
        [SerializeField] private CoordinatesConverter converter;
        [SerializeField] private BoardMono boardMono;
        [SerializeField] private Material fieldMaterial;
        [SerializeField] private float particleRadiusFactor;

        public GameObject highlightPrefab;
        public GameObject fiuuHighlightPrefab;
        public GameObject missionHighlightPrefab;

        private List<(Field, GameObject)> fieldHighlights = new();

        private Quaternion referenceRotation = Quaternion.identity;

        //private void TrackCorners()
        //{
        //    corners[0].transform.position = converter.ConvertCoordinates(Vector2.zero);
        //    corners[1].transform.position = converter.ConvertCoordinates(Vector2.right * boardMono.Board.Width);
        //    corners[2].transform.position = converter.ConvertCoordinates(Vector2.up * boardMono.Board.Heigth);
        //    corners[3].transform.position = converter.ConvertCoordinates(Vector2.one * boardMono.Board.Size);
        //    corners[0].transform.rotation = referenceRotation;
        //    corners[1].transform.rotation = referenceRotation;
        //    corners[2].transform.rotation = referenceRotation;
        //    corners[3].transform.rotation = referenceRotation;
        //}

        private void TrackHighlights()
        {
            foreach ((Field f, GameObject g) highlight in fieldHighlights)
            {
                highlight.g.transform.position = converter.ConvertCoordinates(highlight.f.Figure.CenterPosition);
                highlight.g.transform.rotation = referenceRotation;
            }
        }

        private void MakeQuadrangle(MeshFilter meshFilter, Quadrangle q, Vector2 position)
        {
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(q.leftBottom.x - position.x, 0f, q.leftBottom.y - position.y) * converter.Scale;
            vertices[1] = new Vector3(q.leftUpper.x - position.x, 0f, q.leftUpper.y - position.y) * converter.Scale;
            vertices[2] = new Vector3(q.rightUpper.x - position.x, 0f, q.rightUpper.y - position.y) * converter.Scale;
            vertices[3] = new Vector3(q.rightBottom.x - position.x, 0f, q.rightBottom.y - position.y) * converter.Scale;
            int[] triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        private void AdjustParticleEffectSize(Field field, Transform ps)
        {
            if (field.Figure is Quadrangle)
            {
                Quadrangle q = field.Figure as Quadrangle;
                float shortestSide = Mathf.Min(
                Vector2.Distance(q.leftUpper, q.leftBottom),
                Vector2.Distance(q.leftUpper, q.rightUpper),
                Vector2.Distance(q.rightUpper, q.rightBottom),
                Vector2.Distance(q.leftBottom, q.rightBottom));
                ps.localScale = Vector3.one * shortestSide * particleRadiusFactor;
            }
            else if (field.Figure is Circle)
            {
                Circle c = field.Figure as Circle;
                ps.localScale = Vector3.one * c.Radius * 2 * particleRadiusFactor;
            }
        }

        private void MakeCircle(MeshFilter meshFilter, int numOfPoints, Circle c, Vector2 position)
        {
            float angleStep = 360.0f / (float)numOfPoints;
            List<Vector3> vertexList = new List<Vector3>();
            List<int> triangleList = new List<int>();
            Quaternion quaternion = Quaternion.Euler(0.0f, angleStep, 0.0f);
            vertexList.Add(new Vector3(0.0f, 0.0f, 0.0f));
            vertexList.Add(new Vector3(0.0f, 0.0f, c.Radius * converter.Scale));
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
            AdjustParticleEffectSize(field, highlight.transform.GetChild(0).GetComponent<Transform>());
        }

        public GameObject GetFieldMeshObject(Field f)
        {
            if (f.IsFiuuField)
                return Instantiate(fiuuHighlightPrefab, transform);
            if (f.IsTower)
                return Instantiate(highlightPrefab, transform);
            if (f.IsMissionField)
                return Instantiate(missionHighlightPrefab, transform);
            return Instantiate(highlightPrefab, transform);
        }

        public void HighlightField(Field f)
        {
            if(f.BoardId != boardMono.Board.Id)
            {
                return;
            }
            if (fieldHighlights.FindAll((e) => e.Item1.Index == f.Index).Count > 0)
            {
                return;
            }
            GameObject highlight = GetFieldMeshObject(f);
            AssignFieldMesh(highlight, f);
            highlight.GetComponent<InteractiveElement>().Index = f.Index;
            if(!boardMono.isTracked)
            {
                highlight.SetActive(false);
            }
            fieldHighlights.Add((f, highlight));
        }

        public void UnhighlightField(Field f)
        {
            if (f != null)
            {
                List<(Field, GameObject)> highlights = fieldHighlights.FindAll((e) => e.Item1.Index == f.Index);
                foreach (var h in highlights)
                {
                    h.Item2.SetActive(false);
                    Destroy(h.Item2);
                    fieldHighlights.Remove(h);
                }
            }
            else
            {
                List<(Field, GameObject)> highlights = new List<(Field, GameObject)>(fieldHighlights);
                foreach (var h in highlights)
                {
                    h.Item2.SetActive(false);
                    Destroy(h.Item2);
                    fieldHighlights.Remove(h);
                }
            }
        }

        public void HideVisuals()
        {
            foreach (var highlight in fieldHighlights)
            {
                highlight.Item2.SetActive(false);
            }
            //for (int i = 0; i < 4; i++)
            //{
            //    corners[i].SetActive(false);
            //}
        }

        public void ShowVisuals()
        {
            foreach (var highlight in fieldHighlights)
            {
                highlight.Item2.SetActive(true);
            }
            //for (int i = 0; i < 4; i++)
            //{
            //    corners[i].SetActive(true);
            //}
        }

        private void Update()
        {
            referenceRotation = converter.ReferenceRotation();
            //TrackCorners();
            TrackHighlights();
        }
    }
}