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
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI PlayerGold;
    public TextMeshProUGUI MonsterName;
    public TextMeshProUGUI MonsterStage;

    public GameObject GameOverUI;
    public GameObject GameClearUI;
    public GameObject SettingUI;

    public Slider monsterHpSlider;
    public TextMeshProUGUI monsterHpText;

    // timer
    float limitTime = 0f;
    float time = 30;
    float timer;

    public int Gold = 0;
    int WUpgradeGold = 10;
    int WSUpgradeGold = 100;
    public int WeaponUpgrade = 0;
    int CurRan = 0;
    public bool isBuff;

    // monster
    Monster monster;
   

    private void Awake()
    {
        EmptyText();
        UIClear();

        monster = FindObjectOfType<Monster>();
        Gold = 1000;
    }
    private void Start()
    {
        monsterHpText.text = monster.monsterCurHP.ToString()+ " / " + monster.monsterMaxHP.ToString();       
        monsterHpSlider.value = monster.monsterCurHP/monster.monsterMaxHP;
        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();

        TimerText.text = monster.monsterLimitTime.ToString();
        PlayerGold.text = Gold.ToString();
    }

    // �ּ�
    private void Update()
    {

        Invoke("EmptyText", 1f);
        UpdateUIData();
        UpdateTimer();
    }

    void UpdateUIData() 
    {
        monsterHpText.text = monster.monsterCurHP.ToString() + " / " + monster.monsterMaxHP.ToString();
        monsterHpSlider.value = (float)monster.monsterCurHP / (float)monster.monsterMaxHP;

        
        //Debug.Log("hp" + monster.monsterCurHP / monster.monsterMaxHP);


        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();
        PlayerGold.text = Gold.ToString(); // ������ ����� ���� ������Ʈ �Ǵ� ����
    }

    void UpdateTimer()
    {

        TimerText.text = monster.monsterLimitTime.ToString();

        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        if (time >= 10)
        {
            TimerText.text = "00" + " : " + timer.ToString();
        }
        else if (time < 10)
        {
            TimerText.text = "00" + " : " + "0" + timer.ToString();
        }
        if (timer <= limitTime)
        {
            Time.timeScale = 0;
            GameOverUI.gameObject.SetActive(true);

            // Ÿ�̸Ӱ� 0�� �Ǹ� ������ ü���� �ִ�ü������ �����Ѵ�.
            monster.monsterCurHP = monster.monsterCurHP;
        }


    }

    void EmptyText()
    {
        AttackDamageText.text = "";
        AutoDamageText.text = "";
    }

    //��ȭ Ŭ�����ϸ� �������׷��̵� ���� �ö�


    public void OnClickWeaponUpgrade()
    {
  
        if(Gold > WUpgradeGold)
        {
            Gold -= WUpgradeGold;
            if (Random.Range(0, 10) >= CurRan)
            {
            ++WeaponUpgrade;
            WUpgradeGold = (int)(WUpgradeGold * 1.1f);
                ++CurRan;
                if (CurRan == 5)
                {
                    CurRan = 0;
                }
            }
            //��尪 ���ֱ�
            
        }
    }

    public void OnClickSpecialWeaponUpgrade()
    {
        if(Gold > WSUpgradeGold)
        {
            Gold -= WSUpgradeGold;
            isBuff = true;
            //��ư�ı�
        }
    }

    public void OnClickSettinButton() 
    {
        SettingUI.gameObject.SetActive(true);
    }
    public void OffClickSettinButton()
    {
        SettingUI.gameObject.SetActive(false);
    }

    public void UIClear() 
    {
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
        SettingUI.gameObject.SetActive(false);
    }

}
