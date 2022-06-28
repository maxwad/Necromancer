using Antlr.Runtime.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float levelBase = 1;
    [SerializeField] private float levelBoost = 0;
    [SerializeField] private float currentLevel;

    [SerializeField] private float healthBase = 500f;
    [SerializeField] private float healthBoost = 0;
    [SerializeField] private float currentHealth;

    [SerializeField] private float manaBase = 50f;
    [SerializeField] private float manaBoost = 0;
    [SerializeField] private float currentMana;

    [SerializeField] private float searchRadiusBase = 2f;
    [SerializeField] private float searchRadiusBoost = 0;
    [SerializeField] private float currentSearchRadius;

    [SerializeField] private float speedBase = 200f;
    [SerializeField] private float speedBoost = 0;
    [SerializeField] private float currentSpeed;

    [SerializeField] private float defenceBase = 200f;
    [SerializeField] private float defenceBoost = 0;
    [SerializeField] private float currentDefence;

    [SerializeField] private float regenerationBase = 1f;
    [SerializeField] private float regenerationBoost = 0;
    [SerializeField] private float currentRegeneration;

    private void Start()
    {
        UpgradePlayersStats();
    }

    private void UpgradePlayersStats()
    {
        currentLevel = levelBase + (levelBase * levelBoost);
        EventManager.OnGetPlayerStatEvent(PlayersStats.Level, currentLevel);

        currentHealth = healthBase + (healthBase * healthBoost);
        EventManager.OnGetPlayerStatEvent(PlayersStats.Health, currentHealth);

        currentMana = manaBase + (manaBase * manaBoost);
        EventManager.OnGetPlayerStatEvent(PlayersStats.Mana, currentMana);

        currentSearchRadius = searchRadiusBase + (searchRadiusBase * searchRadiusBoost);
        EventManager.OnGetPlayerStatEvent(PlayersStats.SearchRadius, currentSearchRadius);

        currentSpeed = speedBase + (speedBase * speedBoost);
        EventManager.OnGetPlayerStatEvent(PlayersStats.Speed, currentSpeed);

        currentDefence = defenceBase + (defenceBase * defenceBoost);
        EventManager.OnGetPlayerStatEvent(PlayersStats.Defence, currentDefence);

        currentRegeneration = regenerationBase + (regenerationBase * regenerationBoost);
        EventManager.OnGetPlayerStatEvent(PlayersStats.Regeneration, currentRegeneration);
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

    private void OnEnable()
    {
        EventManager.GetPlayerBoost += UpdateBoost;
    }

    private void OnDisable()
    {
        EventManager.GetPlayerBoost -= UpdateBoost;
    }
}
