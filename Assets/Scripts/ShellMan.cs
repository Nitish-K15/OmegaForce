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
    private bool Invincible;
    public int health = 3;
    [SerializeField] AudioClip Dink, Shot, Exploding;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void Spawn()
    {
        StartCoroutine(Attack());
    }
    private void Start()
    {
        explosionRef = Resources.Load("Explosion");
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
    }
    private void Shoot()
    {
        SoundManager.Instance.Play(Shot);
        for(int i = 0;i<3;i++)
        {
            bullet[i] = Instantiate(bulletref);
            bullet[i].GetComponent<EnemyBullet>().Shoot(-3f, 0);
            bullet[i].transform.position = BulletSpawn[i].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !Invincible)
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
        else if (collision.CompareTag("Bullet") && Invincible)
        {
            SoundManager.Instance.Play(Dink);
            Destroy(collision.gameObject);
        }
    }

    private void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void KillSelf()
    {
        GameManager.Instance.AddScorePoints(60);
        SoundManager.Instance.Play(Exploding);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Destroy(gameObject);
    }
    IEnumerator Attack()
    {
        while(true)
        { 
            yield return new WaitForSeconds(1f);
            sr.sprite = Shooting;
            Invincible = false;
            Shoot();
            yield return new WaitForSeconds(0.5f);
            sr.sprite = Resting;
            Invincible = true;
        }
    }
}
