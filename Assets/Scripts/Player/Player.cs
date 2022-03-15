using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Seep Setup")]
    public Rigidbody2D myRigidbody;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2f;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

    private float _currentSpeed;


    private void Update() {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment() {
        if(Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;
        

        if(Input.GetKey(KeyCode.A))
            {
                myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
            }

            if(myRigidbody.velocity.x > 0)
            {
                myRigidbody.velocity += friction;
            }
            else if(myRigidbody.velocity.x > 0)
            {
                myRigidbody.velocity -= friction;
            }
    }

    private void HandleJump() {
        if(Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = Vector2.up * forceJump;
                myRigidbody.transform.localScale = Vector2.one;

                DOTween.Kill(myRigidbody.transform);

                HandleScaleJump();
            }
    }

    private void HandleScaleJump() {
        myRigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleY(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
