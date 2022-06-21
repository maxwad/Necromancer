using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemiesTypes enemiesType;
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

    private bool isDead = false;

    private SpriteRenderer enemySprite;
    private Color normalColor;
    private Color damageColor = Color.black;

    private HeroController hero;

    private void Start()
    {
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //TODO: check depending of time
        if (isDead != true)
        {
            if (delayAttack <= 0)
            {
                if (collision.CompareTag(TagManager.T_PLAYER) == true)
                {
                    if (hero == null)
                        hero = collision.gameObject.GetComponent<HeroController>();

                    hero.TakeDamage(physicAttack, magicAttack);
                    delayAttack = speedAttack;
                }

                if (collision.CompareTag(TagManager.T_SQUAD) == true)
                {
                    collision.gameObject.GetComponent<UnitController>().TakeDamage(physicAttack, magicAttack);
                    delayAttack = speedAttack;
                }
            }

            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            enemySprite.color = normalColor;
        }
    }

    public void TakeDamage(int physicalDamage, int magicDamage)
    {
        //TODO: we need to create some damage formula
        int damage = physicalDamage + magicDamage;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Dead();
        }

    }
    private void Dead()
    {
        isDead = true;
        Debug.Log(enemiesType + " IS DEAD");
        gameObject.SetActive(false);
    }

}
