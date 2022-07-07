using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static NameManager;
using System;

public class PlayersArmy : MonoBehaviour
{
    private UnitManager unitManager;

    //TODO: we need to change start parameters army in future
    [SerializeField] private UnitsTypes[] playersArmyEnums;
    [HideInInspector] public Unit[] reserveArmy;
    private List<GameObject> realUnitsInReserve = new List<GameObject>();
    private List<UnitController> realUnitsComponentsInReserve = new List<UnitController>(); 
    [HideInInspector] public Unit[] playersArmy = new Unit[4];

    [Space]
    [SerializeField] private PlayersArmyWindow playersArmyWindow;
    private int firstIndexForReplaceUnit = -1;
    private int secondIndexForReplaceUnit = -1;

    [Space]
    [SerializeField] private GameObject ReserveArmyContainer;
    [SerializeField] private Vector2[] playersArmyPositions;
    private GameObject[] realUnitsOnBattlefield = new GameObject[4];


    [Space]
    [SerializeField] private BattleArmyController battleArmyController;

    private void  InitializeArmy()
    {
        unitManager = GlobalStorage.instance.unitManager;
        reserveArmy = new Unit[Enum.GetValues(typeof(UnitsTypes)).Length];
        reserveArmy = unitManager.GetUnitsForPlayersArmy(playersArmyEnums);
        //playersArmy = unitManager.GetUnitsForPlayersArmy(playersArmyEnums);

        //TODO: delete manual quantity
        foreach (var item in reserveArmy)
        {
            if (item != null)
            {
                item.quantity = UnityEngine.Random.Range(4, 10);
                item.currentHealth = item.health;              
            }
        }

        CreateRealUnitsInReserve();

    }

    private void CreateRealUnitsInReserve()
    {
        for(int i = 0; i < reserveArmy.Length; i++)
        {
            if(reserveArmy[i] != null)
            {
                GameObject unit = Instantiate(reserveArmy[i].unitGO);
                unit.transform.SetParent(ReserveArmyContainer.transform);
                unit.GetComponentInChildren<TMP_Text>().text = reserveArmy[i].quantity.ToString();
                unit.GetComponent<UnitController>().Initilize(reserveArmy[i]);
                unit.SetActive(false);
                realUnitsInReserve.Add(unit);
                // we need this for acces to UnitController Script of disable objects
                realUnitsComponentsInReserve.Add(unit.GetComponent<UnitController>());
            }
        }
    }

    private void SwitchUnit(bool mode, Unit unit)
    {
        if(unit == null) return;

        Unit[] targetUnitArray = (mode == true) ? playersArmy : reserveArmy;
        Unit[] sourceUnitArray = (mode == true) ? reserveArmy : playersArmy;

        bool checkEmptySlotInArmy = false;
        int index = 0;

        for(int i = 0; i < targetUnitArray.Length; i++)
        {
            if(targetUnitArray[i] == null)
            {
                checkEmptySlotInArmy = true;
                index = i;
                break;
            }
        }

        if(checkEmptySlotInArmy == true)
        {
            for(int i = 0; i < sourceUnitArray.Length; i++)
            {
                if(sourceUnitArray[i] != null && sourceUnitArray[i].UnitType == unit.UnitType)
                {
                    sourceUnitArray[i] = null;
                    break;
                }
            }

            targetUnitArray[index] = unit;
        }
        else
        {
            return;
        }

        playersArmyWindow.CreateReserveScheme(reserveArmy);
        playersArmyWindow.CreateArmyScheme(playersArmy);
        CreateArmyOnBattlefield(playersArmy);
    }

    public void UnitsReplacementUI(int index)
    {
        if(playersArmy.Length == 0) return;

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
        //int currentArmyCount = 0;
        //check units
        //for (int i = 0; i < realUnitsOnBattlefield.Length; i++)
        //{
        //    if (realUnitsOnBattlefield[i] != null)
        //    {
        //        currentArmyCount++;
        //    }
        //}

        ////create new units if army on battlefield is empty
        //if (currentArmyCount == 0)
        //{
        //    for (int i = 0; i < army.Length; i++)
        //    {
        //        if (army[i] != null)
        //        {
        //            GameObject unit = Instantiate(army[i].unitGO, (Vector3)playersArmyPositions[i] + battleArmyController.gameObject.transform.position, Quaternion.identity);
        //            unit.transform.SetParent(battleArmyController.gameObject.transform);
        //            unit.GetComponentInChildren<TMP_Text>().text = army[i].quantity.ToString();
        //            unit.GetComponent<UnitController>().Initilize(army[i]);
        //            realUnitsOnBattlefield[i] = unit;
        //        }
        //    }
        //}
        ////replacement units if army on battlefield is ready
        //else
        //{
        //    if (firstIndexForReplaceUnit != -1 && secondIndexForReplaceUnit != -1)
        //    {
        //        GameObject firstUnit = realUnitsOnBattlefield[firstIndexForReplaceUnit];
        //        realUnitsOnBattlefield[firstIndexForReplaceUnit] = realUnitsOnBattlefield[secondIndexForReplaceUnit];
        //        realUnitsOnBattlefield[secondIndexForReplaceUnit] = firstUnit;

        //        if (realUnitsOnBattlefield[firstIndexForReplaceUnit] != null)
        //            realUnitsOnBattlefield[firstIndexForReplaceUnit].transform.position = (Vector3)playersArmyPositions[firstIndexForReplaceUnit] + battleArmyController.gameObject.transform.position;

        //        if (realUnitsOnBattlefield[secondIndexForReplaceUnit] != null)
        //            realUnitsOnBattlefield[secondIndexForReplaceUnit].transform.position = (Vector3)playersArmyPositions[secondIndexForReplaceUnit] + battleArmyController.gameObject.transform.position;
        //    }            
        //}

        //move all realArmy units to realArmyReserve
        for(int i = 0; i < realUnitsOnBattlefield.Length; i++)
        {
            if(realUnitsOnBattlefield[i] != null)
            {
                realUnitsOnBattlefield[i].transform.SetParent(ReserveArmyContainer.transform);                

                realUnitsInReserve.Add(realUnitsOnBattlefield[i]);
                realUnitsComponentsInReserve.Add(realUnitsOnBattlefield[i].GetComponent<UnitController>());
                realUnitsOnBattlefield[i].SetActive(false);

                realUnitsOnBattlefield[i] = null;
            }
        }

        //fill realArmy from realArmyReserve
        for(int i = 0; i < playersArmy.Length; i++)
        {
            if(playersArmy[i] != null)
            {
                for(int j = 0; j < realUnitsInReserve.Count; j++)
                {
                    //read the name of list attentive!
                    if(realUnitsComponentsInReserve[j].GetComponent<UnitController>().unitType == playersArmy[i].UnitType)
                    {
                        realUnitsOnBattlefield[i] = realUnitsInReserve[j];
                        realUnitsOnBattlefield[i].SetActive(true);
                        realUnitsOnBattlefield[i].transform.position = (Vector3)playersArmyPositions[i] + battleArmyController.gameObject.transform.position;
                        realUnitsOnBattlefield[i].transform.SetParent(battleArmyController.gameObject.transform);

                        realUnitsInReserve.Remove(realUnitsInReserve[j]);
                        realUnitsComponentsInReserve.Remove(realUnitsComponentsInReserve[j]);
                        break;
                    }
                }
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
                //EventManager.OnReserveArmyIsReadyEvent(playersArmy);
                break;
            }
        }
    }

    private void OnEnable()
    {
        EventManager.AllUnitsIsReady += InitializeArmy;
        EventManager.PlayersArmyIsReady += CreateArmyOnBattlefield;
        EventManager.WeLostOneUnit += UpgradeArmy;
        EventManager.SwitchUnit += SwitchUnit;
    }

    private void OnDisable()
    {
        EventManager.AllUnitsIsReady -= InitializeArmy;
        EventManager.PlayersArmyIsReady -= CreateArmyOnBattlefield;
        EventManager.WeLostOneUnit -= UpgradeArmy;
        EventManager.SwitchUnit -= SwitchUnit;
    }
}
