using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static NameManager;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public bool isBoss = false;
    private Enemy originalStats;

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

    private float maxDamage = 200;
    private SpriteRenderer enemySprite;
    private Color normalColor;
    private Color damageColor = Color.black;
    private float blinkTime = 0.1f;

    [SerializeField] private GameObject damageNote;
    private Color colorDamage = Color.red;

    [SerializeField] private GameObject deathPrefab; 

    private HeroController hero;
    private Rigidbody2D rbEnemy;
    private float pushForce = 4000f;

    public BonusType bonusType;
    private BonusType alternativeBonusType = BonusType.Gold;

    private void Awake()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        hero = GlobalStorage.instance.hero;

        currentHealth = health;
        delayAttack = speedAttack;
        enemySprite = GetComponent<SpriteRenderer>();
        normalColor = enemySprite.color;
    }

    private void Update()
    {
        delayAttack -= Time.deltaTime;

        if(isBoss)
        {
           // Debug.Log(exp);
        }
        
    }

    public void Initialize(Enemy stats = null)
    {
        if(originalStats == null) originalStats = stats;

        enemiesType   = originalStats.EnemiesType;

        health        = originalStats.health;
        magicAttack   = originalStats.magicAttack;
        physicAttack  = originalStats.physicAttack;
        magicDefence  = originalStats.magicDefence;
        physicDefence = originalStats.physicDefence;
        speedAttack   = originalStats.speedAttack;
        size          = originalStats.size;

        EnemyAbility  = originalStats.EnemyAbility;
        attackTool    = originalStats.attackTool;

        exp           = originalStats.exp;
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
        if(currentHealth > 0)
        {
            enemySprite.color = damageColor;
            Invoke("ColorBack", blinkTime);

            //TODO: we need to create some damage formula
            float damage = Mathf.Round(physicalDamage + magicDamage);
            currentHealth -= damage;

            ShowDamage(damage, colorDamage);

            if(forceDirection != Vector3.zero) PushMe(forceDirection, pushForce);

            if(currentHealth <= 0) Dead();
        }   
    }

    public void Kill()
    {
        float damage = currentHealth > maxDamage ? maxDamage : currentHealth;
        TakeDamage(damage, damage, Vector3.zero);
    }

    private void ColorBack()
    {
        enemySprite.color = normalColor;
    }

    public void PushMe(Vector3 direction, float force)
    {
        Vector3 kickForce = (transform.position - direction).normalized * force;
        rbEnemy.AddForce(kickForce, ForceMode2D.Force);
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
        GameObject death = Instantiate(deathPrefab, transform.position, Quaternion.identity);
        death.transform.SetParent(GlobalStorage.instance.effectsContainer.transform);
        CreateBonus();

        EventManager.OnEnemyDestroyedEvent(gameObject);

        if(isBoss == true)
        {
            ReturnBossToOrdinaryEnemy();
            //some event
        }

        //reset enemy before death
        gameObject.GetComponent<EnemyMovement>().MakeMeFixed(false);
        gameObject.SetActive(false);
    }

    private void CreateBonus()
    {
        if(GlobalStorage.instance.isEnoughTempExp == true && bonusType == BonusType.TempExp)
        {
            GlobalStorage.instance.bonusManager.CreateBonus(alternativeBonusType, transform.position, exp);
            return;
        }

        GlobalStorage.instance.bonusManager.CreateBonus(bonusType, transform.position, exp);
    }

    public void MakeBoss() 
    {
        isBoss                = true;
        currentHealth        *= 150;
        magicAttack          *= 3;
        physicAttack         *= 3;
        transform.localScale *= 2;
        rbEnemy.mass         *= 2;
        exp                  *= 50;
    }    

    private void ReturnBossToOrdinaryEnemy()
    {
        isBoss                = false;
        currentHealth        /= 150;
        magicAttack          /= 3;
        physicAttack         /= 3;
        transform.localScale /= 2;
        rbEnemy.mass         /= 2;
        exp                  /= 50;
    }

    private void OnDisable()
    {
        if(isBoss == true) ReturnBossToOrdinaryEnemy();
    }
}
