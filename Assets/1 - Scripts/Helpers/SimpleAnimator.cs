using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class SimpleAnimator : MonoBehaviour
{
    public List<Sprite> spriteList;
    public AfterAnimation actionAfterAnimation;
    private SpriteRenderer image;
    public float framerate = 0.01f;
    private WaitForSeconds waitTime;
    private Coroutine animating;

    private bool stopAnimation = false;

    private void OnEnable()
    {
        stopAnimation = false;

        image = GetComponent<SpriteRenderer>();
        if (animating != null) StopCoroutine(animating);

        animating = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        waitTime = new WaitForSeconds(framerate);

        while (true)
        {            
            foreach (Sprite item in spriteList)
            {
                yield return waitTime;

                if(stopAnimation == false)
                {
                    image.sprite = item;
                    if(gameObject.CompareTag(TagManager.T_ENEMY) == true)
                    {
                        image.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);
                    }
                }                             
            }

            if (actionAfterAnimation == AfterAnimation.Destroy)
            {
                DestroyObject();
                break;
            }

            if (actionAfterAnimation == AfterAnimation.SetDisable)
            {
                DisableObject();
                break;
            }
        }    
    }

    public void StopAnimation(bool mode)
    {
        stopAnimation = mode;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

}