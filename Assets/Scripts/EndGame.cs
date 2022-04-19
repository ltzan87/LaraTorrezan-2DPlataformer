using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public string compareTag = "Player";
    public GameObject uiEndGame;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.CompareTag(compareTag))
        {
            CallEndGame();
        }
    }

    public void CallEndGame() {
        uiEndGame.SetActive(true);
    }
}