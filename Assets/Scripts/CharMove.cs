using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public Animator animator;
    private int playerSpeed = 8;
    private int playerJumpPower = 1700;
    private float playerHeight = 0.0f;
    private float sprintMult = 3;
    private float totalSpeed;
    private float moveX;
    private float sprintX;
    private bool facingRight = true;
    public bool isGrounded;

    void Update()
    {
        PlayerMove();
        PlayerRaycast();
        animator.SetFloat("Speed", Mathf.Abs(moveX));
    }
    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        sprintX = Input.GetAxis("Sprint");
        if (Input.GetButtonDown("Jump") && isGrounded) {
            Jump();
            animator.SetBool("IsJumping", true);
        }
        //ANIMATIONS

        //PLAYER DIRECTION
        if (moveX > 0.0f && !facingRight)
        {
            FlipPlayer();
        }
        else if (moveX < 0.0f && facingRight)
        {
            FlipPlayer();
        }
        //PHYSICS
        if (Mathf.Abs(moveX) > 0.0f) {
            totalSpeed = playerSpeed;
            if (sprintX > 0.0f)
            {
                totalSpeed = playerSpeed * sprintMult;
                animator.SetBool("IsSprinting", true);
            }
            else {
                animator.SetBool("IsSprinting", false);
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * totalSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D col) {
    }

    void PlayerRaycast(){
        playerHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if(   (hit != null)
           && (hit.collider != null)
           && (hit.distance < (playerHeight/1.8))  ){

          if(hit.collider.tag == "enemy"){
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
          }
          if(hit.collider.tag != "enemy"){
            isGrounded = true;
            animator.SetBool("IsJumping", false);
          }
        }
    }
}
