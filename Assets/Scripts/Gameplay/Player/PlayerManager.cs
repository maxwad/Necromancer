using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayersArmyWindow playersArmyWindow;

    [SerializeField] private GameObject globalPlayer;
    [SerializeField] private GameObject battlePlayer;
    private Vector3 globalPlayerPosition;
    [SerializeField] private GameObject battlePlayerPosition;

    private void Update()
    {
        if (MenuManager.isGamePaused == false && Input.GetKeyDown(KeyCode.I))
        {
            playersArmyWindow.OpenWindow();
        }
    }

    private void MovePlayerToTheBattle(bool mode)
    {
        if (mode == false)
        {
            globalPlayer.SetActive(false);
            battlePlayer.SetActive(true);

            globalPlayerPosition = globalPlayer.transform.position;
            battlePlayer.transform.position = battlePlayerPosition.transform.position;
        }
        else
        {
            globalPlayer.SetActive(true);
            battlePlayer.SetActive(false);

            globalPlayer.transform.position = globalPlayerPosition;
        }
    }

    private void OnEnable()
    {
        EventManager.ChangePlayer += MovePlayerToTheBattle;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayer -= MovePlayerToTheBattle;
    }

}
