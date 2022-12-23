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


    // 데미지 출력 텍스트 - 몬스터 위치에 데미지 값 출력

    [Header("플레이어 정보")]
    public TextMeshProUGUI AutoDamageText = null;
    public TextMeshProUGUI AttackDamageText = null;
    public TextMeshProUGUI PlayerGold;

    [Header("타이머")]
    public TextMeshProUGUI TimerText = null;

    [Header("몬스터 정보")]
    public Button Monster = null;
    public TextMeshProUGUI MonsterName = null;
    public TextMeshProUGUI MonsterStage = null;
    public Image att_eff = null;

    [Header("Window UI")]
    public GameObject GameOverUI = null;
    public GameObject GameClearUI = null;
    public GameObject SettingUI = null;

    public Slider monsterHpSlider;
    public TextMeshProUGUI monsterHpText = null;

    [Header("무기/강화 정보")]
    // Wepon
    public GameObject middleUI = null;
    public Button middleButton = null;

    public Image weponimg = null;
    public TextMeshProUGUI weponNameText = null;
    public TextMeshProUGUI weponLevelText = null;
    public TextMeshProUGUI weponPower = null;
    public TextMeshProUGUI ItemDamageUPText = null;
    public TextMeshProUGUI totalDamage = null;

    public Image upgradeSuccess = null;
    public Image upgradeFail = null;
    public Image noGold = null;

    //
    public TextMeshProUGUI upgradeCost = null;
    public TextMeshProUGUI upgradePer = null;
    //

    [Header("상점")]
    public int ItemNum;
    public int ItemCost;
    public int ItemDamageUP = 1;
    public Button[] PotionItem;

    public Button UpgradeButton = null;
    public Button ShopButton = null;
    public Image ShopWindow = null;

    [Header("설정 정보")]
    // Setting
    public Image settingWindow = null;
    public Button settingButton = null;

    public Button BGM_ON_Button = null;
    public Button BGM_OFF_Button = null;
    public Button SE_ON_Button = null;
    public Button SE_OFF_Button = null;

    public Slider SoundBGMSlider = null;
    public Slider effectBGMSlider = null;


    public Button RestartButton = null;
    //데미지
    public TextMeshProUGUI DMGText = null;
    public GameObject DmageUI = null;

    public Button nextStage = null;
    public Button prevStage = null;

    // timer
    float limitTime = 0f;
    public float time; // 30 , 25 초 들어가는 부분
    float timer;

    public int Gold = 0;
    int WUpgradeGold = 10;

    public int WeaponUpgradeNum = 0;

    public int enforceNum = 0;
    public int bestMonsterNum;

    public float SetOffUpgradeUITime;
    public float UITime = 0f;
    public int totaldmg;
    bool OnMiddle = false;

    public bool[] buyPotion = new bool[2] { false, false };

    // monster
    Monster monster;
    //
    Player player;

    SoundEffectController soundController;
    bool IsESC { get; set; }

    private void Awake()
    {
        monster = FindObjectOfType<Monster>();
        player = FindObjectOfType<Player>();
        soundController = FindObjectOfType<SoundEffectController>();
        Gold = 1000;
        IsESC = false;
    }
    private void Start()
    {
        monsterHpText.text = monster.monsterCurHP.ToString() + " / " + monster.monsterMaxHP.ToString();
        monsterHpSlider.value = monster.monsterCurHP / monster.monsterMaxHP;
        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();

        // 타이머 텍스트
        TimerText.text = monster.monsterLimitTime.ToString();
        // 플레이어 골드 텍스트
        PlayerGold.text = Gold.ToString();

        // 몬스터 살아있음
        LiveMonster();
    }

    private void Update()
    {
        // Debug.Log(ItemDamageUP);
        totaldmg = (player.WeaponDmg + enforceNum) * ItemDamageUP;
        weponimg.sprite = player.WeaponImg;
        weponNameText.text = "장비명 : " + player.WeaponName;
        weponLevelText.text = "강화 단계 : " + enforceNum.ToString();
        weponPower.text = "무기 공격력 : " + (player.WeaponDmg + enforceNum);
        ItemDamageUPText.text = "물약 배율 : X" + ItemDamageUP;
        totalDamage.text = "총 공격력 : " + totaldmg;
        

        MiddleMove();
        UpdateUIData();
        UpdateTimer();
        OnupgradeUI();

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ClickESC();
        }
    }

    // ESC 키 누르면 설정창 띄움
    void ClickESC() 
    {
        if (IsESC == false)
        {
            IsESC = true;
            OnClickSettinButton();
        }
        else if (IsESC == true)
        {
            IsESC = false;
            OffClickSettinButton();
        }
    }

    void UpdateUIData()
    {
        monsterHpText.text = monster.monsterCurHP.ToString() + " / " + monster.monsterMaxHP.ToString();
        monsterHpSlider.value = (float)monster.monsterCurHP / (float)monster.monsterMaxHP;


        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();
        PlayerGold.text = Gold.ToString() + " G"; // 보상을 얻었을 때만 업데이트 되는 정보
    }

    public void UpdateTimer()
    {
        if (monster.IsDead) return;


        TimerText.text = monster.monsterLimitTime.ToString();


        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        TimerText.text = timer.ToString();
        if (timer <= limitTime)
        {
            DeadPlayer();            
        }
    }

    //강화 클릭을 하면 웨폰업그레이드 값이 올라감
    public void OnClickWeaponUpgrade()
    {
        if (WeaponUpgradeNum > 4 && enforceNum >= 10)
        {
            WeaponUpgradeNum = 4;
            enforceNum = 10;
            return;
        }
        if (Gold >= WUpgradeGold)
        {
            Gold -= WUpgradeGold;
            PlayerGold.text = Gold.ToString() + " G";
            if (Random.Range(0, 10) >= enforceNum)
            {
                SetOffUpgradeUITime = 0;

                upgradeSuccess.gameObject.SetActive(true);
                upgradeFail.gameObject.SetActive(false);
                noGold.gameObject.SetActive(false);

                soundController.PlaySound("UpgradeSuccess"); // 사운드 출력

                WUpgradeGold = (int)(WUpgradeGold * 1.1f);

                upgradeCost.text = "강화 비용 : " + WUpgradeGold.ToString() + " G";
                upgradePer.text = "성공 확률 : " + ((10 - enforceNum) * 10).ToString() + " %";

                ++enforceNum;
                if (enforceNum == 10 && WeaponUpgradeNum != 4)
                {
                    ++WeaponUpgradeNum;

                    player.WeponLvUP();
                    enforceNum = 0;
                }
                else if (enforceNum == 10 && WeaponUpgradeNum == 4)
                {
                    Debug.Log("더이상 강화할수 없습니다.");
                }
            }
            else
            {
                SetOffUpgradeUITime = 0;
                upgradeSuccess.gameObject.SetActive(false);
                upgradeFail.gameObject.SetActive(true);
                noGold.gameObject.SetActive(false);
                soundController.PlaySound("UpgradeFail"); // 사운드 출력
            }
            // weponimg.sprite = player.WeaponImg;
            // weponNameText.text = "장비명 : " + player.WeaponName;
            // weponLevelText.text = "강화 단계 : " + enforceNum.ToString();
            // weponPower.text = "공격력 : " + totaldmg.ToString();
        }
        else
        {
            SetOffUpgradeUITime = 0;
            upgradeSuccess.gameObject.SetActive(false);
            upgradeFail.gameObject.SetActive(false);
            noGold.gameObject.SetActive(true);
        }
    }

    void OnupgradeUI()
    {
        SetOffUpgradeUITime += Time.deltaTime;
        if (SetOffUpgradeUITime >= 0.5f)
        {
            upgradeSuccess.gameObject.SetActive(false);
            upgradeFail.gameObject.SetActive(false);
            noGold.gameObject.SetActive(false);
            SetOffUpgradeUITime = 0f;
        }

    }

    // 설정 버튼 눌렀을 때
    public void OnClickSettinButton()
    {
        Time.timeScale = 0;
        settingWindow.gameObject.SetActive(true);
        soundController.PlaySound("Click");
    }
    public void OffClickSettinButton()
    {
        Time.timeScale = 1;
        settingWindow.gameObject.SetActive(false);
        soundController.PlaySound("Click");
    }

    // 대장간 버튼 눌렀을 때
    public void OnClickShopButton()
    {
        Time.timeScale = 0;
        ShopWindow.gameObject.SetActive(true);
        soundController.PlaySound("Click");
    }
    public void OffClickShopButton()
    {
        Time.timeScale = 1;
        ShopWindow.gameObject.SetActive(false);
        soundController.PlaySound("Click");
    }

    // 아이템 구매 버튼 눌렀을 때
    public void OnBuyItemButton()
    {
        Debug.Log("물약 정상 구매");

        if (Gold < ItemCost) return; // 골드 부족 표시
        if (buyPotion[ItemNum]) return;

        // 골드차감
        Gold -= ItemCost;
        ItemDamageUP *= 2;

        PotionItem[ItemNum].interactable = false; // 구매한 버튼 비활성화
        buyPotion[ItemNum] = true;
        soundController.PlaySound("Buy"); // 사운드 출력

    }

    // 안쓰는 UI 닦기
    public void UIClear() // 
    {
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(true);
    }

    // 몬스터가 살아있을 때만 몬스터 공격 가능 - 버튼 활성화
    void LiveMonster() 
    {
        Monster.interactable = true;
        Time.timeScale = 1;
    }

    // 몬스터 죽으면 몬스터 공격 불가 - 버튼 비활성화
    public void DeadMonster()
    {
        Monster.interactable = false;
        soundController.PlaySound("MonsterDie"); // 사운드 출력
    }

    // 플레이어 게임오버
    void DeadPlayer()
    {
        Time.timeScale = 0;
        GameOverUI.gameObject.SetActive(true);

    }

    // 몬스터 타격
    public void HitToMonster()
    {
        monster.rectTransform.localScale = new Vector2(0.9f, 0.9f);

        if (monster.IsDead) return;

        soundController.PlaySound("Attack"); // 사운드 출력

        monster.monsterCurHP -= totaldmg;

        // 타격 이펙트 및 텍스트 출력
        Instantiate<Image>(att_eff, Input.mousePosition, Quaternion.identity, Monster.transform);
        Instantiate<TextMeshProUGUI>(DMGText, Input.mousePosition, Quaternion.identity, Monster.transform);

        // 몬스터가 죽었을 때 처리
        if (monster.monsterCurHP <= 0)
        {
            monster.IsDead = true;
            monster.monsterCurHP = 0;
            GameClearUI.gameObject.SetActive(true); // 게임 클리어 시, 게임 멈춤
            RestartButton.gameObject.SetActive(true);

            // UI & Monster Clear
            UIClear();
            DeadMonster();

            // 플레이어에게 GiveGold 만큼 전달
            Gold += monster.monsterGiveGold;
            nextStage.gameObject.SetActive(true);
            if (monster.curMonsterNum >= 4)
            {
                nextStage.gameObject.SetActive(false);
            }

            if (bestMonsterNum <= monster.curMonsterNum)
            {
                ++bestMonsterNum;
            }

        }
    }

    // 왼쪽 중간 무기 UI 슬라이더
    public void OnClickMiddle()
    {
        if (OnMiddle)
        {
            OnMiddle = false;
        }
        else
        {
            OnMiddle = true;
        }
    }

    void MiddleMove()
    {

        if (OnMiddle)
        {
            if (middleUI.transform.position.x >= 1070f) return;

            middleUI.transform.position = Vector2.Lerp(middleUI.transform.position, new Vector2(1070f, 540f), Time.deltaTime * 2f);
            UITime = 0f;
        }
        else
        {
            if (middleUI.transform.position.x < 450f) return;

            middleUI.transform.position = Vector2.Lerp(middleUI.transform.position, new Vector2(450f, 540f), Time.deltaTime * 2f);
            UITime = 0f;
        }
    }

    // 재시작 버튼
    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        soundController.PlaySound("Click"); // 사운드 출력
        Monster.interactable = true;
        monster.IsDead = false;
        monster.monsterCurHP = monster.monsterMaxHP;
        time = monster.monsterLimitTime;
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
    }
}
