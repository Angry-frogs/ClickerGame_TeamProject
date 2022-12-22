using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int AttackPower = 1;

    Monster monster;
    [SerializeField]
    Weapon[] weapons;

    // ����ĳ��Ʈ �ִ�Ÿ�
    float MaxDistance = 1000f;
    // �� ���콺
    Vector3 MousePos;
    // �� ī�޶� 
    Camera cam;

    public Sprite WeaponImg;
    public string WeaponName;
    public int WeaponLevel;
    public int WeaponDmg;

    void Start()
    {
        cam = Camera.main;
        monster = FindObjectOfType<Monster>();

        WeponLvUP();
    }

    void Update()
    {

    }
    /*public void OnAttack()
    {        
        // monster.HitToMonster(AttackPower);
        UIManager.instance.weponPower.text = weapons[UIManager.instance.WeaponUpgradeNum].ToString();
    }*/
    public void WeponLvUP()
    {
        WeaponImg = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponImg;
        WeaponName = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponName;
        WeaponLevel = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponLevel;
        WeaponDmg = weapons[UIManager.INSTANCE.WeaponUpgradeNum].WeaponDmg;
    }




}
