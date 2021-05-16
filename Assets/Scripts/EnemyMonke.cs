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
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
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
