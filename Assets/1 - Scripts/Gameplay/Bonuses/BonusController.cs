using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;
using static UnityEngine.GraphicsBuffer;

public class BonusController : MonoBehaviour
{
    [SerializeField] private BonusType bonusType;
    [SerializeField] private float value;
    public bool isFromPoolObject = false;

    private GameObject player;
    private bool isActivate = false;
    private float speed = 40f;
    private float inertion = -50f;
    private float currentInertion;

    private void Start()
    {
        player = GlobalStorage.instance.hero;
        currentInertion = inertion;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_PLAYER))
        {
            EventManager.OnBonusPickedUpEvent(bonusType, value);
            isActivate = false;
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
        while(player.activeInHierarchy == true && isActivate == true)
        {
            float step = (speed + currentInertion) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            if(currentInertion < 0) currentInertion += 0.5f;

            yield return new WaitForSeconds(0.01f);
        }
    }

    private void DestroyMe()
    {
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

    private void OnEnable()
    {
        EventManager.EndOfBattle += DestroyMe;
    }

    private void OnDisable()
    {
        EventManager.EndOfBattle -= DestroyMe;
    }
}
