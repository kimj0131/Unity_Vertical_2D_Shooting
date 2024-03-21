using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //아이템타입 변수
    public string Type;
    Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        //아래로 내려오는 속도
        rigid.velocity = Vector2.down * 1.8f;
    }
}
