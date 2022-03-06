using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutController : MonoBehaviour
{
    private static FadeInOutController instance; // 싱글톤이 할당될 static 변수

    // 외부에서 싱글톤 오브젝트를 가져올때 사용할 프로퍼티
    public static FadeInOutController Instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (instance == null)
                // 씬에서 Game_Manager_Ex 오브젝트를 찾아 할당
                instance = FindObjectOfType<FadeInOutController>();

            // 싱글톤 오브젝트를 반환
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
