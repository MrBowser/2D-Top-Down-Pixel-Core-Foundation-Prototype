using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();

    }

    private void Start()
    {
        if(myAnimator == null) { return; }
        //this is how you get random starts for the torch animations at the start of the game... plays the looping animation at different times the below code will
        //pull if you have just a single animation... unsure what would happen if you had multiple animation in play
        AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);
        myAnimator.Play(state.fullPathHash, -1, Random.Range(0f,1f));

        
    }
}
