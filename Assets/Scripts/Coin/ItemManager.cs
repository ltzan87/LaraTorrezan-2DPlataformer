using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instace;

    public int coins;

    private void Awake() {
        if(Instace == null)
            Instace = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        Reset();
    }

    private void Reset() {
        coins = 0;
    }

    public void AddCoins(int amount = 1) {
        coins = amount;
    }
}
