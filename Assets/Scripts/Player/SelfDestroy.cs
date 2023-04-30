using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    //note: this script is currently on the deathvfx objects which are then attached to descrutables like the slime or the bush
    //this then instantiatiates this the particle effect
    private ParticleSystem ps;

    private void Awake()
    {
        ps= GetComponent<ParticleSystem>();
    }

    private void Update()
    {

        //the way this works is that is there a particle system? and are there no more particles alive(lifespawn is in ps settings)... if both are true destroy the vfx object
        //this is different from how the swords anim destruction is triggered since it doesn't have an a ps so it skips over this
        if(ps && !ps.IsAlive())
        {
            DestroySelfAnimEvent();
        }
    }

    //this is triggered in the animator at the end of the animation for the sword animation swoosh but is checked above for particle system object destruction
    public void DestroySelfAnimEvent()
    {
        Destroy(gameObject);
    }
}
