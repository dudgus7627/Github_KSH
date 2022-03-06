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
            Debug.Log("캐릭터 만남");
            Debug.Log("플레이어 체력" + PlayerState.Instance.currentHp);
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
        //Debug.Log("몬스터 체력 :" + enermyState1.enermyCurrenthp);
    }
}
