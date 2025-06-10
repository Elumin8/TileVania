using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    Vector2 moveInput;
    [SerializeField] float xMovementSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float ladderMovementSpeed;
    [SerializeField] Animator animator;
    [SerializeField] CapsuleCollider2D collider2D;
    float startingGravityScale;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    bool isAlive = true;
    void Start()
    {
        startingGravityScale = rb2D.gravityScale;
    }

    void Update()
    {
        SetAlive();
        Die();
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
        if(!isAlive){ return; }
        if (value.isPressed
         && boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void OnMove(InputValue inputValue)
    {   
        if(!isAlive){ return; }
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
        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")))
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
    void Die()
    {
        if (collider2D.IsTouchingLayers(LayerMask.GetMask("Enemies")))
            {
                DeathAnim();
            }
        if (collider2D.IsTouchingLayers(LayerMask.GetMask("Spikes")))
        {
            DeathAnim();
        }
        
       
    }
    void DeathAnim()
    {
        isAlive = false;
        animator.SetTrigger("Dying");
        rb2D.velocity = deathKick;

        StartCoroutine(DeathRestart());
        IEnumerator DeathRestart()
        {
            yield return new WaitForSecondsRealtime(1);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    void SetAlive()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
        }
    }
    void OnFire(InputValue value)
    {
        if(!isAlive){ return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }

}
