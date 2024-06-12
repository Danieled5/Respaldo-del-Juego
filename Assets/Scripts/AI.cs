using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float speed;
    public float distanceBetween;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject player;
    private float distance;
    private Vector2 lastPosition;
    private bool facingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance < distanceBetween)
            {
                rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
                animator.SetBool("IsWalking", true);

                if (direction.x > 0 && !facingRight)
                {
                    Flip();
                }
                else if (direction.x < 0 && facingRight)
                {
                    Flip();
                }
            }
            else
            {
                animator.SetBool("IsWalking", false);
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("HitPlayer");
        }

        if (other.CompareTag("PlayerAttack"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.ResetTrigger("HitPlayer");
        }
    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }
}
