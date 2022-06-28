using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayersArmyWindow playersArmyWindow;

    private GameObject globalPlayer;
    private GameObject battlePlayer;

    private GameObject globalMap;

    private void Start()
    {
        globalPlayer = GlobalStorage.instance.globalPlayer;
        battlePlayer = GlobalStorage.instance.battlePlayer;

        globalMap = GlobalStorage.instance.globalMap;
    }

    private void Update()
    {
        if (MenuManager.isGamePaused == false && Input.GetKeyDown(KeyCode.I))
        {
            playersArmyWindow.OpenWindow();
        }
    }

    private void MovePlayerToTheGlobal(bool mode)
    {
        if (mode == false)
        {
            globalPlayer.SetActive(false);
            globalMap.SetActive(false);

            battlePlayer.SetActive(true);
        }
        else
        {
            globalPlayer.SetActive(true);
            globalMap.SetActive(true);

            battlePlayer.SetActive(false);
            //battleMap we turn off in BattleMap script, because we should clear it first!
        }
    }


    private void OnEnable()
    {
        EventManager.ChangePlayer += MovePlayerToTheGlobal;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayer -= MovePlayerToTheGlobal;
    }

}
