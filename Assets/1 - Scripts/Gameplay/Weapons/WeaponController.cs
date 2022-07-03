using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float magicAttack;
    [SerializeField] private float physicAttack;

    private float lifeTime = 0.1f;
    private float currentLifeTime = 0;

    public void SetAttackParameters(float magicDamage, float physicDamage)
    {
        magicAttack = magicDamage;
        physicAttack = physicDamage;
    }

    private void OnEnable()
    {
        currentLifeTime = 0;
    }

    private void FixedUpdate()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= lifeTime) {
            //Destroy(gameObject);
            gameObject.SetActive(false);
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
