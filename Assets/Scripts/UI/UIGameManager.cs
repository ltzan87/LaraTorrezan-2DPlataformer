using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class UIGameManager : Singleton<UIGameManager>
{
    public TextMeshProUGUI uiTextCoins;


    public void UpdateTextCoins(string s) {
        Instance.uiTextCoins.text = s;
    }
}