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
        Monk,
        Knight,
        Riddick,
        Hulk
    }

    public enum UnitsHouses
    {
        Kasarma,
        Avanpost,
        Tower,
        Sklep,
        Church
    }

    public enum UnitStats
    {
        Health,
        MagicAttack,
        PhysicAttack,
        MagicDefence,
        PhysicDefence,
        SpeedAttack,
        Size,

        CoinsPrice,
        FoodPrice,
        WoodPrice,
        IronPrice,
        StonePrice,
        MagicPrice,

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
        Torch,
        BonusGold
    }

    public enum AfterAnimation
    {
        Nothing,
        Destroy,
        SetDisable
    }

    public enum ObstacleTypes
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

    public enum Spells
    {
        SpeedUp,
        AttackUp,
        DoubleCrit,
        Shurikens,
        GoAway,
        AllBonuses,
        Healing,
        DoubleBonuses,
        WeaponSize,
        Maning,
        Immortal,
        EnemiesStop,
        DestroyEnemies,
        ExpToGold,
        ResurrectUnit
    }

    public enum BoostSender
    {
        Spell
    }

    public enum UISlotTypes
    {
        Army,
        Reserve
    }
}
