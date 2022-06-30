using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NameManager;

public class BattleUIManager : MonoBehaviour
{
    public Canvas uiCanvas;

    [Header("Left Column Exp")]
    public Image currentScaleValueWrapper;
    public RectTransform currentScaleValue;
    private float heigthCurrentScaleWrapper;
    private float currentTempExp = 0;

    [Header("Rigth Column Exp")]
    public GameObject tempLevelGO;
    public Image currentTempLevelWrapper;
    private float heigthOneLevel;
    private float currentMaxLevel;
    private List<GameObject> levelList = new List<GameObject>();

    public void Inizialize(bool mode)
    {
        uiCanvas.gameObject.SetActive(!mode);

        if(mode == false)
        {
            ResetCanvas();
        }
    }

    private void ResetCanvas()
    {
        levelList.Clear();

        foreach(Transform child in currentTempLevelWrapper.transform)
            Destroy(child.gameObject);

        heigthCurrentScaleWrapper = currentScaleValueWrapper.GetComponent<RectTransform>().rect.height;
        currentScaleValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        currentTempExp = 0;

        FillRigthTempLevelScale();
    }

    private void FillRigthTempLevelScale()
    {
        currentMaxLevel = GlobalStorage.instance.player.GetComponent<PlayerStats>().GetStartParameter(PlayersStats.Level);

        heigthOneLevel = currentTempLevelWrapper.GetComponent<RectTransform>().rect.height / currentMaxLevel;

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

    private void OnEnable()
    {
        EventManager.ChangePlayer += Inizialize;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayer -= Inizialize;
    }
}
