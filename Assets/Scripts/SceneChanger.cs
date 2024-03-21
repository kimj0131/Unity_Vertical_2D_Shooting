using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GameTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GameplayScene()
    {
        SceneManager.LoadScene(1);
    }
}
