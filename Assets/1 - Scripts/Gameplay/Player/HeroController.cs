using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static NameManager;

public class HeroController : MonoBehaviour
{
    [SerializeField] private float maxHealthBase = 500f;
    [SerializeField] private float maxCurrentHealth;
    [SerializeField] private float healthBoost = 0;
    [SerializeField] private float currentHealth;

    [SerializeField] private float maxManaBase = 50f;
    [SerializeField] private float maxCurrentMana;
    [SerializeField] private float manaBoost = 0;
    [SerializeField] private float currentMana;

    //[SerializeField] private float magicAttack = 1f;
    //[SerializeField] private float physicAttack = 1f;
    //[SerializeField] private float magicDefence = 1f;
    //[SerializeField] private float physicDefence = 1f;
    //[SerializeField] private float speedAttack = 1f;
    //[SerializeField] private float size = 1f;
    //[SerializeField] private float level = 1f;
    //[SerializeField] private UnitsAbilities unitAbility;

    //[SerializeField] private GameObject attackTool;

    private float searchRadius = 2f;
    private float boostSearchRadius = 0;

    private bool isDead = false;

    private SpriteRenderer unitSprite;
    private Color normalColor;
    private Color damageColor = Color.red;
    private float blinkTime = 0.1f;

    [SerializeField] private GameObject deathPrefab;

    [SerializeField] private SpriteRenderer healthBar;

    [SerializeField] GameObject damageNote;
    private Color colorDamage = new Color(1f, 0.45f, 0.03f, 1);

    private void Start()
    {
        ResetStartParameters();
        unitSprite = GetComponent<SpriteRenderer>();
        normalColor = unitSprite.color;

        UpdateHealthBar();
    }

    private void Update()
    {
        CheckBonuses();
    }

    private void ResetStartParameters()
    {
        maxCurrentHealth = maxHealthBase + (maxHealthBase * healthBoost);
        currentHealth = maxCurrentHealth;

        maxCurrentMana = maxManaBase + (maxManaBase * manaBoost);
        currentMana = maxCurrentMana; 
    }

    private void LoadParameters()
    {

    }

    private void CheckBonuses()
    {
        float radius = searchRadius + (boostSearchRadius * searchRadius);
        Collider2D[] findedObjects = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(var col in findedObjects)
        {
            BonusController bonus = col.GetComponent<BonusController>();
            if(bonus != null)
            {
                bonus.ActivatateBonus();
            }
        }
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
            currentHealth / maxCurrentHealth, 
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
        GameObject death = Instantiate(deathPrefab, transform.position, Quaternion.identity);
        death.transform.SetParent(GlobalStorage.instance.effectsContainer.transform);
        gameObject.SetActive(false);
    }

    private void AddHealth(BonusType type, float value)
    {
        if(type == BonusType.Health && isDead == false)
        {

            if(currentHealth + value > maxCurrentHealth)
                currentHealth = maxCurrentHealth;
            else
                currentHealth += value;

            UpdateHealthBar();
        }
    }

    private void AddMana(BonusType type, float value)
    {
        if(type == BonusType.Mana && isDead == false)
        {
            if(currentMana + value > maxCurrentMana)
                currentMana = maxCurrentMana;
            else
                currentMana += value;

            Debug.Log("Mana = " + currentMana);
        }
    }

    private void OnEnable()
    {
        EventManager.BonusPickedUp += AddHealth;
        EventManager.BonusPickedUp += AddMana;
    }

    private void OnDisable()
    {
        EventManager.BonusPickedUp -= AddHealth;
        EventManager.BonusPickedUp -= AddMana;
    }
}
