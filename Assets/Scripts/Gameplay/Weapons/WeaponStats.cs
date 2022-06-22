using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
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

    private void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= lifeTime) gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_ENEMY) == true)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(physicAttack, magicAttack, transform.position);
        }

        if (collision.CompareTag(TagManager.T_BONUSBOX) == true)
        {

        }
    }
}
