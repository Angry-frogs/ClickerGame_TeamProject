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

    public System.Action action;

    // ������ ��� �ؽ�Ʈ - ���� ��ġ�� ������ �� ���

    [Header("�÷��̾� ����")]
    public TextMeshProUGUI AutoDamageText = null;
    public TextMeshProUGUI AttackDamageText = null;
    public TextMeshProUGUI PlayerGold;

    [Header("Ÿ�̸�")]
    public TextMeshProUGUI TimerText = null;    

    [Header("���� ����")]
    public TextMeshProUGUI MonsterName;
    public TextMeshProUGUI MonsterStage;

    [Header("Window UI")]
    public GameObject GameOverUI = null;
    public GameObject GameClearUI = null;
    public GameObject SettingUI = null;

    public Slider monsterHpSlider;
    public TextMeshProUGUI monsterHpText;

    [Header("����/��ȭ ����")]
    // Wepon
    public TextMeshProUGUI weponNameText = null;
    public TextMeshProUGUI weponLevelText = null;
    public TextMeshProUGUI weponPower = null;

    // public TextMeshProUGUI goldText = null;
    public Button UpgradeButton = null;
    public Button ShopButton = null;
    public Image ShopWindow = null;

    [Header("���� ����")]
    // Setting
    public Image settingWindow = null;
    public Button settingButton = null;
    // public Button gameEndButton = null;

    public Slider SoundBGMSlider = null;
    public Slider effectBGMSlider = null;

    // timer
    float limitTime = 0f;
    float time = 30;
    float timer;

    public int Gold = 0;
    int WUpgradeGold = 10;
    int WSUpgradeGold = 100;
    public int WeaponUpgradeNum = 0;

    public int CurRan = 0;
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
        PlayerGold.text = Gold.ToString() + " G"; // ������ ����� ���� ������Ʈ �Ǵ� ����
    }

    void UpdateTimer()
    {

        TimerText.text = monster.monsterLimitTime.ToString();

        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        TimerText.text =  timer.ToString();
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

        if (Gold > WUpgradeGold)
        {
            Gold -= WUpgradeGold;
            PlayerGold.text = Gold.ToString() + " G";
            if (Random.Range(0, 10) >= CurRan)
            {
                Debug.Log("��ȭ ����" + CurRan);

                WUpgradeGold = (int)(WUpgradeGold * 1.1f);
                ++CurRan;
                if (CurRan == 10)
                {
                    ++WeaponUpgradeNum;
                    action();

                    CurRan = 0;
                }
            }
            else
            {
                Debug.Log("��ȭ ����");
            }

        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void OnClickSpecialWeaponUpgrade()
    {
        if (Gold > WSUpgradeGold && isBuff == false)
        {
            Gold -= WSUpgradeGold;
            PlayerGold.text = Gold.ToString()+ " G";
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

    public void OnClickShopButton() 
    {
        ShopWindow.gameObject.SetActive(true);
    }
    public void OffClickShopButton()
    {
        ShopWindow.gameObject.SetActive(false);
    }

    public void UIClear() 
    {
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
        SettingUI.gameObject.SetActive(false);
    }

}
