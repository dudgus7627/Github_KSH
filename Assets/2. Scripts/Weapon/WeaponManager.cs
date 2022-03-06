using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager 
{
    public enum WeponName
    {
        Knife,
        Axe,
        Pistol,
        Uzi
    }

    [Header("Knife")]
    public int knifeAttack = 250; 

    [Header("Axe")]
    public int AxeAttack = 200;

    [Header("Pistol")]
    public int pistolAttack = 200;
    public int pistolCurrentBuellt; 
    public int pistolMaxBuellt = 7;
    
    [Header("Uzi")]
    public int uziAttack = 70;
    public int uziCurrentBuellt;
    public int uziMaxBuellt = 2500;

    public int GetDamage(WeponName wname)
    {
        switch (wname)
        {
            case WeponName.Knife:
                return knifeAttack;
            case WeponName.Axe:
                return AxeAttack; 
            case WeponName.Pistol:
                return pistolAttack;
            case WeponName.Uzi: 
                return uziAttack;
        }


        return 0;
    }

}
