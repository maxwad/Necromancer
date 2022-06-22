using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static NameManager;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitsTypes unitType;
    [SerializeField] private float health;
    [SerializeField] public float magicAttack;
    [SerializeField] public float physicAttack;
    [SerializeField] private float magicDefence;
    [SerializeField] private float physicDefence;
    [SerializeField] public float speedAttack;
    [SerializeField] public float size;
    [SerializeField] public float level;
    [SerializeField] public UnitsAbilities unitAbility;

    [SerializeField] public GameObject attackTool;

    [SerializeField] private int quantity;

    private float currentHealth;

    private bool isDead = false;

    private SpriteRenderer unitSprite;
    private Color normalColor;
    private Color damageColor = Color.red;
    private Color damageTextColor = Color.yellow;

    [SerializeField] GameObject damageNote;
    private TMP_Text damageText;
    private float smallWaitTime = 0.01f;
    private float bigWaitTime = 0.5f;
    private WaitForSeconds smallWait;
    private WaitForSeconds bigWait;
    private Vector3 damageNoteStartOffset = new Vector3(0, 0.5f, 0);
    private Vector3 damageNoteScaleOffset;
    private Vector3 damageNotePositionOffset;

    private void Start()
    {
        currentHealth = quantity > 0 ? health : 0;
        unitSprite = GetComponent<SpriteRenderer>();
        normalColor = unitSprite.color;

        smallWait = new WaitForSeconds(smallWaitTime);
        bigWait = new WaitForSeconds(bigWaitTime);
        damageNoteScaleOffset = new Vector3(smallWaitTime, smallWaitTime, smallWaitTime);
        damageNotePositionOffset = new Vector3(0, smallWaitTime, 0);
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


    #region Damage
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.T_ENEMY) == true && isDead != true)
        {
            unitSprite.color = damageColor;   
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
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

        StartCoroutine(ShowDamage(damage));

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

    private IEnumerator ShowDamage(float damage)
    {

        GameObject flyDamage = Instantiate(damageNote, transform.position + damageNoteStartOffset, Quaternion.identity);
        damageText = flyDamage.GetComponent<TMP_Text>();
        damageText.color = damageTextColor;
        damageText.text = damage.ToString();

        float heigth = 1;

        while (heigth > 0)
        {
            heigth -= 0.01f;

            flyDamage.transform.position += damageNotePositionOffset;

            yield return smallWait;
        }

        float minScale = 0f;
        float maxScale = 1.25f;
        float currentScale = 1f;

        while (currentScale < maxScale)
        {
            currentScale += smallWaitTime;
            flyDamage.transform.localScale += damageNoteScaleOffset;

            yield return smallWait;
        }

        yield return bigWait;

        while (currentScale > minScale)
        {
            currentScale -= smallWaitTime * 2;
            flyDamage.transform.localScale -= damageNoteScaleOffset * 2;

            yield return smallWait;
        }

        Destroy(flyDamage);

    }

    private void Dead()
    {
        isDead = true;
        Debug.Log("DEAD");
        Destroy(gameObject);
    }

    #endregion


    #region For Army updating
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

    #endregion
}
