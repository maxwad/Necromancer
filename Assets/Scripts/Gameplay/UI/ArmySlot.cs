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
    public bool isOpen;
    private Image backlight;
    private Unit unitInSlot;

    private PlayersArmy playersArmy;

    private void Awake()
    {
        backlight = GetComponent<Image>();
        StartCoroutine(GetArmy());
    }

    //this coroutine is nessesary for load data!
    private IEnumerator GetArmy()
    {
        while (GlobalStorage.instance == null)
            yield return null;

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

       GetComponent<Image>().enabled = false;    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        backlight.enabled = !backlight.enabled;
        playersArmy.UnitsReplacement(index);
    }

    public void ResetSelecting()
    {
        backlight.enabled = false;
        playersArmy.ResetReplaceIndexes();
    }

}
