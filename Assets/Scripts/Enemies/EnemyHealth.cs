using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback= GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth= startingHealth;
    }


    public void TakeDamage(int damage)
    {
        //when damge is taken you lose health, get knocked back, flash white in this case and is checked for death(delayed so not instance 

        currentHealth -= damage;
        knockback.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }


    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropItems();
            Destroy(gameObject);
            
        }
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        //note added the divide by 2 so if it is killed we don't wait the whole time for the flash animation
        yield return new WaitForSeconds(flash.GetRestoreDefaultMatTime()/2);
        DetectDeath();
    }

}
