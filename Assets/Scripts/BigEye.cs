using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigEye : MonoBehaviour
{
    bool isFacingLeft = true;
    private BoxCollider2D coll;
    private SpriteRenderer sr;
    public Sprite InAir,Standing;
    private Transform player;
    private UnityEngine.Object explosionRef;
    public Material matWhite;
    private Material matDefault;
    public int health = 5,damage;
    private int jumpType;
    private Rigidbody2D rb2d;
    public LayerMask ground;
    [SerializeField] AudioClip Exploding;
    public GameObject Ender;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        coll = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        explosionRef = Resources.Load("Explosion");
        matDefault = sr.material;
        //StartCoroutine(Attack());
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.25f);
            jumpType = Random.Range(0, 2);
            Jumping(jumpType);
        }
    }
    private void Update()
    {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, ground))
            sr.sprite = Standing;
        else
            sr.sprite = InAir;
    }
    private void Jumping(int JumpType)
    {
        if (player.transform.position.x <= transform.position.x)
        {
            isFacingLeft = true;
            sr.flipX = true;
        }
        else
        {
            isFacingLeft = false;
            sr.flipX = false;
        }
        if (JumpType == 1)
        {
            if (isFacingLeft)
                rb2d.velocity = new Vector2(-1f, 3f);
            else
                rb2d.velocity = new Vector2(1f, 3f);
        }
        else
        {
            if (isFacingLeft)
                rb2d.velocity = new Vector2(-0.75f, 4f);
            else
                rb2d.velocity = new Vector2(0.75f, 4f);
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
        if (collision.gameObject.CompareTag("Player"))
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
        GameManager.Instance.AddScorePoints(200);
        SoundManager.Instance.Play(Exploding);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        Ender.SetActive(true);
        Destroy(gameObject);
    }
}
