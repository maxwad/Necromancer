using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCamera : MonoBehaviour
{
    private GameObject globalPlayer;
    private Vector3 zOffset = new Vector3(0, 0, 10);

    private void Update()
    {
        if (globalPlayer != null)
            transform.position = globalPlayer.transform.position - zOffset;
    }

    public void SetGlobalCamera(Vector3 startPosition, GameObject player)
    {
        globalPlayer = player;
        transform.position = startPosition;
    }

    public void ResetGlobalCamera()
    {
        transform.position = globalPlayer.transform.position;
    }
}
