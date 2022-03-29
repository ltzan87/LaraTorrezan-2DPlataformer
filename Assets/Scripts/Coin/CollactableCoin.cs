using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableCoin : CollactableBase
{
    protected override void OnCollect() {
        base.OnCollect();
        ItemManager.Instace.AddCoins();
    }
}
