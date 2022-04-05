using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public HealthBase healthBase;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2f;

    [Header("Animation Setup")]
   /* public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;*/
    public SOFloat soJumpScaleX;
    public SOFloat soJumpScaleY;
    public SOFloat soAnimationDuration;


    public Ease ease = Ease.OutBack;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float playerSwipeDuration = .1f;

    private float _currentSpeed;


    private void Awake() {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill() {
        healthBase.OnKill -= OnPlayerKill;

        animator.SetTrigger(triggerDeath);
    }

    private void Update() {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment() {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;

            animator.speed = 1.5f;
        }
        else
        {
            _currentSpeed = speed;

            animator.speed = 1;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
                if(myRigidbody.transform.localScale.x != -1)
                {
                    myRigidbody.transform.DOScaleX(-1, playerSwipeDuration);
                }

                animator.SetBool(boolRun, true);
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
                if(myRigidbody.transform.localScale.x != 1)
                {
                    myRigidbody.transform.DOScaleX(1, playerSwipeDuration);
                }

                animator.SetBool(boolRun, true);
            }
            else
            {
                animator.SetBool(boolRun, false);
            }

            if(myRigidbody.velocity.x > 0)
            {
                myRigidbody.velocity += friction;
            }
            else if(myRigidbody.velocity.x < 0)
            {
                myRigidbody.velocity -= friction;
            }

        if(Mathf.Abs(myRigidbody.velocity.x) < Mathf.Abs(friction.x))
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
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
        myRigidbody.transform.DOScaleY(soJumpScaleY.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(soJumpScaleX.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}