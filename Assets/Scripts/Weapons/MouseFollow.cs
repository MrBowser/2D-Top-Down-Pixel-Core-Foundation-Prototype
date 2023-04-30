using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{

    private void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        // the below is used for the bow 360 degree rotation essentiation wherever the mouse is the object will face note this is not on the bow but the activeweapon
        //in case we want the same effect elsewhere

        //won't affect sword or staff since animtor is sorta ovveriding it
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        //note with the above we are actually facing the mouse with the bow along the red axis, since in the animator the idle aniomation is out ~.5 on
        // the x axis it rotates around the active weapon pivout point at that distance getting the nice circle effect
        transform.right = -direction;

    }
}
