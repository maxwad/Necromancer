using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    [SerializeField] private GameObject damageNote;
    private TMP_Text damageText;
    private float smallWaitTime = 0.01f;
    private float bigWaitTime = 0.5f;
    private WaitForSeconds smallWait;
    private WaitForSeconds bigWait;
    private Vector3 damageNoteStartOffset = new Vector3(0, 0.5f, 0);
    private Vector3 damageNoteOffset;

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

        smallWait = new WaitForSeconds(smallWaitTime);
        bigWait = new WaitForSeconds(bigWaitTime);
        damageNoteOffset = new Vector3(smallWaitTime, smallWaitTime, smallWaitTime);
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
        if (isDead != true)
        {
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDead != true)
        {
            enemySprite.color = normalColor;
        }
    }

    public void TakeDamage(float physicalDamage, float magicDamage, Vector3 forceDirection)
    {
        if (isDead != true)
        {
            enemySprite.color = damageColor;
            //TODO: we need to create some damage formula
            float damage = physicalDamage + magicDamage;
            currentHealth -= damage;

            StartCoroutine(ShowDamage(damage));
            if (forceDirection != Vector3.zero) PushMe(forceDirection);

            if (currentHealth <= 0) Dead();

        }      
    }

    private void PushMe(Vector3 direction)
    {
        Vector3 force = (transform.position - direction).normalized * pushForce;
        rbEnemy.AddForce(force, ForceMode2D.Impulse);
    }

    private IEnumerator ShowDamage(float damage)
    {

        GameObject flyDamage = Instantiate(damageNote, transform.position + damageNoteStartOffset, Quaternion.identity);
        damageText = flyDamage.GetComponent<TMP_Text>();
        damageText.text = damage.ToString();

        //float alfa = 1;
        //Color currentColor = damageText.color;

        //while (alfa > 0)
        //{
        //    alfa -= 0.02f;
        //    currentColor.a = alfa;
        //    damageText.color = currentColor;

        //    flyDamage.transform.position += damageNoteOffset;

        //    yield return smallWait;
        //}

        float minScale = 0f;
        float maxScale = 1.25f;
        float currentScale = 1f;

        while (currentScale < maxScale)
        {
            currentScale += smallWaitTime;
            flyDamage.transform.localScale += damageNoteOffset;

            yield return smallWait;
        }

        yield return bigWait;

        while (currentScale > minScale)
        {
            currentScale -= smallWaitTime * 2;
            flyDamage.transform.localScale -= damageNoteOffset * 2;

            yield return smallWait;
        }

        Destroy(flyDamage);

    }

    private void Dead()
    {
        isDead = true;
        Debug.Log(enemiesType + " IS DEAD");
        gameObject.SetActive(false);
    }

}
