using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyState
{


    public int enermyCurrentdamage;
    public int enermyCurrenthp;
    public int enermyCurrentenermyExp;
    public string currentName;
    public int MaxHp;


    public void enermyTempelate(int monsterIndex)
    {

        enermyCurrentdamage = FBManager.Instance.fireBaseEnermyState.damage[monsterIndex];
        enermyCurrenthp = FBManager.Instance.fireBaseEnermyState.hp[monsterIndex];
        enermyCurrentenermyExp = FBManager.Instance.fireBaseEnermyState.enermyExp[monsterIndex];
        currentName = FBManager.Instance.fireBaseEnermyState.name[monsterIndex];
        MaxHp = enermyCurrenthp;
        //if (enermyCurrentdamage != 0 && enermyCurrenthp != 0 && enermyCurrentenermyExp != 0 && currentName != null) break;
    }
}