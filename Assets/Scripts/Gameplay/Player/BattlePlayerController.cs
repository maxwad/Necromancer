using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattlePlayerController : MonoBehaviour
{
    private float speed = 3f;

    private Vector2 currentDirection;
    private bool currentFacing = false;

    [SerializeField] private GameObject hero;
    private SpriteRenderer heroSprite;
    private Animator heroAnimator;

    private GameObject[] army = new GameObject[4];
    private SpriteRenderer[] armySprites = new SpriteRenderer[4];
    private Animator[] armyAnimators = new Animator[4];
    //private SpriteRenderer[] ñountLabel = new SpriteRenderer[4];
    //private MeshRenderer[] armyCounts = new MeshRenderer[4];

    private void Start()
    {
        heroSprite = hero.GetComponent<SpriteRenderer>();
        heroAnimator = hero.GetComponent<Animator>();
    }

    private void Update()
    {
        if (MenuManager.isGamePaused == false && MenuManager.isMiniPause == false)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");

            currentDirection = new Vector2(horizontalMovement, verticalMovement);

            Moving(currentDirection);
            DrawUnit(horizontalMovement);
            Animations(currentDirection);
        }
    }

    private void Moving(Vector2 direction)
    {
        transform.position += (Vector3)direction * Time.deltaTime * speed;
    }

    private void DrawUnit(float direction)
    {
        if (direction == -1) currentFacing = false;

        if (direction == 1) currentFacing = true;

        heroSprite.flipX = currentFacing;
        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] != null)
            {
                armySprites[i].flipX = currentFacing;

                //right sprite order of units
                armySprites[i].sortingOrder = i * 3;

                //right sprite order of count label               
                //ñountLabel[i].sortingOrder = i * 3;
                //armyCounts[i].sortingOrder = i * 3;
            }
                
        }
    }

    private void Animations(Vector2 movement)
    {
        bool runningFlag = movement != Vector2.zero ? true : false;

        heroAnimator.SetBool("bRunning", runningFlag);

        for (int i = 0; i < army.Length; i++)
        {
            if (army[i] != null)
                armyAnimators[i].SetBool("bRunning", runningFlag);
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

                //ñountLabel[i] = army[i].transform.Find("CountBG").GetComponent<SpriteRenderer>();
                //armyCounts[i] = army[i].GetComponentInChildren<MeshRenderer>();
            }
            else
            {
                armySprites[i] = null;
                armyAnimators[i] = null;
                //ñountLabel[i] = null;
                //armyCounts[i] = null;
            }

            
        }
    }

    public void UpdateArmyCount(Unit[] army)
    {

    }
}
