using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class PlayersArmy : MonoBehaviour
{
    private UnitManager unitManager;

    public UnitsTypes[] playersArmyEnums;

    public Unit[] playersArmy = new Unit[4];


    public PlayersArmyWindow playersArmyWindow;
    private int firstIndexForReplaceUnit = -1;
    private int secondIndexForReplaceUnit = -1;

    private void Start()
    {
        unitManager = GlobalStorage.instance.unitManager.GetComponent<UnitManager>();

        playersArmy = unitManager.GetUnitsForPlayersArmy(playersArmyEnums);

        Debug.Log("����� � �����:");
        foreach (var item in playersArmy)
        {
            if (item != null)
            {
                item.quantity = 4;
                item.commonHealth = item.quantity * item.health;
                Debug.Log(item.unitName + " �� ���� " + item.coinsPrice + " � ����� ��������� � " + item.commonHealth + " ������.");
                
            }           
        }
        playersArmyWindow.CreateArmyScheme(playersArmy);
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
