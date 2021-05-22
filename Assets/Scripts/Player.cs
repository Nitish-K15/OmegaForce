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
    Transform GroundCheck,GroundCheckL,GroundCheckR,BulletSpawnPosL,BulletSpawnPosR;
    public GameObject bulletref;
    private bool isShooting, FacingLeft,isInvincible,isTakingDamage;
    public bool DamageSideRight;
    public float PlayerSpeed = 1.6f, PlayerJump = 4;
    public int currentHealth;
    public int maxHealth = 28;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
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
        }
        if(isTakingDamage)
        {
            animator.Play("Hit");
            Invoke("StopDamageAnimation", 0.5f);
            return;
        }
        if(Input.GetKey("f") && isGrounded)
        {
            if (rb2D.velocity.x == 0)
                animator.Play("Shoot");
            else
                animator.Play("Run_Shoot");
               PlayerShoot();
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(PlayerSpeed, rb2D.velocity.y);
            if (isGrounded && !isShooting)
                animator.Play("Run");
            spriteRenderer.flipX = false;
            FacingLeft = false;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-PlayerSpeed, rb2D.velocity.y);
            if (isGrounded && !isShooting)
                animator.Play("Run");
            spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        { 
            if (isGrounded && !isShooting)
                animator.Play("Idle");
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
        if (Input.GetKey("space") && isGrounded == true)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, PlayerJump);
                animator.Play("Jump");
        }
        if (Input.GetKey("f") && isGrounded == false)
        {
            animator.Play("Jump_Shoot");
            PlayerShoot();
        }
    }

    private void PlayerShoot()
    {
        if (isShooting)
            return;
        isShooting = true;
        GameObject bullet = Instantiate(bulletref);
        bullet.GetComponent<Bullet>().StartShoot(FacingLeft);
        if (FacingLeft)
            bullet.transform.position = BulletSpawnPosL.position;
        else
            bullet.transform.position = BulletSpawnPosR.position;
        Invoke("ResetShoot", 0.3f);
    }
    private void ResetShoot()
    {
        isShooting = false;
    }

    public void HitSide(bool rightSide)
    {
        DamageSideRight = rightSide;
    }

    public void Invincibility(bool Invincible)
    {
        isInvincible = Invincible;
    }

    public void TakingDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartDamageAnimation();
        }
    }

    private void StartDamageAnimation()
    {
        if(!isTakingDamage)
        {
            isInvincible = true;
            isTakingDamage = true;
            float hitForceX = 1.5f;
            float hitForceY = 1.5f;
            if(DamageSideRight)
            {
                rb2D.velocity = Vector2.zero;
                rb2D.AddForce(new Vector2(-hitForceX, hitForceY), ForceMode2D.Impulse);
            }
            else
            {
                rb2D.velocity = Vector2.zero;
                rb2D.AddForce(new Vector2(hitForceX, hitForceY), ForceMode2D.Impulse);
            }
        }
    }

    private void StopDamageAnimation()
    {
        isTakingDamage = false;
        isInvincible = false;
        //animator.Play("Hit",0,0f);
    }
}
