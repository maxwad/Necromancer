using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class InfirmaryManager : MonoBehaviour
{
    private float currentCapacity;

    public List<UnitsTypes> injuredList = new List<UnitsTypes>();


    private void SetInfarmaryBoost(PlayersStats type, float value)
    {
        if(type == PlayersStats.Infirmary) currentCapacity = value;
    }

    private void AddUnitToInfirmary(UnitsTypes unitType, int quantity)
    {
        if(injuredList.Count < currentCapacity) 
        {
            injuredList.Add(unitType);
            EventManager.OnUpdateInfirmaryUIEvent(unitType, (float)injuredList.Count, currentCapacity);
        }
    }

    private void RemoveUnitToInfirmary(UnitsTypes unitType, int quantity)
    {
        injuredList.Remove(unitType);
        EventManager.OnUpdateInfirmaryUIEvent(unitType, (float)injuredList.Count, currentCapacity);
        Debug.Log(injuredList.Count);
    }


    private void OnEnable()
    {
        EventManager.UpgradePlayerStat += SetInfarmaryBoost;
        EventManager.WeLostOneUnit += AddUnitToInfirmary;
    }

    private void OnDisable()
    {
        EventManager.UpgradePlayerStat -= SetInfarmaryBoost;
        EventManager.WeLostOneUnit -= AddUnitToInfirmary;
    }
}
