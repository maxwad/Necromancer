using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class HeroController : MonoBehaviour
{
    [SerializeField] private float health = 500f;
    //[SerializeField] private float magicAttack = 1f;
    //[SerializeField] private float physicAttack = 1f;
    //[SerializeField] private float magicDefence = 1f;
    //[SerializeField] private float physicDefence = 1f;
    //[SerializeField] private float speedAttack = 1f;
    //[SerializeField] private float size = 1f;
    //[SerializeField] private float level = 1f;
    //[SerializeField] private UnitsAbilities unitAbility;

    //[SerializeField] private GameObject attackTool;

    [SerializeField] private float currentHealth;
    private bool isDead = false;

    private SpriteRenderer unitSprite;
    private Color normalColor;
    private Color damageColor = Color.red;

    [SerializeField] private SpriteRenderer healthBar;

    private void Start()
    {
        currentHealth = health;
        unitSprite = GetComponent<SpriteRenderer>();
        normalColor = unitSprite.color;

        UpdateHealthBar();
    }
    private void OnCollisionStay2D(Collision2D collision)
    //private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            unitSprite.color = damageColor;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    //private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            unitSprite.color = normalColor;
        }
    }

    public void TakeDamage(float physicalDamage, float magicDamage)
    {
        //TODO: we need to create some damage formula
        float damage = physicalDamage + magicDamage;
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(
            currentHealth / health, 
            healthBar.transform.localScale.y, 
            healthBar.transform.localScale.z
            );
    }

    private void Dead()
    {
        isDead = true;
        Debug.Log("HERO IS DEAD");
        gameObject.SetActive(false);
    }
}
