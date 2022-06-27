using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class BonusController : MonoBehaviour
{
    [SerializeField] private BonusType bonusType;
    [SerializeField] private float value;
    public bool isFromPoolObject = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.T_PLAYER))
        {
            EventManager.OnBonusPickedUpEvent(bonusType, value);
            DestroyMe();
        }
    }

    private void DestroyMe()
    {
        if (isFromPoolObject == true)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
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
