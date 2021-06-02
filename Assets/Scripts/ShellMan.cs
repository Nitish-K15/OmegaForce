using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMan : MonoBehaviour
{
    public Sprite Shooting, Resting;
    public Transform[] BulletSpawn = new Transform[3];
    public GameObject bulletref;
    private UnityEngine.Object explosionRef;
    public Material matWhite;
    private Material matDefault;
    private GameObject[] bullet = new GameObject[3];
    private SpriteRenderer sr;
    public int health = 3;
    private void Start()
    {
        explosionRef = Resources.Load("Explosion");
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        StartCoroutine(Attack());
    }
    private void Shoot()
    {
        for(int i = 0;i<3;i++)
        {
            bullet[i] = Instantiate(bulletref);
            bullet[i].GetComponent<EnemyBullet>().Shoot(3f, 0);
            bullet[i].transform.position = BulletSpawn[i].position;
        }
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
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        sr.sprite = Shooting;
        Shoot();
        yield return new WaitForSeconds(0.5f);
        sr.sprite = Resting;
        StartCoroutine(Attack());
    }
}
