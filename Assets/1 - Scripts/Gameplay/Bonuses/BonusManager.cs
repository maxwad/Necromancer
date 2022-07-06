using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class BonusManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject mana;
    [SerializeField] private GameObject gold;
    [SerializeField] private GameObject tempExp;

    private float bonusBoost = 0;
    private GameObject currentBonus;
    public List<GameObject> bonusesOnTheMap = new List<GameObject>();

    public void CreateBonus(BonusType type, Vector3 position)
    {
        switch (type)
        {
            case BonusType.Health:
                currentBonus = health;
                break;

            case BonusType.Mana:
                currentBonus = mana;
                break;

            case BonusType.Gold:
                currentBonus = gold;
                break;

            case BonusType.TempExp:
                currentBonus = tempExp;
                break;

            case BonusType.Other:
                break;

            case BonusType.Nothing:
                return;

            default:
                return;
        }

        GameObject bonus;

        if (currentBonus.GetComponent<BonusController>().isFromPoolObject == true)
        {
            ObjectPool objectPoolType = ObjectPool.BonusExp;
            if(type == BonusType.Gold) objectPoolType = ObjectPool.BonusGold;

            bonus = GlobalStorage.instance.objectsPoolManager.GetObjectFromPool(objectPoolType);
            bonus.transform.position = position;
            bonus.SetActive(true);
        }
        else
        {
            bonus = Instantiate(currentBonus, position, Quaternion.identity);
            bonus.transform.SetParent(GlobalStorage.instance.bonusesContainer.transform);
        }

        bonus.GetComponent<BonusController>().BoostBonusValue(bonusBoost);

        bonusesOnTheMap.Add(bonus);
    }

    private void ClearBonusList(bool mode)
    {
        if(mode == true) bonusesOnTheMap.Clear();
    }

    public void BoostBonus(float value)
    {
        bonusBoost = value;
    }

    private void OnEnable()
    {
        EventManager.ChangePlayer += ClearBonusList;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayer -= ClearBonusList;
    }
}
