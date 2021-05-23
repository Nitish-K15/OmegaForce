using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonke : MonoBehaviour
{
    private int health = 5;
    public Material matWhite;
    private Material matDefault;
    private SpriteRenderer sr;
    private UnityEngine.Object explosionRef;
    private Animator animator;
    public float jumpHeight;
    public Transform GroundCheck;
    bool isGrounded,FacingLeft = true;
    private Rigidbody2D rb2d;
    public float jumpForceX = 2f;
    public float jumpForceY = 4f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.5f);
        if (FacingLeft)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(2f, 5f), ForceMode2D.Impulse);
            sr.flipX = true;
            FacingLeft = false;
            yield return new WaitForSeconds(1.5f);
        }
        if (!FacingLeft)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(-2f, 5f), ForceMode2D.Impulse);
            sr.flipX = false;
            FacingLeft = true;
        }
        StartCoroutine(Attack());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            int BulletDamage = collision.GetComponent<Bullet>().Damage;
            health = health-BulletDamage;
            sr.material = matWhite; 
            if(health <= 0)
            {
                KillSelf();
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
    }
   
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakingDamage(2);
        }
    }
    private void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void KillSelf()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
    }
}
