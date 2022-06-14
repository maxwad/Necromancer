using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private GameObject falseEnemyArmy;
    private GameObject enemyArmy;

    private int sizeMapX = 20;
    private int sizeMapY = 20;
    private int positionZ = 40; // we should send double size of Z because dividing by 2

    private void Start()
    {
        //falseEnemyArmy = ;
    }

    public void InitializeBattle(GameObject realEnemyArmy)
    {
        //switch the camera script;

        enemyArmy = realEnemyArmy == null ? falseEnemyArmy : realEnemyArmy;

        GlobalStorage.instance.ChangePlayMode(false);
    }

    public Vector3Int GetBattleMapSize()
    {
        return new Vector3Int(sizeMapX, sizeMapY, positionZ);
    }

    public void SetBattleMapSize(int width = 10, int heigth = 10)
    {
        sizeMapX = width;
        sizeMapY = heigth;
    }

    public void FinishTheBattle()
    {
        GlobalStorage.instance.ChangePlayMode(true);
    }

    //public void ChangePlayer(bool mode)
    //{
    //    Debug.Log("SwitchPlayer");
    //}

    //private void OnEnable()
    //{
    //    EventManager.ChangePlayMode += ChangePlayer;
    //}

    //private void OnDisable()
    //{
    //    EventManager.ChangePlayMode -= ChangePlayer;
    //}
}
