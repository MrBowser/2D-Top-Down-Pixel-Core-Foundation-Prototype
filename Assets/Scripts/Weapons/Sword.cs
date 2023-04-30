using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    
    [SerializeField] private float swordAttackCD = .3f;
    [SerializeField] private WeaponInfo weaponInfo;


    
    private Animator myAnimator;
    private Transform weaponCollider;
    private GameObject slashAnim;

    private void Awake()
    {
         myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {

        //note weapon collider and slashAnimSpawn point are 2 ways of doing the same thing..... weaponCollider is more professional and better
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashAnimationSpawnPoint").transform;
    }


    void Update()
    {
        MouseFollowWithOffset();
       
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }


    public void Attack()
    {
        //when attacked what happens is we start the sword attack animation, turns the collider on so it can hit stuff and then the swoosh slash animation triggers
        //ends with the cooldown attack routione


            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);

            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);

            slashAnim.transform.parent = this.transform.parent;
     
           

    }


   

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
                  
        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        //angle makes it so that the weapon will follow where the mouse is so the swing is more dynamic, take it out and it is more flat
        //this is useful for the topdown bullet effect
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


        if(mousePos.x < playerScreenPoint.x)
        {
           ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,angle); 
           weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
        }
        else if (mousePos.x > playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }



    //below are slashAnimations that are being triggered during the animation via a key event and within the animation windows..... hence why they are public and 
    //no references are found also note I believe the swing up and down is via a loop in the animtor and states 

    public void SwingUpFlipAnim()
    {        
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if(PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {

        if(slashAnim == null) { return; }

        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    public void DoneAttackingAnim()
    {
        weaponCollider.gameObject.SetActive(false);
    }



}
