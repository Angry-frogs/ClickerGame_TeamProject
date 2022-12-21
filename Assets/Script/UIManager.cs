using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //
    #region SingleTon

    static public UIManager instance;
    static public UIManager INSTANCE
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }

            }
            return instance;
        }
    }
    #endregion
    //


    // ������ ��� �ؽ�Ʈ - ���� ��ġ�� ������ �� ���

    public TextMeshProUGUI AutoDamageText;
    public TextMeshProUGUI AttackDamageText;
    public TextMeshProUGUI TimteText;
    public GameObject GameOverUI;
    public GameObject GameClearUI;

    public Slider monsterHpSlider;
    public TextMeshProUGUI monsterHpText;

    // timer
    float limitTime = 0f;
    float time = 31;
    float timer;

    // monster
    Monster monster;

    private void Awake()
    {
        EmptyText();
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);

        monster = FindObjectOfType<Monster>();
    }
    private void Start()
    {
        TimteText.text = limitTime.ToString();
        monsterHpSlider.value = monster.monsterMaxHP;
        monsterHpText.text = monster.monsterHP.ToString()+ " / " + monster.monsterMaxHP.ToString();       
        
    }


    private void Update()
    {

        Invoke("EmptyText", 0.2f);

        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        if (time >= 10)
        {
            TimteText.text = "00" + " : " + timer.ToString();
        }
        else if (time < 10)
        {
            TimteText.text = "00" + " : " + "0" + timer.ToString();
        }
        if (timer <= limitTime) 
        {
            Time.timeScale = 0;
            GameOverUI.gameObject.SetActive(true);

            // Ÿ�̸Ӱ� 0�� �Ǹ� ������ ü���� �ִ�ü������ �����Ѵ�.
            monster.monsterHP = monster.monsterMaxHP;
        }
    }

    private void FixedUpdate()
    {
        monsterHpSlider.value = monster.monsterHP;
    }

    void EmptyText()
    {
        AttackDamageText.text = "";
        AutoDamageText.text = "";
    }

}
