using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitsTypes unitType;
    [SerializeField] private float health;
    [SerializeField] private float magicAttack;
    [SerializeField] private float physicAttack;
    [SerializeField] private float magicDefence;
    [SerializeField] private float physicDefence;
    [SerializeField] private float speedAttack;
    [SerializeField] private float size;
    [SerializeField] private float level;
    [SerializeField] private UnitsAbilities unitAbility;

    [SerializeField] private GameObject attackTool;

    [SerializeField] private int quantity;

    private float currentHealth;

    private bool isDead = false;

    private SpriteRenderer unitSprite;
    private Color normalColor;
    private Color damageColor = Color.red;

    private void Start()
    {
        currentHealth = quantity > 0 ? health : 0;
        unitSprite = GetComponent<SpriteRenderer>();
        normalColor = unitSprite.color;
    }

    public void Initilize(Unit unit) 
    {
        unitType = unit.UnitType;

        health        = unit.health;
        magicAttack   = unit.magicAttack;
        physicAttack  = unit.physicAttack;
        magicDefence  = unit.magicDefence;
        physicDefence = unit.physicDefence;
        speedAttack   = unit.speedAttack;
        size          = unit.size;
        level         = unit.level;

        unitAbility   = unit.UnitAbility;
        attackTool    = unit.attackTool;

        quantity      = unit.quantity;
    }

    private void OnCollisionStay2D(Collision2D collision)
    //{
        
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            unitSprite.color = damageColor;   
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    //{
        
    //}
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

        Debug.Log("Hit SQUAD by " + damage);

        if (currentHealth <= 0 && quantity > 1)
        {
            quantity--;
            currentHealth = health;
            UpdateArmy();
        }

        if (quantity <= 1 && currentHealth <= 0)
        {
            quantity--;
            UpdateArmy();
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        Debug.Log("DEAD");
        Destroy(gameObject);
    }

    public void UpdateArmy()
    {
        EventManager.OnWeLostOneUnitEvent(unitType, quantity);
    }

    public UnitsTypes GetTypeUnit()
    {
        return unitType;
    }

    public int GetQuantityUnit()
    {
        return quantity;
    }
}
