using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    public List<Sprite> spriteList;
    private SpriteRenderer image;
    public float framerate = 0.01f;
    private WaitForSeconds waitTime;

    private Coroutine animator;

    private void Start()
    {
        waitTime = new WaitForSeconds(framerate);
        image = GetComponent<SpriteRenderer>();

        animator = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {            
            foreach (Sprite item in spriteList)
            {
                yield return waitTime;
                image.sprite = item;
            }
        }    
    }

    private void OnDestroy()
    {
        StopCoroutine(animator);
    }
}
