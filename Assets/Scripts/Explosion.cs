using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//폭파효과
public class Explosion : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
  
    //충돌이벤트가 없어서 스스로 비활성화
    void OnEnable()
    {
        Invoke("Disable", 1f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    public void StartExplosion(string target)
    {
        anim.SetTrigger("OnExplosion");
        
        //비활성화 되는 대상의 크기에 따라 스케일 변화
        switch (target)
        {
            case "P":
                transform.localScale = Vector3.one * 0.8f;
                break;
            case "ST_G":
            case "ST_R":
                transform.localScale = Vector3.one * 0.7f;
                break;
            case "S_G":
            case "S_R":
                transform.localScale = Vector3.one * 1f;
                break;
            case "M_G":
            case "M_R":
                transform.localScale = Vector3.one * 1f;
                break;
            case "L_G":
            case "L_R":
                transform.localScale = Vector3.one * 2f;
                break;
            case "B":
                transform.localScale = Vector3.one * 3f;
                break;
        }
    }
}
