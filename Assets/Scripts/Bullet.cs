using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public bool isRotate;
    void Update()
    {
        if (isRotate)   //회전속도
            transform.Rotate(Vector3.forward * 8);       
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //BorderBullet 경계에 닿으면 오브젝트 비활성화
        if(collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
