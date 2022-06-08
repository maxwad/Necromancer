using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playersArmyWindow;

    private GameObject globalPlayer;
    private GameObject battlePlayer;

    private void Update()
    {
        if (GlobalStorage.instance.isGlobalMode == true && Input.GetKeyDown(KeyCode.I))
        {
            playersArmyWindow.GetComponent<PlayersArmyWindow>().OpenWindow();
        }
    }

}
