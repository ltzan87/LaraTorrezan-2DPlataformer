using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrIggerTransition : MonoBehaviour
{
    public AudioMixerSnapshot snapshot01;
    public AudioMixerSnapshot snapshot02;
    public string compareTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.CompareTag(compareTag))
        {
            snapshot02.TransitionTo(.1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.transform.CompareTag(compareTag))
        {
            snapshot01.TransitionTo(.1f);
        }
    }
}