using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���� �����ϱ� ���� ��ũ��Ʈ
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