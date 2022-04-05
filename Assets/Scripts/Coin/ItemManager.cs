using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;
    public TextMeshProUGUI uiTextCoins;


    private void Start() {
        Reset();
    }

    private void Reset() {
        coins = 0;
    }

    public void AddCoins(int amount = 1) {
        coins = amount;
        UpdateUI();
    }

    private void UpdateUI() {
        //uiTextCoins.text = coins.ToString();
        UIGameManager.UpdateTextCoins(coins.ToString());

    }
}
