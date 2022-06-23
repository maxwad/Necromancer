using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private float blinkTime = 0.1f;

    [SerializeField] private SpriteRenderer healthBar;

    [SerializeField] GameObject damageNote;
    private Color colorDamage = new Color(1f, 0.45f, 0.03f, 1);

    private void Start()
    {
        currentHealth = health;
        unitSprite = GetComponent<SpriteRenderer>();
        normalColor = unitSprite.color;

        UpdateHealthBar();
    }

    #region DAMAGE
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            unitSprite.color = damageColor;
            Invoke("ColorBack", blinkTime);
        }
    }

    private void ColorBack()
    {
        unitSprite.color = normalColor;
    }

    public void TakeDamage(float physicalDamage, float magicDamage)
    {
        //TODO: we need to create some damage formula
        float damage = physicalDamage + magicDamage;
        currentHealth -= damage;

        ShowDamage(damage, colorDamage);
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

    private void ShowDamage(float damageValue, Color colorDamage)
    {
        GameObject damageObject = GlobalStorage.instance.objectsPoolManager.GetObjectFromPool(ObjectPool.DamageText);
        damageObject.transform.position = transform.position;
        damageObject.SetActive(true);
        damageObject.GetComponent<DamageText>().Iniatilize(damageValue, colorDamage);
    }

    #endregion

    private void Dead()
    {
        isDead = true;
        Debug.Log("HERO IS DEAD");
        gameObject.SetActive(false);
    }
}
