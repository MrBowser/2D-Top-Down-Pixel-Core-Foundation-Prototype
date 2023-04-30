using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;

    const string Coin_Amount_Text = "Gold Amount Text";
    private int currentGold = 0;

    public void UpdateCurrentGold()
    {
        currentGold++;

        if(goldText ==null)
        {
            goldText = GameObject.Find(Coin_Amount_Text).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");

    }
}
