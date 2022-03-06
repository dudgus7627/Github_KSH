using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioUIManager : MonoBehaviour
{
    private static ScenarioUIManager instance; // 싱글톤이 할당될 static 변수

    // 외부에서 싱글톤 오브젝트를 가져올때 사용할 프로퍼티
    public static ScenarioUIManager Instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (instance == null)
                // 씬에서 Game_Manager_Ex 오브젝트를 찾아 할당
                instance = FindObjectOfType<ScenarioUIManager>();

            // 싱글톤 오브젝트를 반환
            return instance;
        }
    }

    public GameObject[] scenarioHomeUI;
    public GameObject[] scenarioFantasyUI;

    [System.Obsolete]
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        for(int i = 0; i < scenarioHomeUI.Length; i++)
        {
            scenarioHomeUI[i].gameObject.SetActive(false);
        }
        /*
        for (int i = 0; i < scenarioFantasyUI.Length; i++)
        {
            scenarioFantasyUI[i].gameObject.SetActive(false);
        }
        */
    }

    [System.Obsolete]
    void Update()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameState.Reality1:
                if(!scenarioHomeUI[0].gameObject.activeSelf)
                    scenarioHomeUI[0].gameObject.SetActive(true);
                break;
            case GameState.Final:
                if (!scenarioHomeUI[1].gameObject.activeSelf)
                    scenarioHomeUI[1].gameObject.SetActive(true);
                break;
        }

        if(GameManager.Instance.gameState == GameState.Fantasy1 && scenarioFantasyUI[0] == null)
        {
            for(int i = 0; i < 4; i++)
            {
                scenarioFantasyUI[i] = GameObject.Find("Scenario Canvas").transform.GetChild(0).transform.GetChild(i).gameObject;

                if (scenarioFantasyUI[i] != null)
                    scenarioFantasyUI[i].gameObject.SetActive(false);
                else Debug.Log("scenarioFantasyUI[0] NULL");
            }

            if (scenarioFantasyUI[0] != null)
            {
                switch (GameManager.Instance.gameState)
                {
                    case GameState.Fantasy1:
                        if (!scenarioFantasyUI[0].gameObject.activeSelf)
                            scenarioFantasyUI[0].gameObject.SetActive(true);
                        break;
                }
            }
        }
    }

    void SceneClose(int childCount, GameObject scenario)
    {
        for (int i = 0; i < childCount; i++)
        {
            scenario.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
