using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int ClickCount=0;
    // �÷��̾ Ŭ���� ������ ����

    // Ŭ�����ص� ������ ����
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    SpriteRenderer sr;
    public GameObject go;

    private void Awake()
    {
        sr = go.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
            
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
                if (hit.transform.gameObject.tag == "Monster")
                {
                    ClickCount++;
                    sr.material.color = Color.red;
                    // �浹�� ��ü ���� ����
                }
            }
        }
        
    }
}
