using System;
using System.Collections;
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

    internal void Triangulate(HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        for(int i = 0; i < cells.Length; i++)
            TriangulateCell(cells[i]);

        hexMesh.vertices  = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.colors    = colors.ToArray();
        hexMesh.RecalculateNormals();

        meshCollider.sharedMesh = hexMesh;
    }

    private void TriangulateCell(HexCell cell)
    {
        Vector2 center = (Vector2)cell.transform.localPosition;

        for(int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i + 1]
                );

            AddTriangleColor(cell.Color);
        }

    }

    private void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    private void AddTriangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        int vertexIndex = vertices.Count;

        vertices.Add(p1);
        vertices.Add(p2);
        vertices.Add(p3);

        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
}
