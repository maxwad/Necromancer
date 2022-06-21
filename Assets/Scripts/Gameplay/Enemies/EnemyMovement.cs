using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rbEnemy;
    private SpriteRenderer sprite;

    private float speed = 1.5f;
    private float speedBoost = 0;


    void Start()
    {
        player = GlobalStorage.instance.battlePlayer;
        rbEnemy = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (player.transform.position.x - transform.position.x > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
        
        rbEnemy.AddForce((player.transform.position - transform.position).normalized * Time.fixedDeltaTime * 2 * (speed + (speed * speedBoost)), ForceMode2D.Impulse);
        rbEnemy.velocity = Vector3.ClampMagnitude(rbEnemy.velocity, speed + (speed * speedBoost));  
    }
}
