using System;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class PlayerBoostManager : MonoBehaviour
{
    private struct Boost
    {
        public SenderTypes senderType;
        public float value;

        public Boost(SenderTypes type, float boostValue)
        {
            senderType = type;
            value = boostValue;
        }
    }

    private Dictionary<PlayersStats, List<Boost>> allBoostDict = new Dictionary<PlayersStats, List<Boost>>();

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            float value = (float)UnityEngine.Random.Range(10, 30) / 100;
            ChangeBoost(true, SenderTypes.Tower, PlayersStats.Health, value);
        }
    }

    private void ChangeBoost(bool mode, SenderTypes sender, PlayersStats stats, float value)
    {
        float newValue = 0;

        List<Boost> currentBoostList = allBoostDict[stats];

        if(mode == true)
        {
            currentBoostList.Add(new Boost(sender, value));
        }
        else
        {
            foreach(var item in currentBoostList)
            {
                if(item.senderType == sender)
                {
                    currentBoostList.Remove(item);
                    break;
                }
            }
        }

        foreach(var item in currentBoostList)
        {
            newValue += item.value;
        }

        EventManager.OnGetPlayerBoostEvent(stats, newValue);
    }

    private void Initialize()
    {
        foreach(PlayersStats itemStat in Enum.GetValues(typeof(PlayersStats)))
        {
            allBoostDict.Add(itemStat, new List<Boost>());
        }
    }
}
