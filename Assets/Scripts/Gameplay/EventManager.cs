using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static NameManager;

public static class EventManager
{
    #region UNITS
    //----------- UNITS----------------

    //calls when we had finished to create all units
    public delegate void AllUnitsIsReadyEvent();
    public static event AllUnitsIsReadyEvent AllUnitsIsReady;
    public static void OnAllUnitsIsReadyEvent() => AllUnitsIsReady?.Invoke();

    //calls when we had finished to create an army of heroes
    public delegate void PlayersArmyIsReadyEvent(Unit[] army);
    public static event PlayersArmyIsReadyEvent PlayersArmyIsReady;
    public static void OnPlayersArmyIsReadyEvent(Unit[] army) => PlayersArmyIsReady?.Invoke(army);

    //calls when we lose 1 unit from squad
    public delegate void WeLostOneUnitEvent(UnitsTypes unitType, int quantity);
    public static event WeLostOneUnitEvent WeLostOneUnit;
    public static void OnWeLostOneUnitEvent(UnitsTypes unitType, int quantity) => WeLostOneUnit?.Invoke(unitType, quantity);

    #endregion


    #region PLAYER
    //calls when we should switch between globalPlayer and battlePlayer
    public delegate void ChangePlayerEvent(bool mode);
    public static event ChangePlayerEvent ChangePlayer;
    public static void OnChangePlayerEvent(bool mode) => ChangePlayer?.Invoke(mode);

    #endregion

    #region OTHER
    //calls when we switch between global mode and battle mode
    public delegate void ChangePlayModeEvent(bool mode);
    public static event ChangePlayModeEvent ChangePlayMode;
    public static void OnChangePlayModeEvent(bool mode) => ChangePlayMode?.Invoke(mode);

    #endregion
}
