using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public void ExitApp()   //앱 종료
    {
        Application.Quit();
    }
    public void InfExit()   //종료안내문 활성화
    {
        this.gameObject.SetActive(true);
    }
    public void InfExitOut()    //종료안내문 비활성화
    {
        this.gameObject.SetActive(false);
    }
}
