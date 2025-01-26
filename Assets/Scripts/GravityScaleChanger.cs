using System;
using System.Collections;
using UnityEngine;

public class GravityScaleChanger : MonoBehaviour
{
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private GameObject transfer;
    private SpriteRenderer transferSr;
    private Animator transferAnimator;

    private void Awake()
    {
        transferSr = transfer.GetComponent<SpriteRenderer>();
        transferAnimator = transfer.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = gravityScale;
            transferSr.enabled = true;
            StartCoroutine(TimerCoroutine());
            if (gravityScale == 0f) {
                Player player = other.GetComponent<Player>();
                player.Die();
            } 
        }
    }

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(0.667f);
        transferSr.enabled = false;
    }
}
