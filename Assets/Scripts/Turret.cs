using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform BulletSpawn1, BulletSpawn2;
    private Animator anim;
    public int health = 5;
    public Material matWhite;
    private Material matDefault;
    private GameObject[] bullet = new GameObject[2];
    public GameObject bulletref;
    private UnityEngine.Object explosionRef;
    private SpriteRenderer sr;
    [SerializeField] AudioClip Shooting, Exploding;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    { 
        anim = GetComponent<Animator>();
        explosionRef = Resources.Load("Explosion");
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }
   
    private void Shoot()
    {
        SoundManager.Instance.Play(Shooting);
        bullet[0] = Instantiate(bulletref);
        bullet[0].GetComponent<EnemyBullet>().Shoot(-3f, 0);
        bullet[0].transform.position = BulletSpawn1.position;
        bullet[1] = Instantiate(bulletref);
        bullet[1].GetComponent<EnemyBullet>().Shoot(3f, 0);
        bullet[1].transform.position = BulletSpawn2.position;
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
        GameManager.Instance.AddScorePoints(50);
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
            Shoot();
            yield return new WaitForSeconds(1f);
            anim.Play("PoppingIn");
            yield return new WaitForSeconds(1f);
            anim.Play("PoppingOut");
        }
    }
}
