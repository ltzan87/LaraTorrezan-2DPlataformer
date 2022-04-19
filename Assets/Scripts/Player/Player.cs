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

    //public Animator animator;

    [Header("Jump Collision Check")]
    public Collider2D colliderPlayer;
    public float distToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;

    private float _currentSpeed;
    private Animator _currentPlayer;

    private void Awake() {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if(colliderPlayer != null)
        {
            distToGround = colliderPlayer.bounds.extents.y;
        }
    }

    private bool IsGrounded() {
        bool grounded = Physics2D.Raycast(transform.position, -Vector2.up, spaceToGround);
        Color color = grounded ? Color.red : Color.magenta;
        Debug.DrawRay(transform.position, -Vector2.up, color);
        return grounded;
        //Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        //return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    private void OnPlayerKill() {
        healthBase.OnKill -= OnPlayerKill;

        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update() {
        IsGrounded();
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment() {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = soPlayerSetup.speedRun;

            _currentPlayer.speed = 1.5f;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;

            _currentPlayer.speed = 1;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
            {
                myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
                if(myRigidbody.transform.localScale.x != -1)
                {
                    myRigidbody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
                }

                _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
                if(myRigidbody.transform.localScale.x != 1)
                {
                    myRigidbody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
                }

                _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            }
            else
            {
                _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
            }

            if(myRigidbody.velocity.x > 0)
            {
                myRigidbody.velocity -= soPlayerSetup.friction;
            }
            else if(myRigidbody.velocity.x < 0)
            {
                myRigidbody.velocity += soPlayerSetup.friction;
            }

        if(Mathf.Abs(myRigidbody.velocity.x) < Mathf.Abs(soPlayerSetup.friction.x))
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        }
    }

    private void HandleJump() {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidbody.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidbody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidbody.transform);

            HandleScaleJump();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX() {
        if (jumpVFX != null) jumpVFX.Play();
        Vfx_Manager.Instance.PlayVFXByType(Vfx_Manager.VFXType.JUMP, transform.position);
    }

    private void HandleScaleJump() {
        myRigidbody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidbody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}