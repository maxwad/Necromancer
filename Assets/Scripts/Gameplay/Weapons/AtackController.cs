using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class AtackController : MonoBehaviour
{
    private GameObject weapon;

    private UnitController unitController;
    private WeaponStats weaponStats;

    [SerializeField] private float size;
    [SerializeField] private float level;
    [SerializeField] private UnitsAbilities unitAbility;

    private float startAttackDelay = 2f;
    private SpriteRenderer sprite;

    private void Start()
    {
        unitController = GetComponent<UnitController>();
        sprite = GetComponent<SpriteRenderer>();

        GetStartParameterOnce();

        StartCoroutine(Attack());
    }

    private void Update()
    {
        GetParametersPeriodically();
    }


    #region Set Weapon Parameter
    private void GetStartParameterOnce()
    {
        CreateWeapon(unitController.attackTool);

        level = unitController.level;
        unitAbility = unitController.unitAbility;

        weaponStats = weapon.GetComponent<WeaponStats>();
    }

    private void GetParametersPeriodically()
    {
        size = unitController.size;

        weaponStats.SetAttackParameters(unitController.magicAttack, unitController.physicAttack);
    }

    private void CreateWeapon(GameObject prefab)
    {
        weapon = Instantiate(prefab, transform.position, Quaternion.identity);
        weapon.transform.SetParent(transform);
    }

    #endregion

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(startAttackDelay);

        while (gameObject != null)
        {
            yield return new WaitForSeconds(unitController.speedAttack);

            if (sprite.flipX == true)
                weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, 180, weapon.transform.eulerAngles.x);
            else
                weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, 0, weapon.transform.eulerAngles.x);

            weapon.transform.localScale = new Vector3(size, size, size);
            weapon.SetActive(true);
        }

    }
}
