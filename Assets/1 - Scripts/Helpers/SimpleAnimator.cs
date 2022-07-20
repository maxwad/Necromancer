using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NameManager;

public class SimpleAnimator : MonoBehaviour
{
    public List<Sprite> spriteList;
    public List<Sprite> spriteListAttack;
    private List<Sprite> currentSpriteList = new List<Sprite>();

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

        currentSpriteList = spriteList;

        animating = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        waitTime = new WaitForSeconds(framerate);

        while (true)
        {            
            foreach (Sprite item in currentSpriteList)
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

    public void ChangeAnimation(Animations animation)
    {
        if(animating != null) StopCoroutine(animating);

        if(animation == Animations.Attack && spriteListAttack.Count != 0) currentSpriteList = spriteListAttack;

        if(animation == Animations.Walk && spriteList.Count != 0) currentSpriteList = spriteList;

        animating = StartCoroutine(Animate());
    }

    public void SetSpeed(float newSpeed)
    {
        StopCoroutine(animating);

        framerate = newSpeed;
        animating = StartCoroutine(Animate());
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
