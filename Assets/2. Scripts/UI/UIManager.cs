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
        //LevelText.text = "현재레벨"+playerState.currentLevel.ToString();
        //HpText.text = "현재 체력"+playerState.currentHp.ToString();
        //ExpText.text = "현재 경험치"+playerState.currentExp.ToString();
        

    }
   

}
