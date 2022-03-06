using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutController : MonoBehaviour
{
    private static FadeInOutController instance; // �̱����� �Ҵ�� static ����

    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static FadeInOutController Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (instance == null)
                // ������ Game_Manager_Ex ������Ʈ�� ã�� �Ҵ�
                instance = FindObjectOfType<FadeInOutController>();

            // �̱��� ������Ʈ�� ��ȯ
            return instance;
        }
    }

    public Image img;

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        Debug.Log("FadeIn!!");
        StartCoroutine("FadeInCoroutine");
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        Debug.Log("FadeOut!!");
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeInCoroutine()
    {
        for(int i = 0; i < 20; i++)
        {
            float alpha = i / 20.0f;
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return new WaitForSeconds(0.1f);
        }
        img.color = new Color(0f, 0f, 0f, 1.0f);
    }

    IEnumerator FadeOutCoroutine()
    {
        for (int i = 20; i >= 0; i--)
        {
            float alpha = i / 20.0f;
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return new WaitForSeconds(0.1f);
        }
        img.color = new Color(0f, 0f, 0f, 0f);
    }
}
