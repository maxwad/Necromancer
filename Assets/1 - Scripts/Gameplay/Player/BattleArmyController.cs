using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static NameManager;

public class BattleArmyController : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private float speed = 200f;
    private float speedBoost = 0;

    private Vector2 currentDirection;
    private bool currentFacing = false;

    [SerializeField] private GameObject hero;
    private SpriteRenderer heroSprite;
    private Animator heroAnimator;

    private GameObject[] army = new GameObject[4];
    private SpriteRenderer[] armySprites = new SpriteRenderer[4];
    private Animator[] armyAnimators = new Animator[4];
    private SpriteRenderer[] ñountLabel = new SpriteRenderer[4];
    private MeshRenderer[] armyCountsMesh = new MeshRenderer[4];
    private TMP_Text[] armyCountsText = new TMP_Text[4];

    private void Start()
    {
        hero.SetActive(true);
        rbPlayer = GetComponent<Rigidbody2D>();
        heroSprite = hero.GetComponent<SpriteRenderer>();
        heroAnimator = hero.GetComponent<Animator>();
    }

    private void Update()
    {
        if (MenuManager.isGamePaused == false && MenuManager.isMiniPause == false)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");

            currentDirection = new Vector2(horizontalMovement, verticalMovement).normalized;

            Moving(currentDirection);
            DrawUnit(horizontalMovement);
            Animations(currentDirection);
        }
    }

    private void Moving(Vector2 direction)
    {
        rbPlayer.velocity = (Vector3)direction * Time.fixedDeltaTime * (speed + (speed * speedBoost));
    }

    private void DrawUnit(float direction)
    {
        if (direction == -1) currentFacing = false;

        if (direction == 1) currentFacing = true;

        heroSprite.flipX = currentFacing;
        heroSprite.sortingOrder = -Mathf.RoundToInt(hero.transform.position.y * 100);

        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] != null)
            {
                armySprites[i].flipX = currentFacing;

                //right sprite order of units
                //armySprites[i].sortingOrder = i * 3;
                armySprites[i].sortingOrder = -Mathf.RoundToInt(army[i].transform.position.y * 100);
                //right sprite order of count label               
                ñountLabel[i].sortingOrder = -Mathf.RoundToInt(army[i].transform.position.y * 100);
                armyCountsMesh[i].sortingOrder = -Mathf.RoundToInt(army[i].transform.position.y * 100);
            }
        }
    }

    private void Animations(Vector2 movement)
    {
        bool runningFlag = movement != Vector2.zero ? true : false;

        heroAnimator.SetBool(TagManager.A_RUN, runningFlag);

        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] != null)
                armyAnimators[i].SetBool(TagManager.A_RUN, runningFlag);
        }    
    }

    public void GetArmy(GameObject[] newArmy)
    {
        army = newArmy;

        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] != null)
            {
                armySprites[i] = army[i].GetComponent<SpriteRenderer>();
                armySprites[i].flipX = currentFacing;

                armyAnimators[i] = army[i].GetComponent<Animator>();

                ñountLabel[i] = army[i].transform.Find("CountBG").GetComponent<SpriteRenderer>();
                armyCountsMesh[i] = army[i].GetComponentInChildren<MeshRenderer>();
                armyCountsText[i] = army[i].GetComponentInChildren<TMP_Text>();
            }
            else
            {
                armySprites[i] = null;
                armyAnimators[i] = null;
                ñountLabel[i] = null;
                armyCountsMesh[i] = null;
                armyCountsText[i] = null;
            }            
        }
    }

    public void UpdateArmyCount(UnitsTypes unitType, int quantity)
    {
        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] != null && army[i].GetComponent<UnitController>().GetTypeUnit() == unitType)
            {
                armyCountsText[i].text = quantity.ToString();
                break;
            }
        }
    }

    public void SetSpeedBoost(float boost)
    {
        speedBoost = boost;
    }

    private void OnEnable()
    {
        EventManager.WeLostOneUnit += UpdateArmyCount;
    }

    private void OnDisable()
    {
        EventManager.WeLostOneUnit -= UpdateArmyCount;
    }
}
