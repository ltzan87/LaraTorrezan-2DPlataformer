using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public KeyCode keycode = KeyCode.P;
    public AudioSource audioSource;


    public void Play() {
        audioSource.Play();
    }

    private void Update() {
        if(Input.GetKeyDown(keycode))
        {
            Play();
        }
    }
}