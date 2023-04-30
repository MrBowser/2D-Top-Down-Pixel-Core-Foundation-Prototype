using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;
    private bool isGrowing = true;

    [SerializeField] private float laserGrowTime = 2;


    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        capsuleCollider2D= GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    //below is called in staff and gets the strech thing going
    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());

    }

    //note unlike the bow or sword with the staff we are taking the laser object and extending it out the distance that is the set range
    //this means changing the size and collider at the same time as shown in the while loop
    private IEnumerator IncreaseLaserLengthRoutine()
    {

        float timePassed = 0;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed+= Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            //sprite

            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT),1f);

            //collider

            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT))/2, capsuleCollider2D.offset.y);

            yield return null;
        }
        //nabs the sprite fad script and calls the fade out and destroy routine
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());


    }

    //prevents laser from going through things like foreground but not going through things like the top of the tree(the istrigger part
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Indestructable>() && !other.isTrigger)
        {
            isGrowing = false;
        }
    }

    //note this is pretty much the same as the mouse follow script in inventory
    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }

}
