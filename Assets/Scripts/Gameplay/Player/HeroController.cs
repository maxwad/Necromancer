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

    [SerializeField] private SpriteRenderer healthBar;

    [SerializeField] GameObject damageNote;
    private TMP_Text damageText;
    private Color damageTextColor = new Color(1f, 0.45f, 0.03f, 1);
    private float smallWaitTime = 0.01f;
    private float bigWaitTime = 0.5f;
    private WaitForSeconds smallWait;
    private WaitForSeconds bigWait;
    private Vector3 damageNoteStartOffset = new Vector3(0, 0.5f, 0);
    private Vector3 damageNoteScaleOffset;
    private Vector3 damageNotePositionOffset;

    private void Start()
    {
        currentHealth = health;
        unitSprite = GetComponent<SpriteRenderer>();
        normalColor = unitSprite.color;

        smallWait = new WaitForSeconds(smallWaitTime);
        bigWait = new WaitForSeconds(bigWaitTime);
        damageNoteScaleOffset = new Vector3(smallWaitTime, smallWaitTime, smallWaitTime);
        damageNotePositionOffset = new Vector3(0, smallWaitTime, 0);

        UpdateHealthBar();
    }
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
        Debug.Log("HERO IS DEAD");
        gameObject.SetActive(false);
    }
}
