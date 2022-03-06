using System.Collections;
using System.Configuration;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public enum GameState
{
    MainMenu,
    Reality1,
    Fantasy1,
    Final,
    EndingCredit
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance; // �̱����� �Ҵ�� static ����

    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
                // ������ Game_Manager_Ex ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<GameManager>();

            // �̱��� ������Ʈ�� ��ȯ
            return instance;
        }
    }

    public GameState gameState;
    public GameObject playerPrefab;
    public GameObject player;
    public string sceneName;

    public Vector3 mainMenuPos;
    public Vector3 homeStartPos;
    public Vector3 finalPos;
    public Vector3 finalKnifePos;
    public Vector3[] fantasyPos;
    public Vector3 endingPos;

    public GameObject[] bloodParticle;


    public bool isGameover { get; private set; } // ���� ���� ����

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        gameState = GameState.MainMenu;

        // ���� �̱��� ������Ʈ�� �� �ٸ� Game_Manager_Ex ������Ʈ�� �ִٸ� �ڽ��� �ı�
        if (Instance != this) Destroy(gameObject);

        mainMenuPos = new Vector3(0, 3f, 1f);
        homeStartPos = new Vector3(370.15f, 2.4f, 405.8329f);
        fantasyPos[0] = new Vector3(4593f, 235.1047f, 3890f);
        finalPos = new Vector3(370.079f, 9.158f, 412.06f);
        finalKnifePos = new Vector3(370.079f, 7.507f, 410.66f);
        endingPos = new Vector3(0f, 0f, 0f);

        if (player == null)
        {
            player = Instantiate(playerPrefab);
            player.transform.position = mainMenuPos;
            //player.transform.localRotation = Quaternion.Euler(0, 180f, 0);

            Player PlayerClass = player.GetComponent<Player>();

            DontDestroyOnLoad(player.gameObject);

            // ���̵� �ƿ�
            FadeInOutController.Instance.FadeOut();
        }
    }

    public void ChangePosition()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameState.MainMenu:
                player.transform.position = mainMenuPos;
                break;
            case GameState.Reality1:
                StartCoroutine(@PlayerMovePosCoroutine(player, homeStartPos));
                player.AddComponent<PlayerManager>();
                player.GetComponent<Rigidbody>().useGravity = false;
                break;
            case GameState.Fantasy1:
                StartCoroutine(@PlayerMovePosCoroutine(player, fantasyPos[0]));
                player.GetComponent<Rigidbody>().useGravity = true;
                GameObject.Find("Portal Check").transform.SetParent(player.transform);
                player.GetComponent<FantasyTrigger>().player = player;
                break;
            case GameState.Final:
                StartCoroutine(@PlayerMovePosCoroutine(player, finalPos));
                //EndGame();
                break;
            case GameState.EndingCredit:
                StartCoroutine(@PlayerMovePosCoroutine(player, endingPos));
                break;
        }
    }

    IEnumerator @PlayerMovePosCoroutine(GameObject _player , Vector3 _pos)
    {
        _player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
        _player.GetComponent<ContinuousTurnProviderBase>().enabled = false;
        _player.GetComponent<NavMeshAgent>().enabled = false;
        _player.transform.position = _pos;

        yield return null;

        _player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
        _player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
        //_player.GetComponent<NavMeshAgent>().enabled = true;

        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);

        // ���̵� ��
        FadeInOutController.Instance.FadeIn();
        FadeInOutController.Instance.FadeOut();
    }

    public void ChangeScene()
    {
        Debug.Log(GameManager.Instance.gameState);

        switch (GameManager.Instance.gameState)
        {
            case GameState.MainMenu:
                gameState = GameState.Reality1;
                sceneName = "Reality";
                break;
            case GameState.Reality1:
                gameState = GameState.Fantasy1;
                sceneName = "Fantasy-Alpha";
                HomeTrigger.Instance.tryDrug = false;
                break;
            case GameState.Final:
                sceneName = "Final";
                break;
            case GameState.EndingCredit:
                sceneName = "EndingCredit";
                break;
        }

        ChangePosition();
    }


    // ���� ���� ó��
    public void EndGame()
    {
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);

        // ���̵� ��
        FadeInOutController.Instance.FadeIn();
        FadeInOutController.Instance.FadeOut();

        /*
        Transform parent = GameObject.Find("Parent").transform;
        Transform friend = GameObject.Find("Friend").transform;

        // �� ��ƼŬ
        for (int i = 0; i < bloodParticle.Length; i++)
        {
            Instantiate(bloodParticle[i], parent.position, parent.rotation);
            Instantiate(bloodParticle[i], friend.position, friend.rotation);
        }
        */
    }
}