using System;
using UnityEngine;
using UnityEngine.UI;

public class HexGridPart : MonoBehaviour
{
    private HexCell[] cells;

    private HexMesh hexMesh;
    private Canvas gridCanvas;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[HexMetrics.partSizeX * HexMetrics.partSizeY];
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
        hexMesh.Triangulate(cells);
        enabled = false;
    }
}
