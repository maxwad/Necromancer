using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    private GameObject battlePlayer;
    private Vector3 zOffset = new Vector3(0, 0, 10);

    private void LateUpdate()
    {
        if (battlePlayer != null) 
            transform.position = battlePlayer.transform.position - zOffset;
    }

    public void SetBattleCamera(Vector3 startPosition, GameObject player)
    {
        battlePlayer = player;
        transform.position = startPosition;
    }
}
