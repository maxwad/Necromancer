using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;
using static StructManager;

public class WeaponStorage : MonoBehaviour
{
    public void Attack(UnitController unitController)
    {
        switch(unitController.unitAbility)
        {
            case UnitsAbilities.Whip:
                WhipAction(unitController);
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

    private void WhipAction(UnitController unitController) 
    {
        if(unitController.level == 1)
        {
            GameObject weapon = Instantiate(unitController.attackTool);
            weapon.transform.position = transform.position;
            weapon.transform.SetParent(transform);
            weapon.GetComponent<WeaponController>().SetSettings(unitController);
            weapon.GetComponent<WeaponMovement>().SetSettings(unitController);

            float yAngle = unitController.unitSprite.flipX == true ? 180 : 0;
            weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, yAngle, weapon.transform.eulerAngles.x);

        }
       
    }

}
