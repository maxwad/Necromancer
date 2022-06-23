using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject veil;
    [SerializeField] private Image fadeScreen;

    [SerializeField] private GameObject battlePlayer;
    [SerializeField] private GameObject globalPlayer;

    private Vector3 globalCameraPosition = new Vector3(0, 0, -10);
    private Vector3 battleCameraPosition = new Vector3(0, 0, 10);

    private GlobalCamera globalCameraMode;
    private BattleCamera battleCameraMode;

    private void Start()
    {
        globalCameraMode = GetComponent<GlobalCamera>();
        battleCameraMode = GetComponent<BattleCamera>();
    }

    private void FadeIn(bool mode)
    {
        StartCoroutine(StartFadeIn(mode));
    }

    private IEnumerator StartFadeIn(bool mode)
    {
        WaitForSecondsRealtime smallWait = new WaitForSecondsRealtime(0.01f);
        WaitForSecondsRealtime bigWait = new WaitForSecondsRealtime(0.25f);

        veil.SetActive(true);

        float alfa = 0;
        Color currentColor = fadeScreen.color;

        while (alfa < 1)
        {
            alfa += 0.05f;
            currentColor.a = alfa;
            fadeScreen.color = currentColor;

            yield return smallWait;
        }

        //switch camera behavior mode
        if (mode == true)
        {            
            globalCameraMode.enabled = true;
            battleCameraMode.enabled = false;
            globalCameraMode.SetGlobalCamera(globalCameraPosition, globalPlayer);
        }
        else
        {
            globalCameraPosition = transform.position;

            globalCameraMode.enabled = false;
            battleCameraMode.enabled = true;
            battleCameraMode.SetBattleCamera(battleCameraPosition, battlePlayer);
        }

        yield return bigWait;
        EventManager.OnChangePlayerEvent(mode);
        yield return bigWait;

        while (alfa > 0)
        {
            alfa -= 0.05f;
            currentColor.a = alfa;
            fadeScreen.color = currentColor;

            yield return smallWait;
        }

        veil.SetActive(false);
    }


    private void OnEnable()
    {
        EventManager.ChangePlayMode += FadeIn;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayMode -= FadeIn;
    }
}
