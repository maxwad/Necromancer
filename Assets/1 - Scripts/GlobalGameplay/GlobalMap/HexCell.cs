using UnityEngine;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;

    private Color color;

    public Color Color {

        get
        {
            return color;
        }

        set
        {
            if(Color == value) return;

            color = value;
            Refresh();
        }    
    }

    public RectTransform uiRect;

    public HexGridPart part;

    public void Refresh()
    {
        if(part != null) part.Refresh();
    }
}
