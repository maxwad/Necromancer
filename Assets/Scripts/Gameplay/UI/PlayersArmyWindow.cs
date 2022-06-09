using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersArmyWindow : MonoBehaviour
{
    private Unit[] playersArmy;
    [SerializeField] private GameObject playerArmyUI;  
    [SerializeField] private ArmySlot[] armySlots;

    [SerializeField] private Button fightButton;
    [SerializeField] private Button closeWindow;

    [HideInInspector] public bool isWindowOpened = false;

    private void Awake()
    {
        fightButton.onClick.AddListener(ToTheBattle);       
        closeWindow.onClick.AddListener(CloseWindow);        
    }

    private void Start()
    {
        EventManager.PlayersArmyIsReady += SetArmy;
    }

    private void OnDestroy()
    {
        fightButton.onClick.RemoveListener(ToTheBattle);
        closeWindow.onClick.RemoveListener(CloseWindow);
        EventManager.PlayersArmyIsReady -= SetArmy;
    }

    private void SetArmy(Unit[] army)
    {
        playersArmy = army;
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
        
        GlobalStorage.instance.battleManager.InitializeBattle(null);
        CloseWindow();
    }


    public void OpenWindow()
    {
        if (isWindowOpened == false)
        {
            playerArmyUI.SetActive(true);
            isWindowOpened = true;
            CreateArmyScheme(playersArmy);
            if (GlobalStorage.instance.isGlobalMode == false)
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
        if (GlobalStorage.instance.isGlobalMode == false)
            MenuManager.instance.MiniPauseOff();
    }
}
