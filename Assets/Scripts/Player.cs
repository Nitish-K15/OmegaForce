using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    public Sprite Defeat;
    public bool isGrounded;
    [SerializeField]
    Transform GroundCheck,GroundCheckL,GroundCheckR,BulletSpawnPosL,BulletSpawnPosR;
    public GameObject bulletref;
    private bool isShooting, FacingLeft,isInvincible,isTakingDamage;
    public bool DamageSideRight;
    public float PlayerSpeed = 1.6f, PlayerJump = 4;
    public int currentHealth;
    public int maxHealth = 28;
    public Image HealthBar;
    private UnityEngine.Object explosionRef;
    public AudioClip Jump, Shoot, Hit, Explode;

    void Start()
    {
        explosionRef = Resources.Load("Explosion");
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }
    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"))||
            Physics2D.Linecast(transform.position, GroundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))||
            Physics2D.Linecast(transform.position, GroundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            if (Input.GetKey("f"))
                animator.Play("Jump_Shoot");
            else
                animator.Play("Jump");
        }
        if(isTakingDamage)
        {
            animator.Play("Hit");
            Invoke("StopDamageAnimation", 1f);
            return;
        }
        if(Input.GetKey("f"))
        {
               PlayerShoot();
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(PlayerSpeed, rb2D.velocity.y);
            if (isGrounded)
            {
                if (Input.GetKey("f"))
                    animator.Play("Run_Shoot");
                else
                    animator.Play("Run");
            }
            spriteRenderer.flipX = false;
            FacingLeft = false;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-PlayerSpeed, rb2D.velocity.y);
            if (isGrounded)
            {
                if (Input.GetKey("f"))
                    animator.Play("Run_Shoot");
                else
                    animator.Play("Run");
            }
            spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            if (isGrounded)
            {
                if (Input.GetKey("f") && isShooting)
                    animator.Play("Shoot");
                else
                    animator.Play("Idle");
            }
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
        if (Input.GetKey("space") && isGrounded == true)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, PlayerJump);
                animator.Play("Jump");
            SoundManager.Instance.Play(Jump);
        }
        if (Input.GetKey("f") && isGrounded == false)
        {
            animator.Play("Jump_Shoot");
            PlayerShoot();
        }
    }

    private void PlayerShoot()
    {
        float delay;
        if (isShooting)
            return;
        SoundManager.Instance.Play(Shoot);
        isShooting = true;
        GameObject bullet = Instantiate(bulletref);
        bullet.GetComponent<Bullet>().StartShoot(FacingLeft);
        delay = bullet.GetComponent<Bullet>().Delay;
        if (FacingLeft)
            bullet.transform.position = BulletSpawnPosL.position;
        else
            bullet.transform.position = BulletSpawnPosR.position;
        Invoke("ResetShoot", delay);
    }
    private void ResetShoot()
    {
        isShooting = false;
    }

    public void HitSide(bool rightSide)
    {
        DamageSideRight = rightSide;
    }

    public void TakingDamage(int damage)
    {
        SoundManager.Instance.Play(Hit);
        currentHealth -= damage;
        //gameObject.layer = 8;
        HealthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        if (currentHealth <= 0)
        {
            animator.enabled = false;
            spriteRenderer.sprite = Defeat;
            rb2D.velocity = Vector2.zero;
            Invoke("KillSelf", 0.5f);
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

    private void KillSelf()
    {
        SoundManager.Instance.Play(Explode);
        Destroy(gameObject);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        GameManager.Instance.updateLives();
    }
        private void StopDamageAnimation()
    {
        //gameObject.layer = 7;
        isTakingDamage = false;
        isInvincible = false;
    }
    
}
