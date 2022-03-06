using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPManager : MonoBehaviour
{
    protected EnermyState enermyState = new EnermyState();
    public Slider HPSlider;
    public int monsterIndex;

    public void Awake()
    {  
        enermyState.enermyTempelate(monsterIndex);
      
        Debug.Log("EnemyName : " + enermyState.currentName);
        Debug.Log("EnemyHP : " + enermyState.enermyCurrenthp);
        Debug.Log("EnemyDamage : " + enermyState.enermyCurrentdamage);
    }

    public void OnTriggerEnter(Collider collsion)
    {

        if (collsion.gameObject.tag == "Player")
        {
            PlayerState.Instance.currentHp -= enermyState.enermyCurrentdamage;
            Debug.Log("ĳ���� ����");
            Debug.Log("�÷��̾� ü��"+PlayerState.Instance.currentHp);
           
        }
        if (enermyState.enermyCurrenthp <= 0)
        {
            PlayerState.Instance.currentExp += enermyState.enermyCurrentenermyExp;
        }    
    }
}
