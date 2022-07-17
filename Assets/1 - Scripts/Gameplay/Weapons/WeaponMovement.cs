using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static NameManager;

public class WeaponMovement : MonoBehaviour
{
    UnitController controller;
    private bool isReadyToWork = false;

    private Rigidbody2D rbSpear;
    private GameObject spearEnemy;
    private Vector2 spearDirection;

    private SpriteRenderer bible;

    private Rigidbody2D rbArrow;

    private Rigidbody2D rbKnife;

    private Rigidbody2D rbBottle;
    private Vector3 goalPoint;
    [SerializeField] private GameObject bottleDeath;


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
                BowMovement();
                break;

            case UnitsAbilities.Knife:
                KnifeMovement();
                break;

            case UnitsAbilities.Bottle:
                BottleMovement();
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

        if(unitController.unitAbility == UnitsAbilities.Bow) ActivateBow();

        if(unitController.unitAbility == UnitsAbilities.Knife) ActivateKnife();

        if(unitController.unitAbility == UnitsAbilities.Bottle) ActivateBottle();

    }

    #endregion

    #region Movement

    private void SpearMovement()
    {
        rbSpear.velocity = -rbSpear.transform.right * speed;
    }

    private void BibleMovement()
    {
        transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
        bible.transform.RotateAround(bible.transform.position, Vector3.forward, -speed * Time.deltaTime);

        //reset EnemyList every cycle
        if(transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 5f)
        {
            GetComponent<WeaponDamage>().ClearEnemyList();
        }
    }

    private void BowMovement()
    {
        rbArrow.velocity = -rbArrow.transform.right * speed;
    }

    private void KnifeMovement()
    {
        rbKnife.velocity = -rbKnife.transform.right * speed;
    }

    private void BottleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, goalPoint, speed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x - goalPoint.x) < 0.1 && Mathf.Abs(transform.position.y - goalPoint.y) < 0.1)
            Explosive();
        
        
        void Explosive()
        {
            GameObject death = Instantiate(bottleDeath, transform.position, Quaternion.identity);
            death.transform.SetParent(GlobalStorage.instance.effectsContainer.transform);
            PrefabSettings settings = death.GetComponent<PrefabSettings>();

            if(settings != null) settings.SetSettings(sortingLayer: TagManager.T_PLAYER, color: UnityEngine.Color.cyan, size: controller.size);

            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, controller.size);
            foreach(Collider2D obj in objects)
            {
                if(obj.CompareTag(TagManager.T_ENEMY) == true)
                    obj.GetComponent<EnemyController>().TakeDamage(controller.physicAttack, controller.magicAttack, transform.position);
            }

            Destroy(gameObject);
        }
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
                GetComponent<WeaponDamage>().ClearEnemyList();

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
        bible = GetComponentInChildren<SpriteRenderer>();
        isReadyToWork = true;
    }

    private void ActivateBow()
    {
        rbArrow = GetComponent<Rigidbody2D>();
        isReadyToWork = true;
    }

    private void ActivateKnife() 
    {
        rbKnife = GetComponent<Rigidbody2D>();
        isReadyToWork = true;
    }

    private void ActivateBottle() 
    {
        rbBottle = GetComponent<Rigidbody2D>();

        goalPoint = transform.position + (Vector3)GetRandomPoint();

        isReadyToWork = true;

        Vector3 GetRandomPoint()
        {
            float minRadiusMovement = 2;
            float maxRadiusMovement = 10;

            float x = Random.Range(-maxRadiusMovement, maxRadiusMovement);
            float y = Random.Range(-maxRadiusMovement, maxRadiusMovement);

            Vector3 resultPoint = Vector3.zero;

            if((x > -minRadiusMovement && x < minRadiusMovement) || (y > -minRadiusMovement && y < minRadiusMovement))
                resultPoint = GetRandomPoint();
            else
                resultPoint = new Vector3(x, y, 0);

            return resultPoint;
        }
    }

    #endregion


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
