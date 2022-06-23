using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static NameManager;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public EnemiesTypes enemiesType;
    [SerializeField] private float health;
    [SerializeField] private float magicAttack;
    [SerializeField] private float physicAttack;
    [SerializeField] private float magicDefence;
    [SerializeField] private float physicDefence;
    [SerializeField] private float speedAttack;
    [SerializeField] private float size;
    [SerializeField] private EnemyAbilities EnemyAbility;

    [SerializeField] private GameObject attackTool;

    [SerializeField] public int exp;

    private float currentHealth;
    private float delayAttack;

    private SpriteRenderer enemySprite;
    private Color normalColor;
    private Color damageColor = Color.black;
    private float blinkTime = 0.1f;

    [SerializeField] private GameObject damageNote;
    private Color colorDamage = Color.red;

    private HeroController hero;
    private Rigidbody2D rbEnemy;
    private float pushForce = 10000f;

    private void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        currentHealth = health;
        delayAttack = speedAttack;
        enemySprite = GetComponent<SpriteRenderer>();
        normalColor = enemySprite.color;
    }

    private void Update()
    {
        delayAttack -= Time.deltaTime;
    }

    public void Initialize(Enemy stats)
    {
        enemiesType   = stats.EnemiesType;

        health        = stats.health;
        magicAttack   = stats.magicAttack;
        physicAttack  = stats.physicAttack;
        magicDefence  = stats.magicDefence;
        physicDefence = stats.physicDefence;
        speedAttack   = stats.speedAttack;
        size          = stats.size;

        EnemyAbility  = stats.EnemyAbility;
        attackTool    = stats.attackTool;

        exp           = stats.exp;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        //TODO: check depending of time
        if (delayAttack <= 0)
        {
            foreach(ContactPoint2D obj in collision.contacts)
            {                    
                if (obj.collider.gameObject.CompareTag(TagManager.T_PLAYER) == true)
                {
                    if (hero == null)
                        hero = obj.collider.gameObject.GetComponent<HeroController>();

                    hero.TakeDamage(physicAttack, magicAttack);
                    delayAttack = speedAttack;
                }

                if (obj.collider.gameObject.CompareTag(TagManager.T_SQUAD) == true)
                {
                    obj.collider.gameObject.GetComponent<UnitController>().TakeDamage(physicAttack, magicAttack);
                    delayAttack = speedAttack;
                }
            }
        }            
               
    }

    public void TakeDamage(float physicalDamage, float magicDamage, Vector3 forceDirection)
    {
        enemySprite.color = damageColor;
        Invoke("ColorBack", blinkTime);

        //TODO: we need to create some damage formula
        float damage = physicalDamage + magicDamage;
        currentHealth -= damage;

        ShowDamage(damage, colorDamage);

        if (forceDirection != Vector3.zero) PushMe(forceDirection);

        if (currentHealth <= 0) Dead();
   
    }

    private void ColorBack()
    {
        enemySprite.color = normalColor;
    }

    private void PushMe(Vector3 direction)
    {
        Vector3 force = (transform.position - direction).normalized * pushForce;
        rbEnemy.AddForce(force, ForceMode2D.Impulse);
    }

    private void ShowDamage(float damageValue, Color colorDamage)
    {
        GameObject damageObject = GlobalStorage.instance.objectsPoolManager.GetObjectFromPool(ObjectPool.DamageText);
        damageObject.transform.position = transform.position;
        damageObject.SetActive(true);
        damageObject.GetComponent<DamageText>().Iniatilize(damageValue, colorDamage);
    }

    private void Dead()
    {
        currentHealth = health;
        gameObject.SetActive(false);
    }

}
