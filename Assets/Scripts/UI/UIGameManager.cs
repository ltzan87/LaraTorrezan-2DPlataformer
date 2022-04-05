using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class UIGameManager : Singleton<ItemManager>
{
    public TextMeshProUGUI uiTextCoins;


    public static void UpdateTextCoins(string s) {
        Instance.uiTextCoins.text = s;
    }
}
