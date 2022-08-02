using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    private bool isSettingsPanelEnebled = true;

    public Color[] colors;

    public HexGrid hexGrid;

    private Color activeColor;
    private bool applyColor = false;

    private int brushSize = 0;

    private void Awake()
    {
        SelectColor(0);
    }


    private void Update()
    {
        if(Input.GetMouseButton(0) == true && EventSystem.current.IsPointerOverGameObject() == false) HandleInput();

        if(Input.GetKeyDown(KeyCode.F1) == true) settingsPanel.SetActive(isSettingsPanelEnebled = !isSettingsPanelEnebled);
    }


    private void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(inputRay, out hit)) EditFewCells(hexGrid.GetCell(hit.point));
    }

    private void EditFewCells(HexCell centerCell)
    {
        int centerCellX = centerCell.coordinates.X;
        int centerCellY = centerCell.coordinates.Y;

        for(int r = 0, y = centerCellY - brushSize; y <= centerCellY; y++, r++)
        {
            for(int x = centerCellX - r; x <= centerCellX + brushSize; x++)
            {
                EditCell(hexGrid.GetCell(new HexCoordinates(x, y)));
            }
        }

        for(int r = 0, y = centerCellY + brushSize; y > centerCellY; y--, r++)
        {
            for(int x = centerCellX - brushSize; x <= centerCellX + r; x++)
            {
                EditCell(hexGrid.GetCell(new HexCoordinates(x, y)));
            }
        }
    }

    private void EditCell(HexCell cell)
    {
        if(cell == null) return;

        if(applyColor == true) cell.Color = activeColor;
    }

    public void SelectColor(int index)
    {
        applyColor = index >= 0;

        if(applyColor == true) activeColor = colors[index];
    }

    public void SetBrushSize(float size)
    {
        brushSize = (int)size;
    }

    public void ShowUI(bool visible)
    {
        hexGrid.ShowUI(visible);
    }
}
