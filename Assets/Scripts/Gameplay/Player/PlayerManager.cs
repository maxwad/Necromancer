using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playersArmyWindow;

    private void Update()
    {
        if (GlobalStorage.instance.isGlobalMode == true && Input.GetKeyDown(KeyCode.I))
        {
            if (playersArmyWindow.activeInHierarchy == true)
                playersArmyWindow.GetComponent<PlayersArmyWindow>().CloseWindow();
            else
                playersArmyWindow.SetActive(true);
        }
    }

}
