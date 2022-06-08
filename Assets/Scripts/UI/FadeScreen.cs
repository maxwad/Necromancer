using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private GameObject veil;
    [SerializeField] private Image fadeScreen;

    private Vector3 globalCameraPosition = new Vector3(0, 0, 0);
    private Vector3 battleCameraPosition = new Vector3(0, 0, 10);

    private void FadeIn(bool mode)
    {
        StartCoroutine(StartFadeIn(mode));
    }

    private IEnumerator StartFadeIn(bool mode)
    {
        veil.SetActive(true);

        float alfa = 0;
        Color currentColor = fadeScreen.color;

        while (alfa < 1)
        {
            alfa += 0.05f;
            currentColor.a = alfa;
            fadeScreen.color = currentColor;

            yield return new WaitForSeconds(0.01f);
        }

        if (mode == true)
        {
            //switch camera mode
            globalCameraPosition = transform.position;
            transform.position = battleCameraPosition;
        }
        else
        {
            transform.position = globalCameraPosition;
        }        

        yield return new WaitForSeconds(0.5f);

        while (alfa > 0)
        {
            alfa -= 0.05f;
            currentColor.a = alfa;
            fadeScreen.color = currentColor;

            yield return new WaitForSeconds(0.01f);
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
