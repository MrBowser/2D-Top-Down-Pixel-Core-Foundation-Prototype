using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovileWhileAttacking = false;
   
     private enum State
    {
        Roaming,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private bool canAttack = true;


    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
       
    }

    private void Start()
    {
       roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl(); 
    }

    private void MovementStateControl()
    {
        switch(state)
        {
            default:
            case State.Roaming:              
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
            
                
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        //if player is within the attackrange then state is switched to attacking
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        //this essentially means that after 2 seconds have passed get a new direction to go to and reset timeRoaming back to zero
        if(timeRoaming > roamChangeDirFloat)
        {
            roamPosition= GetRoamingPosition();
        }

    }

    private void Attacking()
    {
        //if player is within the attackrange then state is switched to attacking
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (canAttack)
        {
            canAttack = false;
            //note the questionmark syntax is a proposed optimization to lever IEnemy Interface and State contorl syntax, the initial solution
            //had a second if clause along with canAttack, attackRange !=0, this means that if an enemy is melee it can't attack, whereas now if attack is missing nothing
            //or if the ienemy interface is missing it does not attack so it no longer limits by range
            (enemyType as IEnemy)?.Attack();

            if(stopMovileWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());

        }
        
        
        
    }

    private IEnumerator AttackCooldownRoutine ()
    {

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }




    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;

        //randomly creats a new place for the slime to go
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }









    //this and getroamingposition are causing the slime to move in a random direction every 2 seconds, below is legacy simple roaming without other states

    /*
    private IEnumerator RoamingRoutine()
    {
        while(state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            //the roamposition in moveto functions as importing the target destination for the moveposition function in enemypathfinding
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }
    */


}
