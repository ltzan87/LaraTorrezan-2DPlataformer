using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    public Animator animator;

    private float _currentSpeed;


    private void Awake() {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill() {
        healthBase.OnKill -= OnPlayerKill;

        animator.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update() {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment() {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = soPlayerSetup.speedRun;

            animator.speed = 1.5f;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;

            animator.speed = 1;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
                if(myRigidbody.transform.localScale.x != -1)
                {
                    myRigidbody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
                }

                animator.SetBool(soPlayerSetup.boolRun, true);
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
                if(myRigidbody.transform.localScale.x != 1)
                {
                    myRigidbody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
                }

                animator.SetBool(soPlayerSetup.boolRun, true);
            }
            else
            {
                animator.SetBool(soPlayerSetup.boolRun, false);
            }

            if(myRigidbody.velocity.x > 0)
            {
                myRigidbody.velocity += soPlayerSetup.friction;
            }
            else if(myRigidbody.velocity.x < 0)
            {
                myRigidbody.velocity -= soPlayerSetup.friction;
            }

        if(Mathf.Abs(myRigidbody.velocity.x) < Mathf.Abs(soPlayerSetup.friction.x))
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        }
    }

    private void HandleJump() {
        if(Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = Vector2.up * soPlayerSetup.forceJump;
                myRigidbody.transform.localScale = Vector2.one;

                DOTween.Kill(myRigidbody.transform);

                HandleScaleJump();
            }
    }

    private void HandleScaleJump() {
        myRigidbody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidbody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}