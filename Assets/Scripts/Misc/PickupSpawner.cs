using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    //note the below is the same as 3 serialized fields of private game objects, this is just short form syntax
    [SerializeField] private GameObject goldcoinPrefab, healthGlobe, staminaGlobe;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 6);

        if(randomNum == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 3 || randomNum == 4)
        {
            int randomAmountOfGold = Random.Range(1, 4);

            for(int i=0; i < randomAmountOfGold; i++)
            {
                Instantiate(goldcoinPrefab, transform.position, Quaternion.identity);
            }

            
        }



        
    }
}
