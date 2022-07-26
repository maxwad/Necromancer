using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;
using static UnityEngine.GraphicsBuffer;

public class BonusController : MonoBehaviour
{
    public BonusType bonusType;
    [SerializeField] private float baseValue;
    [SerializeField] private float originalBaseValue;
    public float value;
    public bool isFromPoolObject = false;

    private GameObject player;
    private bool isActivate = false;
    private float speed = 40f;
    private float inertion = -50f;
    private float currentInertion;

    private void Awake()
    {
        currentInertion = inertion;
        value = baseValue;
        originalBaseValue = baseValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_PLAYER))
        {
            EventManager.OnBonusPickedUpEvent(bonusType, value);
            DestroyMe();
        }
    }

    public void ActivatateBonus()
    {
        if(isActivate == false)
        {
            isActivate = true;
            StartCoroutine("ToThePlayer");            
        }
        
    }

    private IEnumerator ToThePlayer()
    {
        player = GlobalStorage.instance.hero.gameObject;
        while(player.activeInHierarchy == true && isActivate == true)
        {
            float step = (speed + currentInertion) * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            if(currentInertion < 0) currentInertion += 0.05f;

            yield return new WaitForSeconds(0.001f);
        }
    }

    public void DestroyMe()
    {
        isActivate = false;
        GlobalStorage.instance.bonusManager.bonusesOnTheMap.Remove(gameObject);
        ResetBonusValue();

        if (isFromPoolObject == true)
        {
            currentInertion = inertion;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BoostBonusValue(float boost)
    {
        value = baseValue + (baseValue * boost);
    }

    public void SetBonusValue(float newValue)
    {
        baseValue = newValue;
    }

    private void ResetBonusValue()
    {
        baseValue = originalBaseValue;
        value = originalBaseValue;
    }

    private void OnEnable()
    {
        EventManager.EndOfBattle += DestroyMe;
    }

    private void OnDisable()
    {
        EventManager.EndOfBattle -= DestroyMe;
    }
}
