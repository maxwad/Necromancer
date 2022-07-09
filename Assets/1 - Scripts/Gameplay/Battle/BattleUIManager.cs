using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NameManager;

public class BattleUIManager : MonoBehaviour
{
    public Canvas uiCanvas;

    [Header("Left Column Exp")]
    [SerializeField] private RectTransform currentScaleValueWrapper;
    [SerializeField] private RectTransform currentScaleValue;
    private float heigthCurrentScaleWrapper;
    private float currentTempExp = 0;

    [Header("Rigth Column Exp")]
    [SerializeField] private GameObject tempLevelGO;
    [SerializeField] private RectTransform currentTempLevelWrapper;
    private float heigthOneLevel;
    private float currentMaxLevel;
    private List<GameObject> levelList = new List<GameObject>();

    [Header("Infirmary")]
    [SerializeField] private RectTransform infirmaryWrapper;
    [SerializeField] private RectTransform infirmaryValue;
    [SerializeField] private TMP_Text infirmaryInfo;
    private float currentMaxInfirmaryCount;
    private float currentInfirmaryCount;

    [Header("Mana")]
    [SerializeField] private RectTransform manaWrapper;
    [SerializeField] private RectTransform manaValue;
    [SerializeField] private TMP_Text manaInfo;
    private float currentMaxManaCount;
    private float currentManaCount;

    [Header("Gold")]
    [SerializeField] private TMP_Text goldInfo;
    private float currentGoldCount;

    [Header("Spells")]
    [SerializeField] private Button buttonSpell;
    private List<SpellStat> currentSpells = new List<SpellStat>();
    [SerializeField] private GameObject spellButtonContainer;
    private List<Button> currentSpellsButtons = new List<Button>();
    private int countOfActiveSpells = 10;
    private int currentSpellIndex = -1;

    [Header("Enemy")]
    [SerializeField] private RectTransform enemiesWrapper;
    [SerializeField] private RectTransform enemiesValue;
    [SerializeField] private TMP_Text enemiesInfo;
    private float maxEnemiesCount = 0;
    private float currentEnemiesCount = 0;


    private void Update()
    {
        if(GlobalStorage.instance.isGlobalMode == false) Spelling();
    }

    private void Spelling()
    {
        switch(Input.inputString)
        {
            case "1":
                currentSpellIndex = 0;
                break;
            case "2":
                currentSpellIndex = 1;
                break;
            case "3":
                currentSpellIndex = 2;
                break;
            case "4":
                currentSpellIndex = 3;
                break;
            case "5":
                currentSpellIndex = 4;
                break;
            case "6":
                currentSpellIndex = 5;
                break;
            case "7":
                currentSpellIndex = 6;
                break;
            case "8":
                currentSpellIndex = 7;
                break;
            case "9":
                currentSpellIndex = 8;
                break;
            case "0":
                currentSpellIndex = 9;
                break;

            default:
                currentSpellIndex = -1;
                break;
        }

        if(currentSpellIndex != -1) currentSpellsButtons[currentSpellIndex].GetComponent<SpellButtonController>().ActivateSpell();
    }

    public void Inizialize(bool mode)
    {
        uiCanvas.gameObject.SetActive(!mode);

        if(mode == false) ResetCanvas();
    }

    private void ResetCanvas()
    {
        FillRigthTempLevelScale();

        FillInfirmary();

        FillMana();

        FillGold();

        FillSpells(-1);

        FillEnemiesBar(null);
    }

    #region TempExp
    private void FillRigthTempLevelScale()
    {
        levelList.Clear();

        foreach(Transform child in currentTempLevelWrapper.transform)
            Destroy(child.gameObject);

        heigthCurrentScaleWrapper = currentScaleValueWrapper.rect.height;
        currentScaleValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        currentTempExp = 0;

        currentMaxLevel = GlobalStorage.instance.player.GetComponent<PlayerStats>().GetStartParameter(PlayersStats.Level);

        heigthOneLevel = currentTempLevelWrapper.rect.height / currentMaxLevel;

        for(int i = 0; i < currentMaxLevel; i++)
        {
            GameObject levelPart = Instantiate(tempLevelGO);

            RectTransform rectLevelSize = levelPart.GetComponent<RectTransform>();
            levelPart.transform.SetParent(currentTempLevelWrapper.transform);
            rectLevelSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heigthOneLevel);
            rectLevelSize.anchoredPosition = new Vector2(0, heigthOneLevel / 2 + heigthOneLevel * i);

            RectTransform rectNumb = levelPart.GetComponentsInChildren<Image>()[1].GetComponent<RectTransform>();
            rectNumb.anchoredPosition = new Vector2(0, -rectNumb.rect.height / 2);

            rectNumb.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();

            levelList.Add(levelPart);
        }
    }

    public void UpgradeScale(float scale, float value)
    {
        float heightOneExp = heigthCurrentScaleWrapper / scale;
        currentTempExp += value;
        currentScaleValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentTempExp * heightOneExp);
    }

    public void TempLevelUp(float oldLevel)
    {
        levelList[(int)oldLevel].GetComponent<Image>().enabled = true;

        if(oldLevel + 1 < levelList.Count)
        {
            currentTempExp = 0;
            currentScaleValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        }
    }

    #endregion

    #region Infirmary

    private void FillInfirmary(float max = 0, float current = 0)
    {
        if(max == 0)
        {
            currentMaxInfirmaryCount = GlobalStorage.instance.player.GetComponent<PlayerStats>().GetStartParameter(PlayersStats.Infirmary);
            currentInfirmaryCount = GlobalStorage.instance.infirmaryManager.injuredList.Count;
        }
        else
        {
            currentMaxInfirmaryCount = max;
            currentInfirmaryCount = current;
        }        

        float widthInfirmary = infirmaryWrapper.rect.width;
        float widthOneInjured = widthInfirmary / currentMaxInfirmaryCount;

        infirmaryValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthOneInjured * currentInfirmaryCount);
        infirmaryInfo.text = currentInfirmaryCount.ToString() + "/" + currentMaxInfirmaryCount.ToString();

    }

    private void UpdateInfirmaryUI(float quantity, float capacity)
    {
        FillInfirmary(capacity, quantity);
    }

    #endregion

    #region Mana

    private void FillMana(float max = 0, float current = 0)
    {
        if(max == 0)
        {
            currentMaxManaCount = GlobalStorage.instance.player.GetComponent<PlayerStats>().GetStartParameter(PlayersStats.Mana);
            currentManaCount = GlobalStorage.instance.hero.currentMana;
        }
        else
        {
            currentMaxManaCount = max;
            currentManaCount = current;
        }

        float heightMana = currentManaCount / currentMaxManaCount;

        manaValue.GetComponent<Image>().fillAmount = heightMana;

        manaInfo.text = currentManaCount.ToString();
    }

    private void UpdateManaUI(float maxValue, float currentValue)
    {
        FillMana(maxValue, currentValue);
    }

    #endregion

    #region Gold
    private void FillGold(float current = 0)
    {
        if(current == 0)
        {
            currentGoldCount = GlobalStorage.instance.resourcesManager.Gold;
        }
        else
        {
            currentGoldCount = current;
        }               

        goldInfo.text = currentGoldCount.ToString();
    }

    private void UpdateGoldUI(float currentValue)
    {
        FillGold(currentValue);
    }

    #endregion

    #region Spells 

    private void FillSpells(int numberOfSpell)
    {
        currentSpells = GlobalStorage.instance.spellManager.GetCurrentSpells();

        if(numberOfSpell == -1)
        {
            foreach(Transform child in spellButtonContainer.transform)
                Destroy(child.gameObject);

            currentSpellsButtons.Clear();

            for(int i = 0; i < currentSpells.Count; i++)
            {
                int slotNumber = -1;
                if(i < countOfActiveSpells)
                {
                    slotNumber = i + 1;
                    if(i + 1 == 10) slotNumber = 0;
                }   

                Button button = Instantiate(buttonSpell);
                button.GetComponent<SpellButtonController>().SetSpellOnButton(currentSpells[i]);
                button.GetComponent<SpellButtonController>().InitializeButton(slotNumber);
                currentSpellsButtons.Add(button);
                button.transform.SetParent(spellButtonContainer.transform);
            }
        }
        else
        {
            for(int i = 0; i < currentSpells.Count; i++)
            {
                if(i == numberOfSpell)
                {
                    Button button = Instantiate(buttonSpell);
                    button.GetComponent<SpellButtonController>().SetSpellOnButton(currentSpells[i]);
                    button.GetComponent<SpellButtonController>().InitializeButton();
                    currentSpellsButtons.Add(button);
                    button.transform.SetParent(spellButtonContainer.transform);

                    break;
                }
            }
        }        
    }

    #endregion

    #region EnemiesBar
    private void GetStartCountEnemies(int count)
    {
        maxEnemiesCount = count;
        currentEnemiesCount = count;
    }

    public void FillEnemiesBar(GameObject enemy)
    {
        if(enemy != null) currentEnemiesCount--;

        float widthEnemiesBar = enemiesWrapper.rect.width;
        float widthOneEnemiesBarPiece = widthEnemiesBar / maxEnemiesCount;

        enemiesValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthOneEnemiesBarPiece * (maxEnemiesCount - currentEnemiesCount));
        enemiesInfo.text = currentEnemiesCount.ToString();
    }

    #endregion

    private void OnEnable()
    {
        EventManager.ChangePlayer      += Inizialize;
        EventManager.UpdateInfirmaryUI += UpdateInfirmaryUI;
        EventManager.UpgradeMana       += UpdateManaUI;
        EventManager.UpgradeGold       += UpdateGoldUI;
        EventManager.EnemiesCount      += GetStartCountEnemies;
        EventManager.EnemyDestroyed    += FillEnemiesBar;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayer      -= Inizialize;
        EventManager.UpdateInfirmaryUI -= UpdateInfirmaryUI;
        EventManager.UpgradeMana       -= UpdateManaUI;
        EventManager.UpgradeGold       -= UpdateGoldUI;
        EventManager.EnemiesCount      -= GetStartCountEnemies;
        EventManager.EnemyDestroyed    -= FillEnemiesBar;
    }
}