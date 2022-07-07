using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayersArmyWindow : MonoBehaviour
{
    [SerializeField] private GameObject playerArmyUI;

    [Header("Army")]
    [SerializeField] private ArmySlot[] armySlots;

    [Header("Reserve")]
    [SerializeField] private ArmySlot[] reserveSlots;

    [Header("Buttons")]
    [SerializeField] private Button fightButton;
    [SerializeField] private Button closeWindow;
    [SerializeField] private TMP_Text fightButtonText;
    private string toTheBattleText = "Fight!";
    private string toTheGlobalText = "Escape...";

    [HideInInspector] public bool isWindowOpened = false;


    public void CreateReserveScheme(Unit[] army)
    {
        for(int i = 0; i < army.Length; i++)
        {
            reserveSlots[i].FillTheArmySlot(army[i]);
        }
    }

    public void CreateArmyScheme(Unit[] army)
    {
        for (int i = 0; i < army.Length; i++)
        {
            armySlots[i].FillTheArmySlot(army[i]);
        }
    }

    private void ToTheBattle()
    {
        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(ToTheGlobal);
        fightButtonText.text = toTheGlobalText;

        GlobalStorage.instance.battleManager.InitializeBattle();
        //TODO: may be here we need to give info about army future battle to the battle manager
        CloseWindow();
    }

    private void ToTheGlobal()
    {
        fightButton.onClick.RemoveAllListeners();
        fightButton.onClick.AddListener(ToTheBattle);
        fightButtonText.text = toTheBattleText;

        GlobalStorage.instance.battleManager.FinishTheBattle();
        CloseWindow();
    }


    public void OpenWindow()
    {
        if (isWindowOpened == false)
        {
            playerArmyUI.SetActive(true);
            isWindowOpened = true;            
            CreateReserveScheme(GlobalStorage.instance.player.GetComponent<PlayersArmy>().reserveArmy);
            CreateArmyScheme(GlobalStorage.instance.player.GetComponent<PlayersArmy>().playersArmy);
            MenuManager.instance.MiniPauseOn();

        }            
        else
            CloseWindow();
    }

    public void CloseWindow()
    {      
        for (int i = 0; i < armySlots.Length; i++)
            armySlots[i].ResetSelecting();

        playerArmyUI.SetActive(false);
        isWindowOpened = false;
        MenuManager.instance.MiniPauseOff();

        //TODO: here we should clear info about canceled battle
    }

    private void Awake()
    {
        fightButton.onClick.AddListener(ToTheBattle);
        fightButtonText.text = toTheBattleText;
        closeWindow.onClick.AddListener(CloseWindow);
    }

    private void OnDestroy()
    {
        fightButton.onClick.RemoveListener(ToTheBattle);
        closeWindow.onClick.RemoveListener(CloseWindow);
    }
}
