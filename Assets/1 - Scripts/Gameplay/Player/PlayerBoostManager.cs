using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class PlayerBoostManager : MonoBehaviour
{
    private float levelBoost = 0;

    private float healthBoost = 0;

    private float manaBoost = 0;

    private float searchRadiusBoost = 0;

    private float speedBoost = 0;

    private float defenceBoost = 0;

    private float regenerationBoost = 0;

    private void ChangeBoost(PlayersStats stats, float value)
    {
        float newValue = 0;
        switch(stats)
        {
            case PlayersStats.Level:
                levelBoost += value;
                newValue = levelBoost;
                break;

            case PlayersStats.Health:
                healthBoost += value;
                newValue = healthBoost;
                break;

            case PlayersStats.Mana:
                manaBoost += value;
                newValue = manaBoost;
                break;

            case PlayersStats.Speed:
                speedBoost += value;
                newValue = speedBoost;
                break;

            case PlayersStats.SearchRadius:
                searchRadiusBoost += value;
                newValue = searchRadiusBoost;
                break;

            case PlayersStats.Defence:
                defenceBoost += value;
                newValue = defenceBoost;
                break;

            case PlayersStats.Regeneration:
                regenerationBoost += value;
                newValue = regenerationBoost;
                break;

            default:
                break;
        }

        EventManager.OnGetPlayerBoostEvent(stats, newValue);
    }
}
