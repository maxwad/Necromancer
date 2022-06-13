using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayersArmyWindow playersArmyWindow;

    [SerializeField] private GameObject globalPlayer;
    [SerializeField] private GameObject battlePlayer;
    private Vector3 globalPlayerPosition;

    [SerializeField] private GameObject globalMap;
    [SerializeField] private GameObject battleMap;

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

            Vector3Int bounds = new Vector3Int(20, 20, 0); // 10 tiles from each sides
            Vector3Int startPosition = (GlobalStorage.instance.battleManager.GetBattleMapSize() + bounds ) / 2;

            globalPlayerPosition = globalPlayer.transform.position;
            battlePlayer.transform.position = (Vector3)startPosition;
        }
        else
        {
            globalPlayer.SetActive(true);
            globalMap.SetActive(true);

            battlePlayer.SetActive(false);
            //battleMap we turn off in BattleMap script, because we should clear it

            globalPlayer.transform.position = globalPlayerPosition;
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
