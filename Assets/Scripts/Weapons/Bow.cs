using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private Transform arrowSpawnPoint;

    private Animator myAnimator;

    //the readonly is a performance enhancement since finding setTrigger everyTime is resource intensive
    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnimator= GetComponent<Animator>();

    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);

        //myAnimator.SetTrigger("Fire");

        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
         
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
