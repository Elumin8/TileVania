using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] float bulletMovementSpeed;
    PlayerMovementScript player;
    float xSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerMovementScript>();
        xSpeed = player.transform.localScale.x * bulletMovementSpeed;
    }
    void Update()
    {
        rb2D.velocity = new Vector2(xSpeed, 0f);

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
