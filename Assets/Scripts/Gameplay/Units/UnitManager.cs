using System;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class UnitManager : MonoBehaviour
{
    public Dictionary<UnitsTypes, int> currentLevelOfUnitsDict = new Dictionary<UnitsTypes, int>();

    public List<UnitSO> allUnitsSO;

    public List<Unit> allUnitsBase = new List<Unit>();
    public List<Unit> allCurrentBaseUnitsByTypes = new List<Unit>();
    public List<Unit> allCurrentBoostUnitsByTypes = new List<Unit>();

    private UnitBoostManager boostManager;

    private void Awake()
    {
        boostManager = GlobalStorage.instance.boostManager.GetComponent<UnitBoostManager>();

        //формируем список начальных уровней всех юнитов
        foreach (UnitsTypes item in Enum.GetValues(typeof(UnitsTypes)))
        {
            currentLevelOfUnitsDict.Add(item, 1);
        }
        
        //переводим все скриптеблќбджекты в дикт и получаем ¬—≈ юниты базовых параметров
        foreach (UnitSO item in allUnitsSO)
        {
            allUnitsBase.Add(new Unit(item));
        }

        //формируем список из юнитов (по одному каждого типа)
        foreach (UnitsTypes itemType in Enum.GetValues(typeof(UnitsTypes)))
        {
            foreach (var itemUnit in allUnitsBase)
            {
                if (itemUnit.UnitType == itemType && itemUnit.level == 1)
                {
                    allCurrentBaseUnitsByTypes.Add(itemUnit);
                    Debug.Log(itemUnit.unitName + " was created...");
                    break;
                }
            }
        }

        foreach (var item in allCurrentBaseUnitsByTypes)
        {            
            allCurrentBoostUnitsByTypes.Add(boostManager.AddBonusStatsToUnit(item));
            Debug.Log(item.unitName + " was boosted...");
        }      

    }

    public Unit[] GetUnitsForPlayersArmy(UnitsTypes[] playersArmy)
    {
        Unit[] army = new Unit[4];

        for (int i = 0; i < army.Length; i++)
        {
            if (i < playersArmy.Length)
            {
                foreach (var item in allCurrentBoostUnitsByTypes)
                {
                    if (playersArmy[i] == item.UnitType)
                    {
                        army[i] = item;
                        Debug.Log(item.unitName + " got to the army");
                    }
                }
            }
            else
            {
                army[i] = null;
            }
        }              
        
        return army;
    }

    public void UpgradeUnitLevel(UnitsTypes unitType, int level)
    {
        currentLevelOfUnitsDict[unitType] = level;
    }
}
