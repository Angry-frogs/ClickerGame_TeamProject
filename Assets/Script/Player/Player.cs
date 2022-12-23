using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    Weapon[] weapons;



    public Sprite WeaponImg;
    public string WeaponName;
    public int WeaponLevel;
    public int WeaponDmg;

    void Start()
    {
        WeponLvUP();
    }



    public void WeponLvUP()
    {
        if (UIManager.INSTANCE.WeaponUpgradeNum > 4)
        {
            UIManager.INSTANCE.WeaponUpgradeNum = 4;
            return;
        }

        WeaponImg = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponImg;
        WeaponName = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponName;
        WeaponLevel = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponLevel;
        WeaponDmg = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponDmg;
    }




}
