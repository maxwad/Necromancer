using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class PlayersArmy : MonoBehaviour
{
    public UnitManager unitManager;

    public UnitsTypes[] playersArmyEnums;

    public List<Unit> playersArmy = new List<Unit>();

    private void Start()
    {
        playersArmy = unitManager.GetUnitsForPlayersArmy(playersArmyEnums);

        Debug.Log("»того в армии:");
        foreach (var item in playersArmy)
        {
            item.quantity = 4;
            item.commonHealth = item.quantity * item.health;
            Debug.Log(item.unitName + " по цене " + item.coinsPrice + " с общим здоровьем в " + item.commonHealth + " единиц.");
        }
    }
}
