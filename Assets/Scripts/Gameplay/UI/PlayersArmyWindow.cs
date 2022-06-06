using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayersArmyWindow : MonoBehaviour
{
    public ArmySlot[] armySlots;

    public Button fightButton;
    public Button closeWindow;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            fightButton.onClick.AddListener(ToTheBattle);
        else
            fightButton.onClick.AddListener(ToTheGlobalMap);

        closeWindow.onClick.AddListener(CloseWindow);
    }

    private void OnDisable()
    {
        fightButton.onClick.RemoveListener(ToTheBattle);
        fightButton.onClick.RemoveListener(ToTheGlobalMap);
        closeWindow.onClick.RemoveListener(CloseWindow);
    }

    public void CreateArmyScheme(Unit[] army)
    {
        for (int i = 0; i < army.Length; i++)
        {
            armySlots[i].FillTheSlot(army[i]);
        }
    }

    private void ToTheBattle()
    {
        Debug.Log("Start MORTAL COMBAT!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        CloseWindow();
    }

    private void ToTheGlobalMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.Log("Back to the map!");
        CloseWindow();
    }

    public void CloseWindow()
    {      
        for (int i = 0; i < armySlots.Length; i++)
        {
            armySlots[i].ResetSelecting();
        }

        gameObject.SetActive(false);
    }
}
