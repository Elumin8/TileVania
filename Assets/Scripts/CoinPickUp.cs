using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] int scoreCoin = 100;

    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(scoreCoin);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }









}
