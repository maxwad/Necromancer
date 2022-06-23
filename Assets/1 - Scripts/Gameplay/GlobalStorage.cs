using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    [HideInInspector] public static GlobalStorage instance;

    [Header("Managers")]
    public MenuManager menuManager;
    public UnitManager unitManager;
    public BoostManager boostManager;
    public BattleManager battleManager;
    public PlayerManager playerManager;
    public ObjectsPoolManager objectsPoolManager;

    [Header("Player")]
    public GameObject player;
    public GameObject globalPlayer;
    public GameObject battlePlayer;

    [Header("Maps")]
    public GameObject globalMap;
    public GameObject battleMap;

    [HideInInspector] public bool isGlobalMode = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangePlayMode(bool mode)
    {
        isGlobalMode = mode;
        EventManager.OnChangePlayModeEvent(mode);
    }

}
