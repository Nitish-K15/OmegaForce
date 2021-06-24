using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    private Vector3 screenBounds;
    float timer = 3f;
    void Update()
    {
        if (!Enemy)
            Destroy(gameObject);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < screenBounds.x * 1.2f)
            Enemy.SetActive(true);
       else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                Enemy.SetActive(false);
        }
    }
}
