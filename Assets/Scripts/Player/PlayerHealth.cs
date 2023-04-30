using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth =5;
    [SerializeField] private float knockBackThrustAmount = 10;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;

    private Knockback knockback;
    private Flash flash;
    private bool canTakeDamage = true;
    private Slider healthSlider;

    const string Health_Slider_Text = "Health Slider";
    const string TOWN_TEXT = "Town";
    readonly int DEATH_HASH = Animator.StringToHash("Death");


    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();

    }

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        
        if (enemy)
        {
            TakeDamage(1, other.transform);

            
        }
    }


    public void TakeDamage(int damage, Transform hitTransform)
    {

        if(!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();


        knockback.GetKnockBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());

        canTakeDamage = false;
        currentHealth -= damage;
        
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    public void HealPlayer()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
        
    }

    //note this coroutine gives us some time where we will be immune with the cantakedamage toggle
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage= true;
    }


    private void UpdateHealthSlider()
    {
        if(healthSlider == null)
        {
            healthSlider = GameObject.Find(Health_Slider_Text).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void CheckIfPlayerDeath()
    {
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Stamina.Instance.ReplenishStaminaOnDeath();
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }


}
