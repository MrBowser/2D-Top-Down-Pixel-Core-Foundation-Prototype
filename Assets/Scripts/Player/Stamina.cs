using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite fullStaimaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaimaRefresh = 4;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;


    const string Stamina_Container_Text = "Stamina Container";


    protected override void Awake()
    {
        base.Awake();
        maxStamina= startingStamina;
        CurrentStamina= startingStamina;

    }

    private void Start()
    {
        staminaContainer = GameObject.Find(Stamina_Container_Text).transform;
    }


    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
        StopAllCoroutines();
        StartCoroutine(RefreshStaminaRoutine());
    }

    public void RefreshStamina()
    {
        if(CurrentStamina < maxStamina && !PlayerHealth.Instance.isDead)
        {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }

    public void ReplenishStaminaOnDeath()
    {
        CurrentStamina = startingStamina;
        UpdateStaminaImages();
    }

    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            Transform child = staminaContainer.GetChild(i);
            Image image= child?.GetComponent<Image>();
            //note -1 since index number for child starts at 0
            if(i <= CurrentStamina -1)
            {
                image.sprite = fullStaimaImage;
            }
            else
            {
                image.sprite = emptyStaminaImage;
            }

        }

    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenStaimaRefresh);
            RefreshStamina();
        }


    }




}
