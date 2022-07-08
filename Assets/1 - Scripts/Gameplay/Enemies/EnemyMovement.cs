using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rbEnemy;
    private Collider2D collEnemy;
    private SpriteRenderer sprite;

    private float speed = 1f;

    //for playmode: 10, for editor: 3
    private float acceleration = 10f;

    private bool canIMove = true;

    void Start()
    {
        player = GlobalStorage.instance.battlePlayer.gameObject;
        collEnemy = GetComponent<Collider2D>();
        rbEnemy = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(canIMove == true)
            Moving();
        else
            rbEnemy.velocity = Vector2.zero;
    }

    private void Moving()
    {
        if (player.transform.position.x - transform.position.x > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;

        rbEnemy.AddForce((player.transform.position - transform.position).normalized
        * Time.fixedDeltaTime * acceleration * speed,
        ForceMode2D.Impulse);       

        rbEnemy.velocity = Vector3.ClampMagnitude(rbEnemy.velocity, speed);
    }

    public void MakeMeFixed(bool mode)
    {
        canIMove = !mode;
        gameObject.GetComponent<SimpleAnimator>().StopAnimation(mode);
    }

    private void OnBecameVisible()
    {
        collEnemy.enabled = true;
    }

    private void OnBecameInvisible()
    {
        collEnemy.enabled = false;
    }
}
