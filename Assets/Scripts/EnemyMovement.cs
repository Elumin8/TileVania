using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] float moveSpeed = 1f;


    void Update()
    {
        rb2D.velocity = new Vector2(moveSpeed, 0);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        FlipEnemyFacing();
    }
    void FlipEnemyFacing()
    {
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(-(Mathf.Sign(rb2D.velocity.x)), 1f);
    }
}
