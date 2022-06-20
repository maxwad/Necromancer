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
                if (gameObject.CompareTag(TagManager.T_ENEMY) == true)
                {
                    image.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);
                }                
            }
        }    
    }

    private void OnDestroy()
    {
        StopCoroutine(animator);
    }
}
