using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{

    private static PlayerState instance;

    public static PlayerState Instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (instance == null)
                // 씬에서 GameManager 오브젝트를 찾아 할당
                instance = FindObjectOfType<PlayerState>();

            // 싱글톤 오브젝트를 반환
            return instance;

        }
    }
    //플레이어 변수
    public int currentExp;
    public int currentHp;
    public int currentStamina;
    public int currentLevel;

    //플레이어 초기값 세팅

    public void PlayerTempelate()
    {
        currentHp = FBManager.Instance.fireBasePlayerState.maxHp[0];
        currentStamina = FBManager.Instance.fireBasePlayerState.maxStamina[0];
        currentExp = 0;
        currentLevel = 0;
        Debug.Log("-------------------");
        Debug.Log("플레이어 현재 경험치 :" + currentExp);
        Debug.Log("플레이어 현재 체력 :" + currentHp);
        Debug.Log("플레이어 현재 스테미나 :" + currentStamina);
        Debug.Log("플레이어 현재 레벨 :" + currentLevel);
        Debug.Log("-------------------");
    }


    public void LevelCheck()
    {
        if (currentExp >= FBManager.Instance.fireBasePlayerState.levelUpExp[currentLevel + 1])
        {

            currentLevel++;
            currentHp = FBManager.Instance.fireBasePlayerState.maxHp[currentLevel];
            currentStamina = FBManager.Instance.fireBasePlayerState.maxStamina[currentLevel];
            currentExp = 0;
            Debug.Log("-------------------");
            Debug.Log("레벨업 했습니다");
            Debug.Log("플레이어 현재레벨 :" + currentLevel);
            Debug.Log("플레이어 현재체력 :" + currentHp);
            Debug.Log("플레이어 현재스테미나 :" + currentStamina);
            Debug.Log("-------------------");
        }


    }

}
