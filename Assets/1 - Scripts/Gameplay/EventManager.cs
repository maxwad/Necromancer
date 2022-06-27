using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static NameManager;

public static class EventManager
{
    #region UNITS

    //calls when we had finished to create all units
    //
    //SUBSCRIBERS:
    // - PlayersArmy
    //
    //ACTIVATION:
    // - UnitManager
    //
    public delegate void AllUnitsIsReadyEvent();
    public static event AllUnitsIsReadyEvent AllUnitsIsReady;
    public static void OnAllUnitsIsReadyEvent() => AllUnitsIsReady?.Invoke();


    //calls when we had finished to create an army of heroes
    //
    //SUBSCRIBERS:
    // - PlayersArmy
    // - PlayersArmyWindow
    //
    //ACTIVATION:
    // - PlayersArmy
    //
    public delegate void PlayersArmyIsReadyEvent(Unit[] army);
    public static event PlayersArmyIsReadyEvent PlayersArmyIsReady;
    public static void OnPlayersArmyIsReadyEvent(Unit[] army) => PlayersArmyIsReady?.Invoke(army);


    //calls when we lose 1 unit from squad
    //
    //SUBSCRIBERS:
    // - PlayersArmy
    // - BattleArmyController
    //
    //ACTIVATION:
    // - UnitController
    //
    public delegate void WeLostOneUnitEvent(UnitsTypes unitType, int quantity);
    public static event WeLostOneUnitEvent WeLostOneUnit;
    public static void OnWeLostOneUnitEvent(UnitsTypes unitType, int quantity) => WeLostOneUnit?.Invoke(unitType, quantity);

    #endregion


    #region PLAYER
    //calls when we should switch between globalPlayer and battlePlayer
    //
    //SUBSCRIBERS:
    // - BattleMap
    // - PlayerManager
    //
    //ACTIVATION:
    // - CameraManager
    //
    public delegate void ChangePlayerEvent(bool mode);
    public static event ChangePlayerEvent ChangePlayer;
    public static void OnChangePlayerEvent(bool mode) => ChangePlayer?.Invoke(mode);

    #endregion


    #region Battle
    //calls when we finish or crash the battle
    //
    //SUBSCRIBERS:
    // - BonusController
    // - ObjectsPoolManager
    //
    //ACTIVATION:
    // - BattleMap
    //
    public delegate void EndOfBattleEvent(); 
    public static event EndOfBattleEvent EndOfBattle;
    public static void OnEndOfBattleEvent() => EndOfBattle?.Invoke();



    //calls when we finish or crash the battle
    //
    //SUBSCRIBERS:
    // - BattleMap
    //
    //ACTIVATION:
    // - HealthObjectStats
    //
    public delegate void ObstacleDestroyedEvent(GameObject objectOnMap);
    public static event ObstacleDestroyedEvent ObstacleDestroyed;
    public static void OnObstacleDestroyedEvent(GameObject objectOnMap) => ObstacleDestroyed?.Invoke(objectOnMap);


    #endregion

    #region Bonuses

    //calls when we pick up some bonus
    //
    //SUBSCRIBERS:
    // - BonusController
    //
    //ACTIVATION:
    // - BattleMap
    //
    public delegate void BonusPickedUpEvent(BonusType type, float value);
    public static event BonusPickedUpEvent BonusPickedUp;
    public static void OnBonusPickedUpEvent(BonusType type, float value) => BonusPickedUp?.Invoke(type, value);

    #endregion

    #region OTHER
    //calls when we switch between global mode and battle mode
    //
    //SUBSCRIBERS:
    // - CameraManager
    //
    //ACTIVATION:
    // - GlobalStorage
    //
    public delegate void ChangePlayModeEvent(bool mode);
    public static event ChangePlayModeEvent ChangePlayMode;
    public static void OnChangePlayModeEvent(bool mode) => ChangePlayMode?.Invoke(mode);

    #endregion
}
