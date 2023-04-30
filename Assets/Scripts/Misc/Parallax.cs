using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float parallaxOffset = -.15f;

    private Camera cam;
    private Vector2 startPos;
    private Vector2 travel => (Vector2)cam.transform.position - startPos;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        startPos= transform.position;
    }


    private void FixedUpdate()
    {
        //note the Offset and different follow rate gives an layered illusion on the scene
        transform.position = startPos + travel * parallaxOffset;
    }
}
