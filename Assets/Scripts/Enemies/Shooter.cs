using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = .1f;
    [SerializeField] private bool constantWave = false;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool followPlayerBetweenBursts = true;
    [SerializeField] private bool stagger;
    [Tooltip("stagger is rquired for oscillate to work, oscillate switch between starting at start and end angles")]
    [SerializeField] private bool oscillate;

    private bool isShooting = false;

    private void OnValidate()
    {
       //this allows us to create in unity editor rules so for the bellow example the bools would be connected in the component logic
       if(oscillate) { stagger = true; }
       //if (!oscillate) { stagger = false; } we don't need oscillate to do a stagger

        if(projectilesPerBurst <1) { projectilesPerBurst= 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < .1f) { timeBetweenBursts = .1f; }
        if (restTime < .1f) { restTime = .1f; }
        if (startingDistance < .1f) { startingDistance = .1f; }
        if (angleSpread == 0) { projectilesPerBurst = 1; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = .5f; }
    }

    public void Attack()
    {
        if(!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }


    private IEnumerator ShootRoutine()
    {

        isShooting = true;
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        //note out syntax means that it sends out parameter of function as well as return type
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        //note stagger makes the bullets within a burst come out at staggered in equal intervals
        if (stagger)
        {
            timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst;
        }

        for (int i = 0; i < burstCount; i++)
        {

            if (followPlayerBetweenBursts && !oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if(followPlayerBetweenBursts && oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            } 
            else if(followPlayerBetweenBursts && oscillate)
            {
                
                //note if oscillate will alternate between bullets starting at either the end or start angle at the start of a burst
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }


            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);

                //since new bullet spawns slightly awayfrom the ghost we can subtract the bullet transform from shooter object to get the correct direction
                newBullet.transform.right = newBullet.transform.position - transform.position;

                //this allows us to take a method or function modify the parameter and than access it outside the function.... also can return multiple datatype since
                //you can return whatever the parameters are as well
                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if(stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            if(!constantWave) 
            {
                yield return new WaitForSeconds(timeBetweenBursts);
            }
            

  
            
        }



        yield return new WaitForSeconds(restTime);

        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {

        //note gets the angle depends on where the player is
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

        //this gets us a center point for +- when creating the spread allows for equal distributio for burst... target angle is the midpoint
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0)
        {
            //this is for bursts ex. if we have an angle spread of 80 and 7 projectiles we bet 80/6 = anglestep
            angleStep = angleSpread / (projectilesPerBurst - 1);

            // gets us the spread broken into halves
            halfAngleSpread = angleSpread / 2f;

            //this gets us the lower bound
            startAngle = targetAngle - halfAngleSpread;

            //this gets us the top bound
            endAngle = targetAngle + halfAngleSpread;

            //this defines where first bullet comes from
            currentAngle = startAngle;


        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);


        Vector2 pos = new Vector2(x, y);

        return pos;
    }


}
