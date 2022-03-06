using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioUIManager : MonoBehaviour
{
    private static ScenarioUIManager instance; // �̱����� �Ҵ�� static ����

    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static ScenarioUIManager Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
                // ������ Game_Manager_Ex ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<ScenarioUIManager>();

            // �̱��� ������Ʈ�� ��ȯ
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
