using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public Transform playerSideReference;
    public KeyCode keyCode = KeyCode.S;
    public AudioRandomPlayAudioClips randomShoot;

    private Coroutine _currentCoroutine;


    private void Awake() {
        playerSideReference = GameObject.FindObjectOfType<Player>().transform;
    }

    private void Update() {
        if(Input.GetKeyDown(keyCode))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(keyCode))
        {
            if (_currentCoroutine != null) 
                StopCoroutine(_currentCoroutine);
        }
    }

    IEnumerator StartShoot() {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    private void Shoot() {
        if(randomShoot != null) randomShoot.PlayRandom();

        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }
}