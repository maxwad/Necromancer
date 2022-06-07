using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 globalCameraPosition = new Vector3(0, 0, 0);
    private Vector3 battleCameraPosition = new Vector3(0, 0, 10);

    private GameObject falseEnemyArmy;
    private GameObject enemyArmy;

    private void Start()
    {
        mainCamera = Camera.main;

        //falseEnemyArmy = ;
    }

    public void InitializeBattle(GameObject realEnemyArmy)
    {
        globalCameraPosition = mainCamera.transform.position;
        mainCamera.transform.position = battleCameraPosition;
        //switch the camera script;

        enemyArmy = realEnemyArmy == null ? falseEnemyArmy : realEnemyArmy;

        GlobalStorage.instance.isGlobalMode = false;
    }
}
