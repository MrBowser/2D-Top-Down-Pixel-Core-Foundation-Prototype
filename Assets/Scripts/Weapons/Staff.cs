using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnimator;

    readonly int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator= GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }
    public void Attack()
    {
        
        //this starts the staff attack animation, at the end of the animation the spawn staff projectile animation triggers
        myAnimator.SetTrigger(AttackHash);
        
        
    }
    //note we are setting up an animation event that is triggered with an attack and starts mid animation
    public void SpawnStaffProjectileAnimation()
    {
        //note since we are modifying laser direction in magic laser class we can use quaternion identity here instead of getting the position
        GameObject newLaser = Instantiate(magicLaser,magicLaserSpawnPoint.transform.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        //angle makes it so that the weapon will follow where the mouse is so the swing is more dynamic, take it out and it is more flat
        //this is useful for the topdown bullet effect
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            
        }
        else if (mousePos.x > playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            
        }

    }
}
