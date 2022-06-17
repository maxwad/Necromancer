using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //for testing!
    public List<GameObject> falsEnemyArmy;
    public List<int> falseEnemiesQuantity;

    private List<GameObject> enemyArmy = new List<GameObject>();
    private List<int> enemiesQuantity = new List<int>();

    private int sizeMapX = 150;
    private int sizeMapY = 100;
    private int positionZ = 40; // we should send double size of Z because dividing by 2

    [SerializeField] private EnemySpawner enemySpawner;

    private void Start()
    {
        //falseEnemyArmy = ;
    }

    public void InitializeBattle(GameObject realEnemyArmy)
    {
        GlobalStorage.instance.ChangePlayMode(false);

        //for testing!
        if (enemyArmy.Count == 0)
        {
            enemyArmy = falsEnemyArmy;
            enemiesQuantity = falseEnemiesQuantity;
        }

        enemySpawner.Initialize(enemyArmy, enemiesQuantity);
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

    public void SetEnemyArmy(List<GameObject> army, List<int> quantity)
    {
        enemyArmy = army;
        enemiesQuantity = quantity;
    }
    
    public void ClearEnemyArmy()
    {
        enemyArmy.Clear();
        enemiesQuantity.Clear();
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
