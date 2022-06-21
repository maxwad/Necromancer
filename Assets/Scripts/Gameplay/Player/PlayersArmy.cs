using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static NameManager;

public class PlayersArmy : MonoBehaviour
{
    private UnitManager unitManager;

    [SerializeField] private UnitsTypes[] playersArmyEnums;
    [SerializeField] private Unit[] playersArmy = new Unit[4];

    [SerializeField] private PlayersArmyWindow playersArmyWindow;
    private int firstIndexForReplaceUnit = -1;
    private int secondIndexForReplaceUnit = -1;

    [SerializeField] private Vector2[] playersArmyPositions;
    private GameObject[] realUnitsOnBattlefield = new GameObject[4];

    [SerializeField] private BattleArmyController battleArmyController;

    private void  InitializeArmy()
    {
        unitManager = GlobalStorage.instance.unitManager;
        playersArmy = unitManager.GetUnitsForPlayersArmy(playersArmyEnums);

        foreach (var item in playersArmy)
        {
            if (item != null)
            {
                item.quantity = Random.Range(4, 10);
                item.commonHealth = item.quantity * item.health;              
            }
        }

        EventManager.OnPlayersArmyIsReadyEvent(playersArmy);
    }

    public void UnitsReplacementUI(int index)
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

            CreateArmyOnBattlefield(playersArmy);

            ResetReplaceIndexes();
        }
    }

    public void ResetReplaceIndexes()
    {
        firstIndexForReplaceUnit = -1;
        secondIndexForReplaceUnit = -1;
    }

    private void CreateArmyOnBattlefield(Unit[] army)
    {
        int currentArmyCount = 0;
        //check units
        for (int i = 0; i < realUnitsOnBattlefield.Length; i++)
        {
            if (realUnitsOnBattlefield[i] != null)
            {
                currentArmyCount++;
            }
        }

        //create new units if army on battlefield is empty
        if (currentArmyCount == 0)
        {
            for (int i = 0; i < army.Length; i++)
            {
                if (army[i] != null)
                {
                    GameObject unit = Instantiate(army[i].unitGO, (Vector3)playersArmyPositions[i] + battleArmyController.gameObject.transform.position, Quaternion.identity);
                    unit.transform.SetParent(battleArmyController.gameObject.transform);
                    unit.GetComponentInChildren<TMP_Text>().text = army[i].quantity.ToString();
                    unit.GetComponent<UnitController>().Initilize(army[i]);
                    realUnitsOnBattlefield[i] = unit;
                }
            }
        }
        //replacement units if army on battlefield is ready
        else
        {
            if (firstIndexForReplaceUnit != -1 && secondIndexForReplaceUnit != -1)
            {
                GameObject firstUnit = realUnitsOnBattlefield[firstIndexForReplaceUnit];
                realUnitsOnBattlefield[firstIndexForReplaceUnit] = realUnitsOnBattlefield[secondIndexForReplaceUnit];
                realUnitsOnBattlefield[secondIndexForReplaceUnit] = firstUnit;

                if (realUnitsOnBattlefield[firstIndexForReplaceUnit] != null)
                    realUnitsOnBattlefield[firstIndexForReplaceUnit].transform.position = (Vector3)playersArmyPositions[firstIndexForReplaceUnit] + battleArmyController.gameObject.transform.position;

                if (realUnitsOnBattlefield[secondIndexForReplaceUnit] != null)
                    realUnitsOnBattlefield[secondIndexForReplaceUnit].transform.position = (Vector3)playersArmyPositions[secondIndexForReplaceUnit] + battleArmyController.gameObject.transform.position;
            }            
        }

        playersArmyWindow.CreateArmyScheme(playersArmy);
        battleArmyController.GetArmy(realUnitsOnBattlefield);
    }


    public void UpgradeArmy(UnitsTypes unitType, int quantity)
    {
        for (int i = 0; i < playersArmy.Length; i++)
        {
            if (playersArmy[i]?.UnitType == unitType)
            {
                if (quantity == 0)
                {
                    playersArmy[i] = null;
                }
                else 
                {
                    playersArmy[i].quantity = quantity;
                }                
                EventManager.OnPlayersArmyIsReadyEvent(playersArmy);
                break;
            }
        }
    }

    private void OnEnable()
    {
        EventManager.AllUnitsIsReady += InitializeArmy;
        EventManager.PlayersArmyIsReady += CreateArmyOnBattlefield;
        EventManager.WeLostOneUnit += UpgradeArmy;
    }

    private void OnDisable()
    {
        EventManager.AllUnitsIsReady -= InitializeArmy;
        EventManager.PlayersArmyIsReady -= CreateArmyOnBattlefield;
        EventManager.WeLostOneUnit -= UpgradeArmy;
    }
}
