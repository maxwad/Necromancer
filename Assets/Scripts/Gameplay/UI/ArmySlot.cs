using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArmySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public TMP_Text quantity;
    public int index;
    public Image backlight;
    private Color visible = new Color(1, 0.45f, 0, 1);
    private Color unvisible = new Color(1, 0.45f, 0, 0);
    private Unit unitInSlot;

    private PlayersArmy playersArmy;

    private void Start()
    {
        backlight.color = unvisible;
        playersArmy = GlobalStorage.instance.player.GetComponent<PlayersArmy>();
    }

    public void FillTheSlot(Unit unit)
    {
        if (unit != null)
        {
            unitInSlot = unit;
            icon.sprite = unit.unitIcon;
            quantity.text = unit.quantity.ToString();            
        }
        else
        {
            unitInSlot = null;
            icon.sprite = null;
            quantity.text = null;
        }
        backlight.color = unvisible;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        backlight.color = backlight.color == unvisible? visible : unvisible;
        playersArmy.UnitsReplacementUI(index);
    }

    public void ResetSelecting()
    {
        backlight.color = unvisible;
        playersArmy.ResetReplaceIndexes();
    }

}
