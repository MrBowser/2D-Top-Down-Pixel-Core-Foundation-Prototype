using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake()
    {   
        spriteRenderer= GetComponent<SpriteRenderer>();
        tilemap= GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //this triggers the associated coroutine if the player hits a transparency object like the canopy(tilemap) or tree (sprite renderer)
        if(other.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            }
            else if (tilemap)
            {
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // this puts the object back to its opaque state
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1));
            }
            else if (tilemap)
            {
                

             if (tilemap.isActiveAndEnabled)
                {
                    StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1));
                }
                
            }
            
        }

    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        //fade time is the amount of time it fades, new alpha calculates the fading transparency in the moment as time increases
        //essentially where it fallson the transpency slider over time and then applies that to the sprite or till
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime+= Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue,targetTransparency,elapsedTime/ fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }



}
