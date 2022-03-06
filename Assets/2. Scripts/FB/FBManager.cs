using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static FireBaseEnermyState;

public class FBManager : MonoBehaviour
{

    private static FBManager instance;
    public GameObject player;

    public static FBManager Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<FBManager>();

            // �̱��� ������Ʈ�� ��ȯ
            return instance;

        }
    }

    private DatabaseReference _referencePlayer;
    private DatabaseReference _referenceEnermy;
    private DatabaseReference _referenceSave;
    private DatabaseReference _referenceLoad;
    private string referencePlayer = "Release/Player";
    private string referenceEnermy = "Release/Enermy";
    private string referenceSave = "Release/Save/User1";
    private string referenceLoad = "Release/Save";
    public FireBasePlayerState fireBasePlayerState;
    public FireBaseEnermyState fireBaseEnermyState;
    public Save playerSave;

    public bool bLoaded = false;
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //���ͳ� ���� �����϶��� DB���� �ϵ��� �ϴ� �Լ�
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);

        AppOptions options = new AppOptions { DatabaseUrl = new Uri("https://creeps-566b4-default-rtdb.firebaseio.com/") };
        FirebaseApp.Create(options);

        _referencePlayer = FirebaseDatabase.DefaultInstance.GetReference(referencePlayer);
        _referenceEnermy = FirebaseDatabase.DefaultInstance.GetReference(referenceEnermy);
        _referenceSave = FirebaseDatabase.DefaultInstance.GetReference(referenceSave);

        Debug.Log(_referenceSave.ToString());
    }

    //ó�� �ϱ�(GetPlayerInfo()�� ���� �ϹǷ� �÷��̾� �ʱⰪ�� �޾ƿ��� ,  GetEnermyInfo() �� �������ֹǷ� ���� ���� �޾ƿ´�)
    public void OnNewStart()
    {
        GetPlayerInfo();
    }
    //�̾��ϱ�(LoadPlayerInfo() �� �����ϹǷ� ���尪�� �ҷ����� ���������� GetEnermyInfo()�� �������ֹǷ� ���� ���� �޾ƿ´�)
    public void OnLoadStart()
    {
        LoadPlayerInfo();
    }

    //�÷��̾� �⺻ ���� �޾ƿ��� �޼ҵ�
    public void GetPlayerInfo()
    {
        //Debug.Log("ReadInformation");
        _referencePlayer.GetValueAsync().ContinueWith(task =>
        {

            if (task.IsFaulted)
            {
                Debug.LogError("failed reading...");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = task.Result.GetRawJsonValue();
                fireBasePlayerState = JsonUtility.FromJson<FireBasePlayerState>(json);
                GetEnermyInfo();
            }
        });
    }

    //���� ���� �޾ƿ��� �޼ҵ�
    void GetEnermyInfo()
    {
        _referenceEnermy.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("failed EnermyInfo reading...");
            }
            else if (task.IsCompleted)
            {
                bLoaded = true;
                DataSnapshot snapshot = task.Result;
                string json = task.Result.GetRawJsonValue();
                fireBaseEnermyState = JsonUtility.FromJson<FireBaseEnermyState>(json);

                player.AddComponent<PlayerManager>();
            }
        });
    }

    //���� �� �ҷ����� �޼���
    public void LoadPlayerInfo()
    {
        _referenceSave.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("failed LoadPlayerInfo reading...");
            }
            else if (task.IsCompleted)
            {
                bLoaded = true;

                DataSnapshot snapshot = task.Result;
                string json = task.Result.GetRawJsonValue();
                if (json == null)
                {
                    Debug.Log("�����Ͱ� �����ϴ�.");
                    playerSave = null;
                }
                else
                {
                    playerSave = JsonUtility.FromJson<Save>(json);
                    Debug.Log("������ ���ֽ��ϴ�");
                    GetEnermyInfo();
                }
            }
        });
    }
    //DB���� ��
    public void OnSave(int c_exp, int c_hp, int c_st, int c_lv)
    {
        Save save = new Save(c_exp, c_hp, c_st, c_lv);
        string json = JsonUtility.ToJson(save);
        _referenceSave.SetRawJsonValueAsync(json);
    }
}
