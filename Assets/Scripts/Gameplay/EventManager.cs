using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class EventManager
{
    #region UNITS
    //----------- UNITS----------------

    //calls when we had finished to create an army of heroes
    public delegate void PlayersArmyIsReadyEvent(Unit[] army);
    public static event PlayersArmyIsReadyEvent PlayersArmyIsReady;
    public static void OnPlayersArmyIsReadyEvent(Unit[] army) => PlayersArmyIsReady?.Invoke(army);


    #endregion

    #region OTHER
    //calls when we switch between global mode and battle mode
    public delegate void ChangePlayModeEvent(bool mode);
    public static event ChangePlayModeEvent ChangePlayMode;
    public static void OnChangePlayModeEvent(bool mode) => ChangePlayMode?.Invoke(mode);


    #endregion
}
