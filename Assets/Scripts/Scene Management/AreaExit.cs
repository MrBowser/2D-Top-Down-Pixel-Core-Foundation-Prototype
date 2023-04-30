using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoadTime = 1f;

    //as the player hits the trigger area in the it sets the scenemanagement singleton name to be stored for where to spawn and loads the next scene
    //once it sufficiently fades to black for the effect, on the next scene load area entrance script is triggered which determines where the player
    //spawns looking at the name stored in singleton

    //note the area entrance object is a child of the area exit object prefab

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());

        }
    }


    private IEnumerator LoadSceneRoutine()
    {
        
        while(waitToLoadTime >=0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);

        


    }
}
