using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed =1f;

    private IEnumerator fadeRoutine;

    //the below is used to fade to black and fade back into clear during scene transitions
    //uses sames scripts but 1 means completely black with the alpha and 0 means completely clear, the fadescreen is just a black box as an image  in the ui canvas
    public void FadeToBlack()
    {
        if(fadeRoutine !=null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);

    }

    public void FadeToClear() 
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);

    }

    

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        //approximately is used to help with floats since they might miss eachother at fringe decimal levels
        // movetowards is what the alpha value should be the target and how fast it changes with fadescreen.color being the implementation of it
        while(!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);

            yield return null;
        }
    }
}
