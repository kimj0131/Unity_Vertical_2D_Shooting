using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Background : MonoBehaviour
{

    public float speed;             //스크롤 속도
    public int startIndex;          //뷰 위쪽 배경시작점
    public int endIndex;            //한번 사용해서 맨아래까지 스크롤된 배경
    public Transform[] sprites;     //각 배경그룹은 스프라이트를 3개씩 가지고 있으므로

    float viewHeight;               //카메라 높이를 가져옴

    private void Awake()
    {
        //메인카메라를 가져옴
        //실제뷰(게임화면)의 높이가 나옴
        viewHeight = Camera.main.orthographicSize * 2;
    }
    void Update()
    {
        Move();
        Scrolling();

    }
    void Move()
    {
        //Inspector 상의 speed속도에 따라 아래로 움직임
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void Scrolling()
    {
        //카메라 세팅에따라서 유기적으로 설정됨
        if (sprites[endIndex].position.y < viewHeight * (-1))       //아래로 스크롤된 배경의 y축 포지션이 실제뷰 높이보다 아래일때
        {
            //스프라이트 재사용
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * 10;

            //Index 
            int startIndexSave = startIndex;
            startIndex = endIndex;              //endIndex를 startIndex로 갱신
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;       //배열을 넘어가지않도록 예외처리
        }
    }
}