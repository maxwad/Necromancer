using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballController : MonoBehaviour
{
    private Vector2 destination;
    private float speed = 3;

    private bool canIMove = false;

    private void Update()
    {
        //if(canIMove == true) Movement();
    }

    public void Initialize(Vector2 point)
    {
        destination = point;
        canIMove = true;
    }

    private void Movement()
    {
        Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        if(destination == (Vector2)transform.position)
        {
            Explosion();
        }
    }

    private void Explosion()
    {

        Debug.Log("Boom");
        Destroy(gameObject);
    }
}
