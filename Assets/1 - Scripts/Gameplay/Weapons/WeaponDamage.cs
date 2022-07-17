using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private float physicAttack;
    [SerializeField] private float magicAttack;
    [HideInInspector] public UnitController controller;

    public float lifeTime = 0.1f;
    private float currentLifeTime = 0;

    [HideInInspector] private List<GameObject> enemyList = new List<GameObject>();

    public void SetSettings(UnitController unitController)
    {
        controller = unitController;
        physicAttack = unitController.physicAttack;
        magicAttack = unitController.magicAttack;

    }

    public void ClearEnemyList()
    {
        enemyList.Clear();
    }

    private void OnEnable()
    {
        currentLifeTime = 0;
    }

    private void Update()
    {
        if(lifeTime != 0)
        {
            currentLifeTime += Time.deltaTime;

            if(currentLifeTime >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_ENEMY) == true)
        {
            //we need to check for re-touch. if we don't need this then add enemy in list
            if(enemyList.Contains(collision.gameObject) == false) 
            {
                if(controller.unitAbility == NameManager.UnitsAbilities.Garlic)
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, Vector3.zero);

                    if(controller.level == 2) collision.GetComponent<EnemyController>().PushMe(transform.position, 5000f);
                    if(controller.level == 3) collision.GetComponent<EnemyMovement>().MakeMeFixed(true, true);

                    enemyList.Add(collision.gameObject);
                }

                else if(controller.unitAbility == NameManager.UnitsAbilities.Axe)
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
                    enemyList.Add(collision.gameObject);
                }

                else if(controller.unitAbility == NameManager.UnitsAbilities.Spear)
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
                    enemyList.Add(collision.gameObject);
                }
                else if(controller.unitAbility == NameManager.UnitsAbilities.Bible)
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
                    enemyList.Add(collision.gameObject);
                }
                else if(controller.unitAbility == NameManager.UnitsAbilities.Bow)
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
                    enemyList.Add(collision.gameObject);
                }
                else if(controller.unitAbility == NameManager.UnitsAbilities.Knife)
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
                    enemyList.Add(collision.gameObject);
                }
                else if(controller.unitAbility == NameManager.UnitsAbilities.Bottle)
                {
                    //no one action
                }
                else
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
                }
            }           
            
        }

        if (collision.CompareTag(TagManager.T_OBSTACLE) == true)
        {
            collision.gameObject.GetComponent<HealthObjectStats>().TakeDamage(physicAttack, magicAttack);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
    }

}
