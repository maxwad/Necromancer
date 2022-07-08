using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class SpellLibrary : MonoBehaviour
{
    [SerializeField] private GameObject shuriken;
    [SerializeField] private GameObject kickAssCircle;

    private List<Spells> currentSpells = new List<Spells>();

    public void ActivateSpell(Spells spell, bool mode, float value = 0, float duration = 0)
    {
        switch(spell)
        {
            case Spells.SpeedUp:
                SpeedUp(mode, value);
                break;

            case Spells.AttackUp:
                AttackUp(mode, value);
                break;

            case Spells.DoubleCrit:
                DoubleCrit(mode, value);
                break;

            case Spells.Shurikens:
                Shurikens(mode, value);
                break;

            case Spells.GoAway:
                GoAway(mode, value);
                break;

            case Spells.AllBonuses:
                AllBonuses(mode, value);
                break;

            case Spells.Healing:
                Healing(mode, value);
                break;

            case Spells.DoubleBonuses:
                DoubleBonuses(mode, value);
                break;

            case Spells.WeaponSize:
                WeaponSize(mode, value);
                break;

            case Spells.Maning:
                Maning(mode, value);
                break;

            case Spells.Immortal:
                Immortal(mode, value);
                break;

            case Spells.EnemiesStop:
                EnemiesStop(mode, value);
                break;

            case Spells.DestroyEnemies:
                DestroyEnemies(mode, value);
                break;

            case Spells.ExpToGold:
                ExpToGold(mode, value);
                break;

            case Spells.ResurrectUnit:
                ResurrectUnit(mode, value);
                break;

            default:
                break;
        }

        if(mode == true)
        {
            currentSpells.Add(spell);
            StartCoroutine(DeactivateSpell(spell, duration));
        }
        else
        {
            currentSpells.Remove(spell);
        }
    }

    #region Helpers

    private IEnumerator DeactivateSpell(Spells spell, float duration)
    {
        yield return new WaitForSeconds(duration);

        ActivateSpell(spell, false);
    }

    private void DeactivateAllSpells(bool mode)
    {
        if(mode == true)
        {
            foreach(var item in currentSpells)
{
                StartCoroutine(DeactivateSpell(item, 0));
            }
        }
    }

    private void OnEnable()
    {
        EventManager.ChangePlayer += DeactivateAllSpells;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayer -= DeactivateAllSpells;
    }

    #endregion

    //Increases the hero's movement speed by 20% for 30 seconds.
    private void SpeedUp(bool mode, float value)
    {
        EventManager.OnBoostStatEvent(mode, BoostSender.Spell, PlayersStats.Speed, value);
    }


    //Increase attack power by 20% for 30 seconds.
    private void AttackUp(bool mode, float value)
    {
        EventManager.OnBoostUnitStatEvent(true, mode, BoostSender.Spell, UnitStats.PhysicAttack, value);
        EventManager.OnBoostUnitStatEvent(true, mode, BoostSender.Spell, UnitStats.MagicAttack, value);
    }


    //All damage becomes critical for 5 seconds.
    private void DoubleCrit (bool mode, float value)
    {
        EventManager.OnBoostStatEvent(mode, BoostSender.Spell, PlayersStats.Luck, value);
    }


    //Increases weapon size by 20% for 30 seconds.
    private void WeaponSize(bool mode, float value)
    {
        EventManager.OnBoostUnitStatEvent(true, mode, BoostSender.Spell, UnitStats.Size, value);
    }


    //Hero throws 8 shurikens that kill everyone in their path.
    private void Shurikens(bool mode, float value)
    {
        if(mode == true)
        {
            float angleItem = 360 / value;

            for(int i = 0; i < value; i++)
            {
                GameObject item = Instantiate(shuriken);
                item.transform.position = GlobalStorage.instance.hero.transform.position;
                item.transform.rotation = Quaternion.Euler(0f, 0f, angleItem * i);             
            }
        }
    }


    //Knock back all enemies within a radius of 10 meters.
    private void GoAway(bool mode, float value)
    {
        if(mode == true)
        {            
            GameObject circle = Instantiate(kickAssCircle);
            circle.transform.position = GlobalStorage.instance.hero.transform.position;
            circle.transform.SetParent(GlobalStorage.instance.effectsContainer.transform);

            float currentSize = 0;
            StartCoroutine(Size());

            IEnumerator Size()
            {
                WaitForSeconds delay = new WaitForSeconds(0.01f);
                while(currentSize <= value * 2)
                {
                    currentSize += 0.4f;
                    circle.transform.localScale = new Vector3(currentSize, currentSize, 1);
                    yield return delay;
                };

                Destroy(circle);
            }
        }
    }


    //Pull all the bonuses in the field.
    private void AllBonuses(bool mode, float value)
    {
        if(mode == true)
        {
            List<GameObject> bonusList = new List<GameObject>();
            bonusList = GlobalStorage.instance.bonusManager.bonusesOnTheMap;
            foreach(var bonus in bonusList)
            {
                bonus.GetComponent<BonusController>().ActivatateBonus();
            }
        }
    }


    //Heal the hero by 20%.
    private void Healing(bool mode, float value)
    {
        if(mode == true)
        {
            EventManager.OnBonusPickedUpEvent(BonusType.Health, value);
        }
    }


    //Restore 20% mana.
    private void Maning(bool mode, float value)
    {
        if(mode == true)
        {
            EventManager.OnBonusPickedUpEvent(BonusType.Mana, value);
        }
    }


    //Double all bonuses for 30 seconds.
    private void DoubleBonuses(bool mode, float value)
    {
        //boost new bonuses
        GlobalStorage.instance.bonusManager.BoostBonus(value);

        //boost existing bonuses
        foreach(Transform child in GlobalStorage.instance.objectsPoolManager.transform)
        {
            BonusController bonus = child.GetComponent<BonusController>();
            if(bonus != null) bonus.BoostBonusValue(value);
        }

        foreach(Transform child in GlobalStorage.instance.bonusesContainer.transform)
        {
            BonusController bonus = child.GetComponent<BonusController>();
            if(bonus != null) bonus.BoostBonusValue(value);
        }
    }


    //Make units immortal for 30 seconds.
    private void Immortal(bool mode, float value)
    {
        EventManager.OnSpellImmortalEvent(mode);
    }


    //Stop all enemies for 10 seconds.
    private void EnemiesStop(bool mode, float value)
    {
        List<GameObject> enemies = GlobalStorage.instance.battleMap.GetComponent<EnemySpawner>().enemiesOnTheMap;

        foreach(var enemy in enemies)
            enemy.GetComponent<EnemyMovement>().MakeMeFixed(mode);
        // do we need stop spawn enemy while they freeze?
    }


    //Destroy all enemies within a radius of 15 meters.
    private void DestroyEnemies(bool mode, float value)
    {
        if(mode == true)
        {
            List<GameObject> enemies = GlobalStorage.instance.battleMap.GetComponent<EnemySpawner>().enemiesOnTheMap;
            GameObject hero = GlobalStorage.instance.hero.gameObject;

            int count = enemies.Count - 1;

            for(int i = count; i >= 0; i--)
            {
                if(Vector2.Distance(enemies[i].transform.position, hero.transform.position) <= value)
                {
                    enemies[i].GetComponent<EnemyController>().Kill();
                }
            }            
        }
    }


    //Turn all experience bonuses within 20 meters into gold.
    private void ExpToGold(bool mode, float value)
    {
        if(mode == true)
        {
            List<GameObject> bonuses = GlobalStorage.instance.bonusManager.bonusesOnTheMap;

            int count = bonuses.Count - 1;

            for(int i = count; i >= 0; i--)
            {
                BonusController bonus = bonuses[i].GetComponent<BonusController>();
                if(bonus.bonusType == BonusType.TempExp)
                {
                    GlobalStorage.instance.bonusManager.CreateBonus(BonusType.Gold, bonus.transform.position);
                    bonus.DestroyMe();
                }
            }
        }
    }


    //Resurrect one last killed unit from the infirmary.
    private void ResurrectUnit(bool mode, float value)
    {
        if(mode == true)
        {
            EventManager.OnRemoveUnitFromInfirmaryEvent(mode, true, 1);
        }
    }
}
