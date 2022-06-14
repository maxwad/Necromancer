using System;
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
    [SerializeField] private GameObject[] realUnitsOnBattlefield = new GameObject[4];

    [SerializeField] private BattlePlayerController battlePlayerController;

    private void Start()
    {
        unitManager = GlobalStorage.instance.unitManager;

        playersArmy = unitManager.GetUnitsForPlayersArmy(playersArmyEnums);

        //Debug.Log("»того в армии:");
        foreach (var item in playersArmy)
        {
            if (item != null)
            {
                item.quantity = 4;
                item.commonHealth = item.quantity * item.health;
                //Debug.Log(item.unitName + " по цене " + item.coinsPrice + " с общим здоровьем в " + item.commonHealth + " единиц.");                
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

            playersArmyWindow.CreateArmyScheme(playersArmy);
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
        //clear units
        for (int i = 0; i < realUnitsOnBattlefield.Length; i++)
        {
            if (realUnitsOnBattlefield[i] != null)
            {
                Destroy(realUnitsOnBattlefield[i]);
            }
        }

        //create new units
        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] != null)
            {
                GameObject unit = Instantiate(army[i].unitGO, (Vector3)playersArmyPositions[i] + battlePlayerController.gameObject.transform.position, Quaternion.identity);
                unit.transform.SetParent(battlePlayerController.gameObject.transform);
                //unit.transform.SetParent(playersArmyPositions[i].transform);
                //unit.GetComponentInChildren<TMP_Text>().text = army[i].quantity.ToString();
                realUnitsOnBattlefield[i] = unit;
            }
        }

        battlePlayerController.GetArmy(realUnitsOnBattlefield);
    }

    private void OnEnable()
    {
        EventManager.PlayersArmyIsReady += CreateArmyOnBattlefield;
    }

    private void OnDisable()
    {
        EventManager.PlayersArmyIsReady -= CreateArmyOnBattlefield;
    }
}
