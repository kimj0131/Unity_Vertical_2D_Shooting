using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    public void Help() //도움말 UI 활성
    {
        this.gameObject.SetActive(true);
    }

    public void HelpOut() //도움말 UI 비활성
    {
        this.gameObject.SetActive(false);
    }
}
