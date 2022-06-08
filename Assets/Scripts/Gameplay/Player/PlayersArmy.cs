using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static NameManager;

public class PlayersArmy : MonoBehaviour
{
    private UnitManager unitManager;

    [SerializeField] private UnitsTypes[] playersArmyEnums;
    [SerializeField] private Unit[] playersArmy = new Unit[4];

    [SerializeField] private PlayersArmyWindow playersArmyWindow;
    private int firstIndexForReplaceUnit = -1;
    private int secondIndexForReplaceUnit = -1;

    private void Start()
    {
        unitManager = GlobalStorage.instance.unitManager;

        playersArmy = unitManager.GetUnitsForPlayersArmy(playersArmyEnums);

        Debug.Log("»того в армии:");
        foreach (var item in playersArmy)
        {
            if (item != null)
            {
                item.quantity = 4;
                item.commonHealth = item.quantity * item.health;
                Debug.Log(item.unitName + " по цене " + item.coinsPrice + " с общим здоровьем в " + item.commonHealth + " единиц.");
                
            }           
        }


        EventManager.OnPlayersArmyIsReadyEvent(playersArmy);
        //playersArmyWindow.CreateArmyScheme(playersArmy);
    }

    public void UnitsReplacement(int index)
    {
        if (firstIndexForReplaceUnit == -1)
        {
            firstIndexForReplaceUnit = index;
        }
        else if (index == firstIndexForReplaceUnit)
        {
            ResetReplaceIndexes();
        }
        else
        {
            secondIndexForReplaceUnit = index;

            Unit oldUnit = playersArmy[firstIndexForReplaceUnit];
            playersArmy[firstIndexForReplaceUnit] = playersArmy[secondIndexForReplaceUnit];
            playersArmy[secondIndexForReplaceUnit] = oldUnit;

            playersArmyWindow.CreateArmyScheme(playersArmy);
            ResetReplaceIndexes();
        }
    }

    public void ResetReplaceIndexes()
    {
        firstIndexForReplaceUnit = -1;
        secondIndexForReplaceUnit = -1;
    }
}
