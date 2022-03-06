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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<PlayerState>();

            // �̱��� ������Ʈ�� ��ȯ
            return instance;

        }
    }
    //�÷��̾� ����
    public int currentExp;
    public int currentHp;
    public int currentStamina;
    public int currentLevel;

    //�÷��̾� �ʱⰪ ����

    public void PlayerTempelate()
    {
        currentHp = FBManager.Instance.fireBasePlayerState.maxHp[0];
        currentStamina = FBManager.Instance.fireBasePlayerState.maxStamina[0];
        currentExp = 0;
        currentLevel = 0;
        Debug.Log("-------------------");
        Debug.Log("�÷��̾� ���� ����ġ :" + currentExp);
        Debug.Log("�÷��̾� ���� ü�� :" + currentHp);
        Debug.Log("�÷��̾� ���� ���׹̳� :" + currentStamina);
        Debug.Log("�÷��̾� ���� ���� :" + currentLevel);
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
            Debug.Log("������ �߽��ϴ�");
            Debug.Log("�÷��̾� ���緹�� :" + currentLevel);
            Debug.Log("�÷��̾� ����ü�� :" + currentHp);
            Debug.Log("�÷��̾� ���罺�׹̳� :" + currentStamina);
            Debug.Log("-------------------");
        }


    }

}
