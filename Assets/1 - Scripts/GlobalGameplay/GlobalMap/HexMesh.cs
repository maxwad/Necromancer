using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class HexMesh : MonoBehaviour
{
    private Mesh hexMesh;
    private static List<Vector3> vertices = new List<Vector3>();
    private static List<int> triangles = new List<int>();
    private static List<Color> colors = new List<Color>();

    MeshCollider meshCollider;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();

        hexMesh.name = "Hex Mesh";
    }

    public void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    public void AddTriangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        int vertexIndex = vertices.Count;

        vertices.Add(p1);
        vertices.Add(p2);
        vertices.Add(p3);

        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    public void Clear()
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();
    }

    public void Apply()
    {
        hexMesh.SetVertices(vertices);
        hexMesh.SetTriangles(triangles, 0);
        hexMesh.SetColors(colors);
        hexMesh.RecalculateNormals();

        meshCollider.sharedMesh = hexMesh;
    }
}
