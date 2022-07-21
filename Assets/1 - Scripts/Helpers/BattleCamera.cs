using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    private GameObject battlePlayer;
    private Vector3 zOffset = new Vector3(0, 0, 10);

    private float originalCameraSize;
    private float shakePercent = 1.5f / 100;
    private float shakingInterval = 0.05f;
    private float timeCount = 0f;
    private float shakingDuring = 0.5f;
    private Coroutine coroutine;

    private void Start()
    {
        originalCameraSize = Camera.main.orthographicSize;
    }

    private void Update()
    {
        if (battlePlayer != null) 
            transform.position = battlePlayer.transform.position - zOffset;
    }

    public void SetBattleCamera(Vector3 startPosition, GameObject player)
    {
        battlePlayer = player;
        transform.position = startPosition;
    }


    #region Shaking

    public void ResetCameraShakingPoint(float shakingTime = 0.5f)
    {
        timeCount = 0f;
        Camera.main.orthographicSize = originalCameraSize;
        shakingDuring = shakingTime;
    }

    private IEnumerator Shake()
    {        
        while(timeCount < shakingDuring)        
        {
            timeCount += shakingInterval;
            Camera.main.orthographicSize = (Camera.main.orthographicSize == originalCameraSize) ? 
                (originalCameraSize - (originalCameraSize * shakePercent)) : 
                originalCameraSize;

            yield return new WaitForSeconds(shakingInterval);
        }

        ResetCameraShakingPoint();
    }

    public void ShakeCamera()
    {
        if(coroutine != null) StopCoroutine(coroutine);

        coroutine = StartCoroutine(Shake());
    }

    #endregion
}
