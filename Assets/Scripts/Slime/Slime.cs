using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public LayerMask playerLayer;
    public float raycastDistance = 2f;
    [SerializeField] private SlimeAnimatonController animator;

    private void Awake()
    {
        animator = GetComponent<SlimeAnimatonController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
            Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, playerLayer);

            AudioManager.Instance.PlaySFX("SlimeBounce");

            if (hit.collider != null)
            {
                if (rigidbody2D.velocity.y < 0)
                {
                    animator.SetCollisionUp();
                    playerMovement.ApplyKnockback(new Vector2(0, 10), 0f);
                }
            } else {
                animator.SetCollisionSide();
                if (spriteRenderer.flipX == true)
                    playerMovement.ApplyKnockback(new Vector2(10, 10), 2f);
                else {
                    playerMovement.ApplyKnockback(new Vector2(-10, 10), 2f);
                }
            }
        }
    }
}
