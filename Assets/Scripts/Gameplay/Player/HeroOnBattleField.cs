using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class HeroOnBattleField : MonoBehaviour
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

    private float currentHealth;
    private bool isDead = false;

    private SpriteRenderer unitSprite;
    private Color normalColor;
    private Color damageColor = Color.red;

    private void Start()
    {
        currentHealth = health;
        unitSprite = GetComponent<SpriteRenderer>();
        normalColor = unitSprite.color;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            unitSprite.color = damageColor;

            currentHealth--;

            if (currentHealth <= 0)
                Dead();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            unitSprite.color = normalColor;
        }
    }

    private void Dead()
    {
        isDead = true;
        Debug.Log("HERO IS DEAD");
        gameObject.SetActive(false);
    }
}
