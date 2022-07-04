using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static NameManager;

public class SpellLibrary : MonoBehaviour
{
    private List<Spells> currentSpells = new List<Spells>();

    public void ActivateSpell(Spells spell, bool mode, float value = 0, float duration = 0)
    {
        switch(spell)
        {
            case Spells.SpeedUp:
                SpeedUp(mode, value);
                break;

            case Spells.AttackUp:
                break;

            case Spells.DoubleCrit:
                break;

            case Spells.Shurikens:
                break;

            case Spells.GoAway:
                break;

            case Spells.AllBonuses:
                break;

            case Spells.Healing:
                break;

            case Spells.ExtraExp:
                break;

            case Spells.WeaponSize:
                break;

            case Spells.Maning:
                break;

            case Spells.Immortal:
                break;

            case Spells.EnemiesStop:
                break;

            case Spells.DestroyEnemies:
                break;

            case Spells.ExpToGold:
                break;

            case Spells.ResurrectUnit:
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

    private void SpeedUp(bool mode, float value)
    {
        if(mode == true)
        {

        }
        else
        {

        }
    }

    private void SomeSpell(bool mode, float value)
    {
        if(mode == true)
        {

        }
        else
        {

        }
    }
}
