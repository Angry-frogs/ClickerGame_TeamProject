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
    public TextMeshProUGUI monsterHpText=null;

    [Header("����/��ȭ ����")]
    // Wepon
    public Image weponimg = null;
    public TextMeshProUGUI weponNameText = null;
    public TextMeshProUGUI weponLevelText = null;
    public TextMeshProUGUI weponPower = null;

    public TextMeshProUGUI upgradCost = null;
    public TextMeshProUGUI upgradePer = null;

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

    public int enforceNum = 0;
    public bool isBuff;

    // monster
    Monster monster;
    Player player;
   

    private void Awake()
    {
        EmptyText();
        UIClear();

        monster = FindObjectOfType<Monster>();
        player = FindObjectOfType<Player>();
        Gold = 1000;
    }
    private void Start()
    {
        monsterHpText.text = "123";
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
                
        Debug.Log("hp" + monster.monsterCurHP / monster.monsterMaxHP);

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
            if (Random.Range(0, 10) >= enforceNum)
            {
                Debug.Log("��ȭ ����" + enforceNum);

                WUpgradeGold = (int)(WUpgradeGold * 1.1f);
                upgradCost.text ="��ȭ ��� : " + WUpgradeGold.ToString() + " G";
                upgradePer.text = "���� Ȯ�� : " + ((10 - enforceNum) * 10).ToString() + " %";
                ++enforceNum;
                if (enforceNum == 10)
                {
                    ++WeaponUpgradeNum;
                    player.WeponLvUP();
                    enforceNum = 0;
                }
            }
            else
            {
                Debug.Log("��ȭ ����");
            }

            weponimg.sprite = player.WeaponImg;
            weponNameText.text = "���� : " + player.WeaponName;
            weponLevelText.text = "��ȭ �ܰ� : " + enforceNum.ToString();
            weponPower.text = "���ݷ� : " + (player.WeaponDmg + enforceNum).ToString();

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

    public void HitToMonster()
    {
        if (monster.monsterCurHP <= 0) return;
        

        monster.monsterCurHP -= player.WeaponDmg + enforceNum;

        AttackDamageText.text = (player.WeaponDmg + enforceNum).ToString();

        if (monster.monsterCurHP <= 0)
        {
            monster.monsterCurHP = 0;   
            GameClearUI.gameObject.SetActive(true); // ���� Ŭ���� ��, ���� ����

            // UI & Monster Clear
            UIClear();

            // �÷��̾�� GiveGold ��ŭ ����
            Gold += monster.monsterGiveGold;

            // 3�� �ڿ� ���ο� ���� ����
            Invoke("NextMonster", 10f);

        }
    }
}
