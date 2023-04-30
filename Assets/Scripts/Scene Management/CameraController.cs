using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    //this is used to make the camera follow the player on entering a new scene (area entrance script)

    private void Start()
    {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow()
    {
        cinemachineVirtualCamera= FindObjectOfType<CinemachineVirtualCamera>();

        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform ;
    }
}
