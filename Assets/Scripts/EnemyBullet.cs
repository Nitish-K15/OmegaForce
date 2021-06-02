using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    private Rigidbody2D rb2d;
    private void Start()
    {
        Invoke("DestroySelf", 2);
    }
    public void Shoot(float dirX,float dirY)
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(dirX, dirY);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakingDamage(damage);
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
