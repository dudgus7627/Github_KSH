using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//실제 값을 적용하기 위한 스크립트
public class Save
{
    public int saveExp;
    public int saveHp;
    public int saveStamina;
    public int saveLevel;

    public Save(int saveExp, int saveHp, int saveStamina, int saveLevel)
    {
        this.saveExp = saveExp;
        this.saveHp = saveHp;
        this.saveStamina = saveStamina;
        this.saveLevel = saveLevel;

    }
}