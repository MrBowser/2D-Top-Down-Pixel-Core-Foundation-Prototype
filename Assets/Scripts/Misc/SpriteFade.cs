using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float fadeTime = .4f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }





    public IEnumerator SlowFadeRoutine()
    {
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;

        // fade time is the amount of time it fades, new alpha calculates the fading transparency in the moment as time increases
        //essentially where it fallson the transpency slider over time and then applies that to the sprite or till
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
