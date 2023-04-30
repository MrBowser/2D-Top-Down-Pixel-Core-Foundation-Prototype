using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;

    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPosition;


    private void Start()
    {
        startPosition= transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistaince();

    }

    private void MoveProjectile()
    {
        //note since we the bow already is facing the correct direction and rotation we can just use the vector right which is the x(?) axis
        //and should work 
        transform.Translate(Vector3.right* moveSpeed *Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructable indestructable = other.gameObject.GetComponent<Indestructable>();

        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        //below is if it collides with an object with any of the following it triggers the vfx and is destroyed

        if(!other.isTrigger && (indestructable || enemyHealth || player))
        {
            if((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                //note the question mark c# syntax that checks if it is a player
                player?.TakeDamage(1, transform);

                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);

                Destroy(gameObject);
            }
            else if (!other.isTrigger && indestructable)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);

                Destroy(gameObject);
            }

            
            
        }
       

        
    }

    private void DetectFireDistaince()
    {
        if(Vector3.Distance(transform.position,startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }


    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;


    }


    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
