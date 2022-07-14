using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static NameManager;

public class WeaponStorage : MonoBehaviour
{
    private bool isGarlicWork = false;
    private bool isBibleWork = false;

    public void Attack(UnitController unitController)
    {
        switch(unitController.unitAbility)
        {
            case UnitsAbilities.Whip:
                WhipAction(unitController);
                break;

            case UnitsAbilities.Garlic:
                if(isGarlicWork == false) GarlicAction(unitController);
                break;

            case UnitsAbilities.Axe:
                AxeAction(unitController);
                break;

            case UnitsAbilities.Spear:
                SpearAction(unitController);
                break;

            case UnitsAbilities.Bible:
                if(isBibleWork == false) BibleAction(unitController);
                break;

            case UnitsAbilities.Bow:
                break;
            case UnitsAbilities.Knife:
                break;
            case UnitsAbilities.Bottle:
                break;
            default:
                break;
        }
    }

    #region Helpers

    private GameObject CreateWeapon(UnitController unitController)
    {
        GameObject weapon = Instantiate(unitController.attackTool);

        weapon.transform.position = transform.position;
        weapon.transform.localScale = new Vector3(unitController.size, unitController.size, unitController.size);
        weapon.transform.SetParent(transform);

        weapon.GetComponent<WeaponDamage>().SetSettings(unitController);
        weapon.GetComponent<WeaponMovement>().SetSettings(unitController);

        return weapon;
    }

    #endregion


    private void WhipAction(UnitController unitController) 
    {
        float normalYAngle = 0f;
        float flipYAngle = 180f;
        float zAngle = 25f;

        if(unitController.level == 1)
        {
            CreateConfiguredWeapon(flipYAngle, normalYAngle);           
        }

        if(unitController.level == 2)
        {
            CreateConfiguredWeapon(flipYAngle, normalYAngle);

            CreateConfiguredWeapon(normalYAngle, flipYAngle);            
        }

        if(unitController.level == 3)
        {
            CreateConfiguredWeapon(normalYAngle, flipYAngle, zAngle);
            CreateConfiguredWeapon(normalYAngle, flipYAngle, -zAngle);

            CreateConfiguredWeapon(flipYAngle, normalYAngle, zAngle);
            CreateConfiguredWeapon(flipYAngle, normalYAngle, -zAngle);            
        }

        void CreateConfiguredWeapon(float normalAngleY, float flipAngleY, float angleZ = 0)
        {
            GameObject itemWeapon = CreateWeapon(unitController);
            float yAngle = unitController.unitSprite.flipX == true ? normalAngleY : flipAngleY;
            itemWeapon.transform.eulerAngles = new Vector3(itemWeapon.transform.eulerAngles.x, yAngle, angleZ);
        }
    }


    private void GarlicAction(UnitController unitController) 
    {
        isGarlicWork = true;

        GameObject weapon = CreateWeapon(unitController);
        weapon.GetComponent<WeaponMovement>().ActivateWeapon(unitController);
    }


    private void AxeAction(UnitController unitController)
    {
        float axeAngleLvl1 = 20;
        float axeAngleLvl2 = 45;

        if(unitController.level == 1)
        {
            CreateConfiguredWeapon(1, 0);            
        }

        if(unitController.level == 2)
        {
            CreateConfiguredWeapon(1, axeAngleLvl1);

            CreateConfiguredWeapon(2, -axeAngleLvl1);
        }

        if(unitController.level == 3)
        {
            CreateConfiguredWeapon(1, axeAngleLvl2);

            CreateConfiguredWeapon(2, 0);

            CreateConfiguredWeapon(3, -axeAngleLvl2);
        }

        void CreateConfiguredWeapon(int index, float angleZ = 0)
        {
            GameObject weapon = CreateWeapon(unitController);
            weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, weapon.transform.eulerAngles.y, angleZ);
            weapon.GetComponent<WeaponMovement>().ActivateWeapon(unitController, index);
        }
    }


    private void SpearAction(UnitController unitController) 
    {
        StartCoroutine(CreateSpears(unitController.level));

        IEnumerator CreateSpears(int count)
        {
            for(int i = 0; i < count; i++)
            {
                GameObject itemWeapon = CreateWeapon(unitController);
                itemWeapon.GetComponent<WeaponMovement>().ActivateWeapon(unitController);
                yield return new WaitForSeconds(0.2f);
            }
        }        
    }

    private void BibleAction(UnitController unitController)
    {
        isBibleWork = true;

        StartCoroutine(CreateBible(unitController.level));

        IEnumerator CreateBible(int count)
        {
            for(int i = 0; i < count; i++)
            {
                GameObject itemWeapon = CreateWeapon(unitController);
                itemWeapon.GetComponent<WeaponMovement>().ActivateWeapon(unitController);
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}
