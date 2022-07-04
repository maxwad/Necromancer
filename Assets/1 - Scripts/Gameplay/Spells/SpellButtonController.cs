using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static NameManager;

public class SpellButtonController : MonoBehaviour
{
    [SerializeField] private SpellStat spell;
    [SerializeField] private Image veil;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    //private EventTrigger eventTrigger;

    private bool disabledBecauseMana = false;
    private bool disabledBecauseDelay = false;
    private bool isDisabled = false;

    private Coroutine coroutine;
    private float checkTimeMana = 0.2f;
    private float checkTimeDelay = 0.1f;
    private WaitForSeconds checkMana;
    private WaitForSeconds checkDelay;

    private HeroController hero;
    private SpellLibrary spellLibrary;

    public void SetSpellOnButton(SpellStat newSpell)
    {
        spell = newSpell;
    }

    public void InitializeButton()
    {
        hero = GlobalStorage.instance.hero;
        spellLibrary = GlobalStorage.instance.spellManager.GetComponent<SpellLibrary>();

        icon = GetComponent<Image>();
        icon.sprite = spell.icon;

        button = GetComponent<Button>();

        //TODO: tooltip for description of spell
        //eventTrigger = GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();

        button.onClick.AddListener(ActivateSpell);

        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(CheckDisabling());

    }

    private void ActivateSpell()
    {
        if(isDisabled == true) return;

        hero.SpendMana(spell.manaCost);
        spellLibrary.ActivateSpell(spell.spellType, true, spell.value, spell.actionTime);

        StartCoroutine(StartDelay());
    }

    private bool CheckMana()
    {
        float currentManaCount = hero.currentMana;
        return !(currentManaCount - spell.manaCost >= 0);
    }

    private IEnumerator CheckDisabling()
    {
        checkMana = new WaitForSeconds(checkTimeMana);

        while(true)
        {
            yield return checkMana;

            disabledBecauseMana = CheckMana();

            if(disabledBecauseDelay == true || disabledBecauseMana == true)
                isDisabled = true;
            else
                isDisabled = false;

            if(isDisabled == true)
                icon.color = Color.red;
            else
                icon.color = Color.white;
        }
    }

    private IEnumerator StartDelay()
    {
        disabledBecauseDelay = true;

        checkDelay = new WaitForSeconds(checkTimeDelay);
        float currentWaitingTime = 0;

        veil.gameObject.SetActive(true);
        float veilStep = 1 / (spell.reloading / checkTimeDelay);

        while(currentWaitingTime <= spell.reloading)
        {
            currentWaitingTime += checkTimeDelay;
            yield return checkDelay;

            veil.fillAmount -= veilStep;
        }

        veil.gameObject.SetActive(false);
        veil.fillAmount = 1;
        disabledBecauseDelay = false;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(ActivateSpell);
    }
}
