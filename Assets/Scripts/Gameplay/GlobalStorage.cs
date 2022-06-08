using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    public static GlobalStorage instance;

    public UnitManager unitManager;
    public UnitBoostManager boostManager;
    public BattleManager battleManager;
    public PlayerManager playerManager;

    public GameObject player;

    public bool isGlobalMode = true;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //InitializeAll();
    }

    public void ChangePlayMode(bool mode)
    {
        isGlobalMode = mode;
        EventManager.OnChangePlayModeEvent(mode);

    }
    private void InitializeAll()
    {

    }

}
