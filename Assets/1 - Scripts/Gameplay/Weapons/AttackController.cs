using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class AttackController : MonoBehaviour
{
    private GameObject weapon;

    private UnitController unitController;
    private WeaponController weaponStats;

    [SerializeField] private float size;
    [SerializeField] private float level;
    [SerializeField] private UnitsAbilities unitAbility;

    private float startAttackDelay = 2f;
    private SpriteRenderer sprite;

    private Coroutine attack;

    private void Start()
    {
        unitController = GetComponent<UnitController>();
        sprite = GetComponent<SpriteRenderer>();

        level = unitController.level;
        unitAbility = unitController.unitAbility;

        if (attack != null) StopCoroutine(attack);

        attack = StartCoroutine(Attack());
    }

    private void OnEnable()
    {
        if (attack != null)
        {
            StopCoroutine(attack);
            attack = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(startAttackDelay);

        while (gameObject != null)
        {
            yield return new WaitForSeconds(unitController.speedAttack);

            weapon = Instantiate(unitController.attackTool, transform.position, Quaternion.identity);
            weapon.transform.SetParent(transform);

            if (sprite.flipX == true)
                weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, 180, weapon.transform.eulerAngles.x);
            else
                weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, 0, weapon.transform.eulerAngles.x);

            weaponStats = weapon.GetComponent<WeaponController>();
            weaponStats.SetAttackParameters(unitController.magicAttack, unitController.physicAttack);
            size = unitController.size;

            weapon.transform.localScale = new Vector3(size, size, size);
        }

    }
}
