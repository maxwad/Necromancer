using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GlobalStorage.instance.battlePlayer;

    }

    void Update()
    {
        
    }
}
