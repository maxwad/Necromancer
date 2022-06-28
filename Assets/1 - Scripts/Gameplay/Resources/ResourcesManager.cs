using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class ResourcesManager : MonoBehaviour
{
    public float Gold { private set; get; } = 1000;
    public float Food { private set; get; } = 100;
    public float Stone { private set; get; } = 10;
    public float Wood { private set; get; } = 10;
    public float Iron { private set; get; } = 10;
    public float Magic { private set; get; } = 0;



    private void AddResource(ResourceType type, float value)
    {
        switch(type)
        {
            case ResourceType.Gold:
                Gold += value;
                break;

            case ResourceType.Food:
                Food += value;
                break;

            case ResourceType.Stone:
                Stone += value;
                break;

            case ResourceType.Wood:
                Wood += value;
                break;

            case ResourceType.Iron:
                Iron += value;
                break;

            case ResourceType.Magic:
                Magic += value;
                break;

            case ResourceType.Nothing:
                break;
            default:
                break;
        }
    }

    private void AddGoldAsBonus(BonusType type, float value)
    {
        if(type == BonusType.Gold)
        {
            Gold += value;
        }
    }

    private bool DecreaseResource(ResourceType type, float value)
    {
        if(CheckResource(type, value) == true)
        {
            switch(type)
            {
                case ResourceType.Gold:
                    Gold -= value;
                    return true;

                case ResourceType.Food:
                    Food -= value;
                    return true;

                case ResourceType.Stone:
                    Stone -= value;
                    return true;

                case ResourceType.Wood:
                    Wood -= value;
                    return true;

                case ResourceType.Iron:
                    Iron -= value;
                    return true;

                case ResourceType.Magic:
                    Magic -= value;
                    return true;

                case ResourceType.Nothing:
                    break;
                default:
                    break;
            }
        }

        return false;
    }

    private bool CheckResource(ResourceType type, float value)
    {
        switch(type)
        {
            case ResourceType.Gold:
                if(Gold >= value) return true;
                break;

            case ResourceType.Food:
                if(Food >= value) return true;
                break;

            case ResourceType.Stone:
                if(Stone >= value) return true;
                break;

            case ResourceType.Wood:
                if(Wood >= value) return true;
                break;

            case ResourceType.Iron:
                if(Iron >= value) return true;
                break;

            case ResourceType.Magic:
                if(Magic >= value) return true;
                break;

            case ResourceType.Nothing:
                break;
            default:
                break;
        }

        return true;
    }

    private void OnEnable()
    {
        EventManager.BonusPickedUp += AddGoldAsBonus;
        EventManager.ResourcePickedUp += AddResource;
    }

    private void OnDisable()
    {
        EventManager.BonusPickedUp -= AddGoldAsBonus;
        EventManager.ResourcePickedUp -= AddResource;
    }
}
