using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    //for the script to work the transition name needs to match the corresponding area exit scenetransitionname field as that stores where the player is comning from
    //the below pulls the stored value explained above and spawns the player where the area entrance object is and sets the camera to thmen.

    private void Start()
    {
        if(transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();

            UIFade.Instance.FadeToClear();
        }
    }

}
