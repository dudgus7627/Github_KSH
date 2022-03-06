using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGUI : MonoBehaviour
{

    public Image hpbar;
    public static float hp;

    private void Start()
    {

        hp = PlayerState.Instance.currentHp;
        Debug.Log("gui : " + hp);
    }

    void LateUpdate()
    {
        hpbar.fillAmount = PlayerState.Instance.currentHp / hp;
        
    }
}