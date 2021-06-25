using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    GameObject Player;
    [SerializeField]
    float timeOffset;
    [SerializeField]
    Vector2 posOffset;
    [SerializeField]
    float upperlimit, lowerlimit, leftlimit, rightlimit;
    private void Update()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        Vector3 startPos = transform.position;
        Vector3 endPos = Player.transform.position;
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;
        transform.position = Vector3.Lerp(startPos,endPos,timeOffset * Time.deltaTime);
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftlimit, rightlimit),
                Mathf.Clamp(transform.position.y, lowerlimit, upperlimit),
                transform.position.z
            );
    }
}
