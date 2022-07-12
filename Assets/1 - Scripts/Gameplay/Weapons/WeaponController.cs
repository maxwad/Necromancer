using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StructManager;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float physicAttack;
    [SerializeField] private float magicAttack;
    [HideInInspector] public UnitController controller;

    public float lifeTime = 0.1f;
    private float currentLifeTime = 0;

    public void SetSettings(UnitController unitController)
    {
        controller = unitController;
        physicAttack = unitController.physicAttack;
        magicAttack = unitController.magicAttack;

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
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
        }

        if (collision.CompareTag(TagManager.T_OBSTACLE) == true)
        {
            collision.gameObject.GetComponent<HealthObjectStats>().TakeDamage(physicAttack, magicAttack);
        }
    }


}
