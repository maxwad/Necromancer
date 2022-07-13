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
                //WhipMovement();
                break;

            case UnitsAbilities.Garlic:
                //GarlicMovement();
                break;

            case UnitsAbilities.Axe:
                //AxeMovement();
                break;

            case UnitsAbilities.Spear:
                SpearMovement();
                break;

            case UnitsAbilities.Bible:
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

    public void ActivateWeapon(UnitController unitController, int index = 0)
    {
        if(unitController.unitAbility == UnitsAbilities.Garlic) ActivateGarlic();

        if(unitController.unitAbility == UnitsAbilities.Axe) ActivateAxe(index);

    }

    #endregion

    #region Movement

    private void SpearMovement()
    {

    }

    #endregion

    #region Activators
    private void ActivateGarlic()
    {
        StartCoroutine(Resize());

        IEnumerator Resize()
        {
            float lifetime = 2f;
            float currentSize;
            float sizeStep = 0.05f;

            while(true)
            {                
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                currentSize = 0;
                GetComponent<WeaponDamage>().enemyList.Clear();

                yield return new WaitForSeconds(controller.speedAttack);

                while(currentSize <= lifetime)
                {
                    yield return new WaitForSeconds(0.005f);
                    currentSize += sizeStep;

                    gameObject.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
                }                
            }            
        }
    }

    private void ActivateAxe(int numberOfWeapon)
    {
        float force = 30f;
        float torqueForce = -200;

        if(controller.level == 2)
        {
            if(numberOfWeapon == 1) torqueForce = -torqueForce;
        }

        if(controller.level == 3)        {

            if(numberOfWeapon == 3) torqueForce = -torqueForce;
        }

        Rigidbody2D rbAxe = GetComponent<Rigidbody2D>();

        rbAxe.AddForce(rbAxe.transform.up * force, ForceMode2D.Impulse);
        rbAxe.AddTorque(torqueForce);
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.T_ENEMY) == true)
        {

            if(controller.unitAbility == UnitsAbilities.Garlic)
            {
                
            }
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
