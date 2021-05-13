using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    bool isGrounded;
    [SerializeField]
    Transform GroundCheck,GroundCheckL,GroundCheckR;
    public float PlayerSpeed = 1.6f, PlayerJump = 4;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"))&&
            Physics2D.Linecast(transform.position, GroundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))&&
            Physics2D.Linecast(transform.position, GroundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            animator.Play("Jump");
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(PlayerSpeed, rb2D.velocity.y);
            if (isGrounded)
                animator.Play("Run");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-PlayerSpeed, rb2D.velocity.y);
            if (isGrounded)
                animator.Play("Run");
            spriteRenderer.flipX = true;
        }
        else
        {
            if (isGrounded)
                animator.Play("Idle");
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
        if (Input.GetKey("space") && isGrounded == true)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, PlayerJump);
            animator.Play("Jump");
        }
    }
}
