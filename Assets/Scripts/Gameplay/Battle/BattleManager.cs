using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private GameObject falseEnemyArmy;
    private GameObject enemyArmy;

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

    public void ChangePlayer(bool mode)
    {
        Debug.Log("SwitchPlayer");
    }

    private void OnEnable()
    {
        EventManager.ChangePlayMode += ChangePlayer;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayMode -= ChangePlayer;
    }
}
