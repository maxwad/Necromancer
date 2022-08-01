using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HexGrid : MonoBehaviour
{
    private int cellCountX;
    private int cellCountY;
    private int scaleMultiplier = 1;

    public int partCountX = 4;
    public int partCountY = 3;

    public HexCell cellPrefab;
    private HexCell[] cells;

    public TMP_Text cellLabel;

    public HexGridPart gridPartPrefab;

    private HexGridPart[] mapsParts;

    public Color defaultColor = Color.red;
    public Color touchColor = Color.green;

    private void Awake()
    {
        cellCountX = partCountX * HexMetrics.partSizeX;
        cellCountY = partCountY * HexMetrics.partSizeY;

        CreateMapsParts();
        CreateCells();       
    }

    private void CreateMapsParts()
    {
        mapsParts = new HexGridPart[partCountX * partCountY];

        for(int y = 0, i = 0; y < partCountY; y++)
        {
            for(int x = 0; x < partCountX; x++)
            {
                HexGridPart part = mapsParts[i++] = Instantiate(gridPartPrefab);
                part.transform.SetParent(transform);
            }
        }
    }

    private void CreateCells()
    {
        cells = new HexCell[cellCountX * cellCountY];

        for(int y = 0, i = 0; y < cellCountY; y++)
        {
            for(int x = 0; x < cellCountX; x++)
            {
                CreateOneCell(x, y, i++);
            }
        }
    }

    public void ColorCell(Vector3 point, Color color)
    {
        point = transform.InverseTransformPoint(point);

        HexCoordinates coordinates = HexCoordinates.FromPosition(point);
        Debug.Log("Clicked at " + coordinates.ToString());

        int index = coordinates.X + coordinates.Y * cellCountX + coordinates.Y / 2;
        HexCell cell = cells[index];
        cell.Color = color;
    }

    private void CreateOneCell(int x, int y, int i)
    {
        Vector2 position;
        position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f) * scaleMultiplier;
        position.y = y * (HexMetrics.outerRadius * 1.5f) * scaleMultiplier;

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        //cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, y);
        cell.Color = defaultColor;

        TMP_Text label = Instantiate<TMP_Text>(cellLabel);
        //label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
        label.text = cell.coordinates.ToString();

        cell.uiRect = label.rectTransform;

        AddCellToPart(x, y, cell);
    }

    private void AddCellToPart(int x, int y, HexCell cell)
    {
        int partX = x / HexMetrics.partSizeX;
        int partY = y / HexMetrics.partSizeY;
        HexGridPart part = mapsParts[partX + partY * partCountX];

        int localX = x - partX * HexMetrics.partSizeX;
        int localY = y - partY * HexMetrics.partSizeY;
        part.AddCell(localX + (localY * HexMetrics.partSizeX), cell);

    }
}
