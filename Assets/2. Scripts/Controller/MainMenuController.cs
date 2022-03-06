using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject start;
    public GameObject load;
    public GameObject exit;
    private FadeInOutController fadeInOut;
    public  string sceneName;

    private void Start()
    {
        fadeInOut = GetComponent<FadeInOutController>();
    }

    [ContextMenu("Canvas Start")]
    public void BtnStart()
    {
        Debug.Log("Start Button!");
        FBManager.Instance.GetPlayerInfo();
        ChangeScene();
    }

    public void BtnLoad()
    {
        Debug.Log("Load Button!");
        FBManager.Instance.LoadPlayerInfo();
        ChangeScene();
    }

    public void BtnExit()
    {
        Debug.Log("Exit Button!");
        Application.Quit();
    }

    void ChangeScene()
    {
        if (FBManager.Instance.bLoaded)
        {
            Debug.Log("DB가 연결되었습니다.");

            GameManager.Instance.ChangeScene();
        }
        else
        {
            Debug.Log("DB 연결중입니다. 다시시도 하세요.");
        }
    }

    private void BtnActive()
    {
        start.SetActive(false);
        load.SetActive(false);
        exit.SetActive(false);
    }
}
