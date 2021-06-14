using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepe : MonoBehaviour
{
    private UnityEngine.Object explosionRef;
    public Material matWhite;
    private Material matDefault;
    private SpriteRenderer sr;
    public int health = 3,damage = 3;
    public AudioClip Exploding;
    private Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        explosionRef = Resources.Load("Explosion");
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            int BulletDamage = collision.GetComponent<Bullet>().Damage;
            health = health - BulletDamage;
            sr.material = matWhite;
            if (health <= 0)
            {
                KillSelf();
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }

        if(collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakingDamage(damage);
        }
    }
    private void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void KillSelf()
    {
        GameManager.Instance.AddScorePoints(50);
        SoundManager.Instance.Play(Exploding);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
    }
    void Update()
    {
        rb2d.velocity = new Vector2(-1, 0);
    }
}
