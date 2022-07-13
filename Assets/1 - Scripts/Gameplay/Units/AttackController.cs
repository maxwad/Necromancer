using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StructManager;

public class AttackController : MonoBehaviour
{
    private WeaponStorage weaponStorage;
    private UnitController unitController;

    private float startAttackDelay = 2f;
    private SpriteRenderer sprite;

    private Coroutine attack;

    private void Awake()
    {
        unitController = GetComponent<UnitController>();
        weaponStorage = GetComponent<WeaponStorage>();
        sprite = GetComponent<SpriteRenderer>();

        startAttackDelay = Random.Range(startAttackDelay * 10 - startAttackDelay, startAttackDelay * 10 + startAttackDelay) / 10;

        if (attack != null) StopCoroutine(attack);
        attack = StartCoroutine(Attack());
    }

    private void OnEnable()
    {
        if (attack != null)
        {
            StopCoroutine(attack);
            startAttackDelay = Random.Range(startAttackDelay * 10 - startAttackDelay, startAttackDelay * 10 + startAttackDelay) / 10;
            attack = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(startAttackDelay);

        while (gameObject != null)
        {
            yield return new WaitForSeconds(unitController.speedAttack);

            weaponStorage.Attack(unitController);
        }
    }
}