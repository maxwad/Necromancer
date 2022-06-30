using Antlr.Runtime.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float levelBase = 0;
    [SerializeField] private float levelBoost = 0;
    [SerializeField] private float currentMaxLevel;

    [Space]
    [SerializeField] private float healthBase = 500f;
    [SerializeField] private float healthBoost = 0;
    [SerializeField] private float currentMaxHealth;

    [Space]
    [SerializeField] private float manaBase = 50f;
    [SerializeField] private float manaBoost = 0;
    [SerializeField] private float currentMaxMana;

    [Space]
    [SerializeField] private float searchRadiusBase = 2f;
    [SerializeField] private float searchRadiusBoost = 0;
    [SerializeField] private float currentMaxSearchRadius;

    [Space]
    [SerializeField] private float speedBase = 200f;
    [SerializeField] private float speedBoost = 0;
    [SerializeField] private float currentMaxSpeed;

    [Space]
    [SerializeField] private float defenceBase = 200f;
    [SerializeField] private float defenceBoost = 0;
    [SerializeField] private float currentMaxDefence;

    [Space]
    [SerializeField] private float regenerationBase = 1f;
    [SerializeField] private float regenerationBoost = 0;
    [SerializeField] private float currentMaxRegeneration;

    private void Start()
    {
        UpgradePlayersStats();
    }

    private void UpgradePlayersStats()
    {
        currentMaxLevel = levelBase + (levelBase * levelBoost);
        EventManager.OnUpgradePlayerStatEvent(PlayersStats.Level, currentMaxLevel);

        currentMaxHealth = healthBase + (healthBase * healthBoost);
        EventManager.OnUpgradePlayerStatEvent(PlayersStats.Health, currentMaxHealth);

        currentMaxMana = manaBase + (manaBase * manaBoost);
        EventManager.OnUpgradePlayerStatEvent(PlayersStats.Mana, currentMaxMana);

        currentMaxSearchRadius = searchRadiusBase + (searchRadiusBase * searchRadiusBoost);
        EventManager.OnUpgradePlayerStatEvent(PlayersStats.SearchRadius, currentMaxSearchRadius);

        currentMaxSpeed = speedBase + (speedBase * speedBoost);
        EventManager.OnUpgradePlayerStatEvent(PlayersStats.Speed, currentMaxSpeed);

        currentMaxDefence = defenceBase + (defenceBase * defenceBoost);
        EventManager.OnUpgradePlayerStatEvent(PlayersStats.Defence, currentMaxDefence);

        currentMaxRegeneration = regenerationBase + (regenerationBase * regenerationBoost);
        EventManager.OnUpgradePlayerStatEvent(PlayersStats.Regeneration, currentMaxRegeneration);
    }

    private void UpdateBoost(PlayersStats stats, float value)
    {
        switch(stats)
        {
            case PlayersStats.Level:
                levelBoost = value;
                break;

            case PlayersStats.Health:
                healthBoost = value;
                break;

            case PlayersStats.Mana:
                manaBoost = value;
                break;

            case PlayersStats.Speed:
                speedBoost = value;
                break;

            case PlayersStats.SearchRadius:
                searchRadiusBoost = value;
                break;

            case PlayersStats.Defence:
                defenceBoost = value;
                break;

            case PlayersStats.Regeneration:
                regenerationBoost = value;
                break;

            default:
                break;
        }

        UpgradePlayersStats();
    }

    private void SendStartParemeters(bool mode)
    {
        //we need to delay sending because not all scripts subscribed on event in call moment.
        if(mode == false) Invoke("UpgradePlayersStats", 0.1f);
    }

    //for starting current values in HeroController
    public float GetStartParameter(PlayersStats stat)
    {
        if(stat == PlayersStats.Health) return currentMaxHealth;

        if(stat == PlayersStats.Mana) return currentMaxMana;

        if(stat == PlayersStats.Level) return currentMaxLevel;

        return 0;
    }

    private void OnEnable()
    {
        EventManager.GetPlayerBoost += UpdateBoost;
        EventManager.ChangePlayer += SendStartParemeters;
    }

    private void OnDisable()
    {
        EventManager.GetPlayerBoost -= UpdateBoost;
        EventManager.ChangePlayer -= SendStartParemeters;
    }
}
