using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public PlayerState playerState;
    public Text LevelText;
    public Text HpText;
    public Text ExpText;


    void LateUpdate()
    {
        //LevelText.text = "���緹��"+playerState.currentLevel.ToString();
        //HpText.text = "���� ü��"+playerState.currentHp.ToString();
        //ExpText.text = "���� ����ġ"+playerState.currentExp.ToString();
        

    }
   

}
