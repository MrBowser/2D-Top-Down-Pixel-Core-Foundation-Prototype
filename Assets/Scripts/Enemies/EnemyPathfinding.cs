using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        if(knockback.GettingKnockedBack ==true) { return; }

        //takes the existing position and then adds the distance it goes to via the below equation
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        //below takes the random move dir location and if - means it is going to the left and flips the sprite
        if(moveDir.x <0)
        {
            spriteRenderer.flipX = true;
        }
        else if(moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }

}
