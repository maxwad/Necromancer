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

    private void Start()
    {
        boostManager = GlobalStorage.instance.unitBoostManager;

        StartCreatingPlayersArmy();
    }

    #region Start army creation 
    private void StartCreatingPlayersArmy()
    {
        GetCurrentLevelOfUnits();
        GetAllUnitsBase();
    }

    private void GetCurrentLevelOfUnits()
    {
        //��������� ������ ��������� ������� ���� ������
        foreach (UnitsTypes item in Enum.GetValues(typeof(UnitsTypes)))
            currentLevelOfUnitsDict.Add(item, 1);
    }

    private void GetAllUnitsBase()
    {
        //��������� ��� ����������������� � ���� � �������� ��� ����� ������� ����������
        foreach (UnitSO item in allUnitsSO)
            allUnitsBase.Add(new Unit(item));

        GetAllCurrentBaseUnitsByTypes();
    }

    private void GetAllCurrentBaseUnitsByTypes()
    {
        //��������� ������ �� ������ (�� ������ ������� ����)
        foreach (UnitsTypes itemType in Enum.GetValues(typeof(UnitsTypes)))
        {
            foreach (var itemUnit in allUnitsBase)
            {
                if (itemUnit.UnitType == itemType && itemUnit.level == 1)
                {
                    allCurrentBaseUnitsByTypes.Add(itemUnit);
                    break;
                }
            }
        }

        GetAllCurrentBoostUnitsByTypes();
    }

    private void GetAllCurrentBoostUnitsByTypes()
    {
        //����������� ������� �� ��� ������� �����
        foreach (var item in allCurrentBaseUnitsByTypes)
            allCurrentBoostUnitsByTypes.Add(boostManager.AddBonusStatsToUnit(item));

        EventManager.OnAllUnitsIsReadyEvent();
    }

    #endregion


    public Unit[] GetUnitsForPlayersArmy(UnitsTypes[] playersArmy)
    {
        Unit[] army = new Unit[Enum.GetValues(typeof(UnitsTypes)).Length];

        for (int i = 0; i < army.Length; i++)
        {
            if (i < playersArmy.Length)
            {
                foreach (var item in allCurrentBoostUnitsByTypes)
                {
                    if (playersArmy[i] == item.UnitType)
                    {
                        army[i] = item;
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

    public Unit GetNewSquad(UnitsTypes type)
    {
        foreach(var unit in allCurrentBoostUnitsByTypes)
        {
            if(unit.UnitType == type)
            {
                return unit;
            }
        }
        return null;
    }

    public void UpgradeUnitLevel(UnitsTypes unitType, int level)
    {
        currentLevelOfUnitsDict[unitType] = level;
    }
}
