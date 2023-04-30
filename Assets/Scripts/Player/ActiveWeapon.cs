using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private PlayerControls playerControls;

    private bool attackButtonDown, isAttacking = false;

    private float timeBetweenAttacks;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }


    //note since we have an interface iweapon  ex look at the timeBetweenAttacks variable, each weapons has a set of characteristics that are the same
    //but have different values or can do different things, same with methods, each attack trigger is a seperate action but can still be called 
    // ex. in the attack method with the as IWeapon clause
    
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;

    }

    private void AttackCooldown()
    {
        isAttacking= true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking= false;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }


    private void StartAttacking()
    {
        attackButtonDown = true;


    }

    private void StopAttacking()
    {
        attackButtonDown = false;

    }

    private void Attack()
    {
        //note optimally would combine if statements but like showing one line quick fix syntax
        if(CurrentActiveWeapon == null) { return; }

        if (attackButtonDown && !isAttacking)
        {
             AttackCooldown();

            (CurrentActiveWeapon as IWeapon).Attack();
        }
        
    }

   


}
