using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage, Speed;
    public float DestroyTime,Delay;
    void Start()
    {
        Invoke("DestroySelf", DestroyTime);  //Destroy Bullet after set time
    }

    public void StartShoot(bool FacingLeft)
    {
        Rigidbody2D rgbd = GetComponent<Rigidbody2D>();
        if (FacingLeft)                                    //Check to spawn bullets in the correct direction
        {
            rgbd.velocity = new Vector2(-Speed, 0);
        }
        else
        {
            rgbd.velocity = new Vector2(Speed, 0);
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

}
