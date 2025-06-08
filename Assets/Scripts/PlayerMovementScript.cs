using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector2 moveInput;
    [SerializeField] float xMovementSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float ladderMovementSpeed;
    Animator animator;
    CapsuleCollider2D collider2D;
    float startingGravityScale;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();
        startingGravityScale = rb2D.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * xMovementSpeed, rb2D.velocity.y);
        rb2D.velocity = playerVelocity;
        if (Mathf.Abs(rb2D.velocity.x) > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void FlipSprite()

    {
        bool hasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {

            transform.localScale = new Vector2(Mathf.Sign(rb2D.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!collider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            rb2D.gravityScale = startingGravityScale;
            animator.SetBool("isClimbing", false);
            return;
        }
            Vector2 movementUp = new Vector2(rb2D.velocity.x, moveInput.y * ladderMovementSpeed);
            rb2D.velocity = movementUp;
            rb2D.gravityScale = 0f;
        if (Mathf.Abs(rb2D.velocity.y) > 0)
        {
            animator.SetBool("isClimbing", true);
        }
        else
        {
            animator.SetBool("isClimbing", false);
        }
    }

}
