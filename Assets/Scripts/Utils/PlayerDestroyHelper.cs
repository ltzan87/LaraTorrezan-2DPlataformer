using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyHelper : MonoBehaviour
{
    //public Player player;

    Player player;

    public void KillPlayer() {
        //player.DestroyMe();
        player = GameObject.Find("Player").GetComponent<Player>();
        player.DestroyMe();
    }
}