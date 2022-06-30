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

    private GameObject currentBonus;

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

        if (currentBonus.GetComponent<BonusController>().isFromPoolObject == true)
        {            
            GameObject enemy = GlobalStorage.instance.objectsPoolManager.GetObjectFromPool(ObjectPool.BonusExp);
            enemy.transform.position = position;
            enemy.SetActive(true);
        }
        else
        {
            GameObject bonus = Instantiate(currentBonus, position, Quaternion.identity);
            bonus.transform.SetParent(GlobalStorage.instance.bonusesContainer.transform);
        }        
    }
}
