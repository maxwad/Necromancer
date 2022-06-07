using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersArmyWindow : MonoBehaviour
{
    public ArmySlot[] armySlots;

    public Button fightButton;
    public Button closeWindow;

    private void Awake()
    {
        fightButton.onClick.AddListener(ToTheBattle);       
        closeWindow.onClick.AddListener(CloseWindow);
    }

    private void OnDestroy()
    {
        fightButton.onClick.RemoveListener(ToTheBattle);
        //fightButton.onClick.RemoveListener(ToTheGlobalMap);
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
        fightButton.onClick.RemoveListener(ToTheBattle);
        //fightButton.onClick.AddListener(ToTheGlobalMap);
        
        GlobalStorage.instance.battleManager.GetComponent<BattleManager>().InitializeBattle(null);
        CloseWindow();
    }

    //private void ToTheGlobalMap()
    //{
    //    fightButton.onClick.RemoveListener(ToTheGlobalMap);
    //    fightButton.onClick.AddListener(ToTheBattle);

    //    CloseWindow();
    //}

    public void CloseWindow()
    {      
        for (int i = 0; i < armySlots.Length; i++)
            armySlots[i].ResetSelecting();

        gameObject.SetActive(false);
    }
}
