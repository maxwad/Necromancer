using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static NameManager;
using static UnityEngine.GraphicsBuffer;

public class WeaponMovement : MonoBehaviour
{
    UnitController controller;
    private bool isReadyToWork = false;

    private Rigidbody2D rbSpear;
    private GameObject spearEnemy;
    private Vector2 spearDirection;

    private Vector2 bibleCenter;

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
                BibleMovement();
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

        if(unitController.unitAbility == UnitsAbilities.Spear) ActivateSpear();

        if(unitController.unitAbility == UnitsAbilities.Bible) ActivateBible();

    }

    #endregion

    #region Movement

    private void SpearMovement()
    {
        rbSpear.velocity = -rbSpear.transform.right * speed;
    }

    private void BibleMovement()
    {
        transform.RotateAround(controller.transform.position, Vector3.forward, speed * Time.deltaTime);

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

    private void ActivateSpear()
    {
        rbSpear = GetComponent<Rigidbody2D>();

        float searchRadius = 25f;
        float distance = 9999999;
        Vector2 nearestEnemyPosition = Vector2.zero;


        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, searchRadius);
        foreach(Collider2D obj in objects)
        {
            if(obj.CompareTag(TagManager.T_ENEMY) == true)
            {
                float currentDistance = Vector2.Distance(transform.position, obj.transform.position);

                if(currentDistance < distance)
                {
                    distance = currentDistance;
                    nearestEnemyPosition = obj.transform.position;                 
                }
            }            
        }

        if(nearestEnemyPosition != Vector2.zero)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                Mathf.Atan2(nearestEnemyPosition.y - transform.position.y, nearestEnemyPosition.x - transform.position.x) * Mathf.Rad2Deg - 180
            );

        }
        else
        {
            float yAngle = unitSprite.flipX == true ? 180 : 0;
            rbSpear.transform.eulerAngles = new Vector3(rbSpear.transform.eulerAngles.x, yAngle, rbSpear.transform.eulerAngles.z);
        }
        
        isReadyToWork = true;
    }

    private void ActivateBible()
    {
        bibleCenter = transform.position;
        transform.position += (Vector3)(Vector2.one * 3);

        isReadyToWork = true;
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
