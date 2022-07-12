using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class WeaponMovement : MonoBehaviour
{
    UnitController controller;
    private bool isReadyToWork = false;

    public float speed = 1;
    private SpriteRenderer unitSprite;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetSettings(UnitController unitController)
    {
        controller = unitController;
        unitSprite = unitController.unitSprite;

        isReadyToWork = true;
    }

    private void Update()
    {
        if(isReadyToWork == true) WeaponMoving();
    }

    private void WeaponMoving()
    {
        switch(controller.unitAbility)
        {
            case UnitsAbilities.Whip:
                //Whip();
                break;
            case UnitsAbilities.Garlic:
                break;
            case UnitsAbilities.Axe:
                break;
            case UnitsAbilities.Spear:
                break;
            case UnitsAbilities.Bible:
                break;
            case UnitsAbilities.Bow:
                break;
            case UnitsAbilities.Knive:
                break;
            case UnitsAbilities.Bottle:
                break;
            default:
                break;
        }
    }



}
