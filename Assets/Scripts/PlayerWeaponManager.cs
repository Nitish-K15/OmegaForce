using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public GameObject BulletPref1, BulletPref2, BulletPref3;
    public AudioClip Bullet1, Bullet2, Bullet3;
    Player playerShoot;
    private void Awake()
    {
        playerShoot = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(3);
        }
    }

    void SetWeapon(int Weaponid)
    {
        switch(Weaponid)
        {
            case 1:
                {
                    playerShoot.bulletref = BulletPref1;
                    playerShoot.Shoot = Bullet1;
                }
                break;
            case 2:
                {
                    playerShoot.bulletref = BulletPref2;
                    playerShoot.Shoot = Bullet2;
                }
                break;
            case 3:
                {
                    playerShoot.bulletref = BulletPref3;
                    playerShoot.Shoot = Bullet3;
                }
                break;
        }
    }
}
