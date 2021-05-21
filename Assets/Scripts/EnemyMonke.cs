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
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        animator = GetComponent<Animator>();
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
