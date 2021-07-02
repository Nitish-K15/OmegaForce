using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    public GameObject Player;
    [SerializeField]
    float timeOffset;
    [SerializeField]
    Vector2 posOffset;
    [SerializeField]
    float upperlimit, lowerlimit, leftlimit, rightlimit;
    private void Start()
    {
        transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,-2);
    }
    private void Update()
    {
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
