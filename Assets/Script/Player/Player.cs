using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Monster monster;
    [SerializeField]
    Weapon[] weapons;

    // ����ĳ��Ʈ �ִ�Ÿ�
    float MaxDistance = 1000f;
    // �� ���콺
    Vector3 MousePos;
    // �� ī�޶� 
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        monster = FindObjectOfType<Monster>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePos, transform.forward, MaxDistance);
            if (hit == false)
            {
                return;
            }
            Debug.DrawRay(MousePos, transform.forward * 10, Color.red, 0.3f);
            if (hit.collider.CompareTag("Monster") == true)
            {
                if (UIManager.INSTANCE.isBuff)
                {
                    monster.HitToMonster(weapons[UIManager.instance.WeaponUpgrade].WeaponDmg * 2);
                }
                else
                {
                    monster.HitToMonster(weapons[UIManager.instance.WeaponUpgrade].WeaponDmg);
                }

                /*if (hit)
                {
                    hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
                }*/
            }

        }
    }

   
}
