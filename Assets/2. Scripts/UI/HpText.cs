using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpText : MonoBehaviour
{


    public Text LevelText;
    public Text ExpText;
    void Update()
    {
        LevelText.text = "Level " + PlayerState.Instance.currentLevel.ToString();
        ExpText.text = "Exp " + PlayerState.Instance.currentExp.ToString();

    }
}