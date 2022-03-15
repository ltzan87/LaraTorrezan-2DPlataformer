using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float forceJump = 2f;

    private void Update() {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment() {
        if(Input.GetKey(KeyCode.A))
            {
                //rb2D.MovePosition(rb2D.position - velocity * Time.deltaTime);
                rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                //rb2D.MovePosition(rb2D.position + velocity * Time.deltaTime);
                rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            }

            if(rb2D.velocity.x > 0)
            {
                rb2D.velocity += friction;
            }
            else if(rb2D.velocity.x > 0)
            {
                rb2D.velocity -= friction;
            }
    }

    private void HandleJump() {
        if(Input.GetKeyDown(KeyCode.Space))
            {
                rb2D.velocity = Vector2.up * forceJump;
            }
    }
}
