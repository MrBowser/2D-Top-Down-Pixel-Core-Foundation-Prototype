using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    private CinemachineImpulseSource source;

    //note this screen shake is a bit clunky in the sense that is uses the NoiseSettings Object in URP settings
    // then you need to add noisesettings to the cinemachine raw signal impulse control (in screen shake manager
    // then add cinemachine impulse listener in the state driven camera
    //then generate impulse when you take damage

    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen ()
    {
        source.GenerateImpulse();
    }




}
