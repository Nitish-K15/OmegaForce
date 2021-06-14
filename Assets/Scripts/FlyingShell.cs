using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShell : MonoBehaviour
{
    public Sprite Shooting,Resting;
    public Transform[] BulletSpawn = new Transform[3];
    private Rigidbody2D rb2d;
    private UnityEngine.Object explosionRef;
    public Material matWhite;
    private Material matDefault;
    public bool FacingLeft;
    public GameObject bulletref;
    private GameObject[] bullet = new GameObject[3];
    private SpriteRenderer sr;
    public int health = 5;
    [SerializeField] AudioClip Shot,Exploding;
    void Start()
    {
        explosionRef = Resources.Load("Explosion");
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        StartCoroutine(Attack());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
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
    }

    private void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void KillSelf()
    {
        SoundManager.Instance.Play(Exploding);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
    }
    private void Shoot()
    {
        SoundManager.Instance.Play(Shot);
            bullet[0] = Instantiate(bulletref);
            bullet[0].GetComponent<EnemyBullet>().Shoot(-3f,-3f);
            bullet[0].transform.position = BulletSpawn[0].position;
            bullet[1] = Instantiate(bulletref);
            bullet[1].GetComponent<EnemyBullet>().Shoot(0, -3f);
            bullet[1].transform.position = BulletSpawn[1].position;
            bullet[2] = Instantiate(bulletref);
            bullet[2].GetComponent<EnemyBullet>().Shoot(3f, -3f);
            bullet[2].transform.position = BulletSpawn[2].position;
    }
 
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        rb2d.velocity = Vector2.zero;
        sr.sprite = Shooting;
        Shoot();
        yield return new WaitForSeconds(0.5f);
        sr.sprite = Resting;
        if(FacingLeft)
         rb2d.velocity = new Vector2(-0.5f, rb2d.velocity.y);
        else
         rb2d.velocity = new Vector2(0.5f, rb2d.velocity.y);
        StartCoroutine(Attack());
    }


}
