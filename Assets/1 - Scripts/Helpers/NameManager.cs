using System;
using System.Collections.Generic;
using UnityEngine;

public static class NameManager
{
    public enum UnitsTypes
    {
        Kosar,
        Ranger,
        Meg,
        Skeleton,
        Monk
    }

    public enum UnitsHouses
    {
        Kasarma,
        Avanpost,
        Tower,
        Sklep,
        Church
    }

    public enum UnitsAbilities
    {
        Kosar_lvl1,
        Kosar_lvl2,
        Kosar_lvl3,
        Ranger_lvl1,
        Ranger_lvl2,
        Ranger_lvl3,
        Meg_lvl1,
        Meg_lvl2,
        Meg_lvl3,
        Skeleton_lvl1,
        Skeleton_lvl2,
        Skeleton_lvl3,
        Monk_lvl1,
        Monk_lvl2,
        Monk_lvl3
    }

    public enum EnemiesTypes
    {
        Goblin,
        Zombie,
        Samurai
    }

    public enum EnemyAbilities 
    { 
        Empty
    }

    public enum ObjectPool
    {
        Enemy,
        DamageText,
        BonusExp,
        Torch
    }

    public enum AfterAnimation
    {
        Nothing,
        Destroy,
        SetDisable
    }

    public enum SenderTypes
    {
        Enemy,
        EnemyBoss,
        Obstacle,
        Torch,
        Tower
    }

    public enum BonusType
    {
        Health,
        Mana,
        Gold,
        TempExp,
        Exp,
        Other,
        Nothing
    }

    public enum ResourceType
    {
        Gold,
        Food,
        Stone,
        Wood,
        Iron,
        Magic,
        Nothing
    }

    public enum PlayersStats
    {
        Level,
        Health,
        Mana,
        Speed,
        SearchRadius,
        Defence,
        Infirmary,
        Luck,

        MovementDistance,
        HealthRegeneration,
        RadiusView,
        ExtraResourcesProduce,
        ExtraReward,

        Spell,
        NegativeCell,
        MedicAltar,

        MedicTry,
        EnemyView,
        Portal,
        ExtraResourcesReward,
        ManaRegeneration,
        HeroArmyToEnemy
    }
}
