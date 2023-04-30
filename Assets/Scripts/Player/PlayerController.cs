using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;

    

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Knockback knockback;
    private bool isDashing = false;
    private float startingMoveSpeed;

    

    private bool facingLeft = false;

    public bool FacingLeft
    {
        get { return facingLeft; }
        
    }


    protected override void Awake()
    {
        base.Awake();
        
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator= GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        
    }

    private void Start()
    {
        //below uses new input system... if the dash button is clicked run the the dash method,, lambda function?
        playerControls.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;

        ActiveInventory.Instance.EquipStartingWeapon();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        AdjustPlayerFacingDirection();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void PlayerInput()
    {
        //below is how to get the values from the new unity input system, it is a 1 or 0 or -1
        movement = playerControls.Movement.Move.ReadValue<Vector2>();


        //this refers to the paramators within the Animator for player, used currently for transitions
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

    }

    private void Move()
    {
        //way to think about this is that it has the x value + movement which is either 1-,0,1 multiplied by speed, this will shift the player
        //same would apply for why since movent is is a vector 2 so the value getting added to the rb vector 2 is teh vector 2(movement.x,movement.y)

        //the if statement essentially allows the player to get knocked back, if this was gone the move would overide the function when triggered in 
        //player health
        if(knockback.GettingKnockedBack || PlayerHealth.Instance.isDead)
        { return; }

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }


    //this has the sprite flip direction dependant on the mouse location
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX= true;
            //outside of this class need to use the capitalized FacingLeft since Getter
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX= false;
            facingLeft = false;
        }

    }

    private void Dash()
    {
        if(isDashing || Stamina.Instance.CurrentStamina <= 0 ) { return; }

        //multiplies player speed by dashspeed and triggers little trail to signify speed change
        Stamina.Instance.UseStamina();
        isDashing= true;
        moveSpeed *= dashSpeed;
        myTrailRenderer.emitting= true;
        StartCoroutine(EndDashRoutine());
    }

    private IEnumerator EndDashRoutine()
    {
        //not can have multiple yield retruns in coroutines for multi step which here is used for speed increase time as well as cooldown
        float dashTime = .15f;
        float dashCD = .4f;
        yield return new WaitForSeconds(dashTime);
        //note: we create starting movespeed varsince we don't want to divide movespeed by dashspeed, dividing by 2 floats can cause errors
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting= false;
        yield return new WaitForSeconds(dashCD);
        isDashing= false;
    }

}

/* mitch bowser notes regarding this course.... there is a nifty effect that allows the player to go behind objects with depth
 * below are the steps
 * Found in UnityRPG2d
 * Video Titled Player Physics
 * 
 * Note: this requires urp version of unity
 * step 1 click render 2d and for transparency sort click custom axis y =1, this sort things up and down
 * step 2 for sprites go to sprite renderer and on sprite sort point click pivot (this is the blue donut dot when you edit a sprite) 
 *
 */