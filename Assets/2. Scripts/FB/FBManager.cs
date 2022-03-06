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
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (instance == null)
                // 씬에서 GameManager 오브젝트를 찾아 할당
                instance = FindObjectOfType<FBManager>();

            // 싱글톤 오브젝트를 반환
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

        //인터넷 연결 상태일때만 DB저장 하도록 하는 함수
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);

        AppOptions options = new AppOptions { DatabaseUrl = new Uri("https://creeps-566b4-default-rtdb.firebaseio.com/") };
        FirebaseApp.Create(options);

        _referencePlayer = FirebaseDatabase.DefaultInstance.GetReference(referencePlayer);
        _referenceEnermy = FirebaseDatabase.DefaultInstance.GetReference(referenceEnermy);
        _referenceSave = FirebaseDatabase.DefaultInstance.GetReference(referenceSave);

        Debug.Log(_referenceSave.ToString());
    }

    //처음 하기(GetPlayerInfo()를 선언 하므로 플레이어 초기값을 받아오고 ,  GetEnermyInfo() 을 선언해주므로 몬스터 값을 받아온다)
    public void OnNewStart()
    {
        GetPlayerInfo();
    }
    //이어하기(LoadPlayerInfo() 를 선언하므로 저장값을 불러오고 마찬가지로 GetEnermyInfo()을 선언해주므로 몬스터 값을 받아온다)
    public void OnLoadStart()
    {
        LoadPlayerInfo();
    }

    //플레이어 기본 정보 받아오는 메소드
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

    //몬스터 정보 받아오는 메소드
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

    //저장 값 불러오는 메서드
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
                    Debug.Log("데이터가 없습니다.");
                    playerSave = null;
                }
                else
                {
                    playerSave = JsonUtility.FromJson<Save>(json);
                    Debug.Log("데이터 가있습니다");
                    GetEnermyInfo();
                }
            }
        });
    }
    //DB저장 값
    public void OnSave(int c_exp, int c_hp, int c_st, int c_lv)
    {
        Save save = new Save(c_exp, c_hp, c_st, c_lv);
        string json = JsonUtility.ToJson(save);
        _referenceSave.SetRawJsonValueAsync(json);
    }
}
