using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enermy : MonoBehaviour
{
    public int monsterIndex;
    public EnermyState enermyState = new EnermyState();
    public Slider HPSlider;
    public bool Damage = false;
    public int dam;


    public void Awake()
    {
        enermyState.enermyTempelate(monsterIndex);

    }
    public void OnTriggerEnter(Collider collsion)
    {

        if (collsion.gameObject.tag == "Player")
        {
            PlayerState.Instance.currentHp -= enermyState.enermyCurrentdamage;
            Debug.Log("ĳ���� ����");
            Debug.Log("�÷��̾� ü��" + PlayerState.Instance.currentHp);
        }

        if (enermyState.enermyCurrenthp <= 0)
        {
            //Destroy(this.gameObject);
            PlayerState.Instance.currentExp += enermyState.enermyCurrentenermyExp;
        }
    }
    public void SetDamage(int damage)
    {
        dam = damage;
        enermyState.enermyCurrenthp -= damage;
        HPSlider.value = enermyState.enermyCurrenthp;
        //Debug.Log("���� ü�� :" + enermyState1.enermyCurrenthp);
    }
}
