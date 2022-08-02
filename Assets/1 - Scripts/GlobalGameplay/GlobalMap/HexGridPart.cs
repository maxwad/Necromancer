using System;
using UnityEngine;
using UnityEngine.UI;

public class HexGridPart : MonoBehaviour
{
    private HexCell[] cells;

    //private HexMesh hexMesh;
    public HexMesh terrain;
    private Canvas gridCanvas;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        terrain = GetComponentInChildren<HexMesh>();

        cells = new HexCell[HexMetrics.partSizeX * HexMetrics.partSizeY];

        ShowUI(false);
    }

    internal void AddCell(int index, HexCell cell)
    {
        cells[index] = cell;
        cell.part = this;
        cell.transform.SetParent(transform, false);
        cell.uiRect.SetParent(gridCanvas.transform, false);
    }

    public void Refresh()
    {
        enabled = true;        
    }

    private void LateUpdate()
    {
        Triangulate();
        enabled = false;
    }

    public void ShowUI(bool visible)
    {
        gridCanvas.gameObject.SetActive(visible);
    }

    private void Triangulate()
    {
        terrain.Clear();

        for(int i = 0; i < cells.Length; i++)
            TriangulateCell(cells[i]);

        terrain.Apply();
    }

    private void TriangulateCell(HexCell cell)
    {
        Vector2 center = (Vector2)cell.transform.localPosition;

        for(int i = 0; i < 6; i++)
        {
            terrain.AddTriangle(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i + 1]
                );

            terrain.AddTriangleColor(cell.Color);
        }
    }


}
