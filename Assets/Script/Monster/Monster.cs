using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Monster : MonoBehaviour
{
    public RectTransform rectTransform;

    public MonsterData[] monsterData = null;

    public string monsterName { get; set; }
    public int monsterStage { get; set; }
    public int monsterCurHP { get; set; }
    public int monsterMaxHP { get; set; }
    public int monsterGiveGold { get; set; }
    public float monsterLimitTime { get; set; }
    public Image monsterImage { get; set; }
    public bool IsDead { get; set; }

    int autoDamage { get; set; }

    public int curMonsterNum;

    bool isNewMonster { get; set; }
    

    private void Awake()
    {

        monsterImage = GetComponent<Image>();

        curMonsterNum = 0;

        monsterName = monsterData[curMonsterNum].Name;

        monsterCurHP = monsterData[curMonsterNum].HP;
        monsterMaxHP = monsterData[curMonsterNum].HP;

        monsterStage = monsterData[curMonsterNum].Stage;

        monsterGiveGold = monsterData[curMonsterNum].GiveGold;

        monsterLimitTime = monsterData[curMonsterNum].LimitTime;

        monsterImage.sprite = monsterData[curMonsterNum].Image;

        autoDamage = 1;

        IsDead = false;

        rectTransform = GetComponent<RectTransform>();

    }

    // Start is called before the first frame update
    void Start()
    {
        isNewMonster = false;

    }
    float time = 0f;

    // Update is called once per frame
    void Update()
    {
        if (UIManager.INSTANCE.time <= 0)
        {
            UIManager.INSTANCE.time = 0;
            return;
        }
        if (IsDead) return;
        if (monsterCurHP <= 0)
        {
            IsDead = true;
            UIManager.INSTANCE.nextStage.gameObject.SetActive(true);
            // UI & Monster Clear
            UIManager.INSTANCE.UIClear();
            UIManager.INSTANCE.DeadMonster();
            UIManager.INSTANCE.RestartButton.gameObject.SetActive(true);
            // 플레이어에게 GiveGold 만큼 전달
            UIManager.INSTANCE.Gold += monsterGiveGold;

        }
        AutoDamage();
    }

    void AutoDamage() 
    {
        // 1초마다 1씩 공격
        time += Time.deltaTime;
        if (time >= 1f)
        {
            rectTransform.localScale = new Vector2(1.0f, 1.0f); // 몬스터 원래 사이즈 
            monsterCurHP -= autoDamage;
            UIManager.INSTANCE.AutoDamageText.text = autoDamage.ToString();
            time = 0f;
        }
    }


    public void CurMonster()
    {
        isNewMonster = false;
        NewMonster(curMonsterNum);
    }
    public void NextMonster()
    {
 
        curMonsterNum += 1;
        isNewMonster = true;
        NewMonster(curMonsterNum);
  
        UIManager.INSTANCE.Monster.interactable = true;
        IsDead = false;
        monsterCurHP = monsterMaxHP;
        //
        time = monsterLimitTime;
        //
        UIManager.INSTANCE.GameOverUI.gameObject.SetActive(false);
        UIManager.INSTANCE.GameClearUI.gameObject.SetActive(false);
        UIManager.INSTANCE.RestartButton.gameObject.SetActive(false);
        UIManager.INSTANCE.prevStage.gameObject.SetActive(true);

        if (curMonsterNum == 0) // 첫번째 몬스터일 때 이전 버튼 막기
        {
            UIManager.INSTANCE.prevStage.gameObject.SetActive(false);
        }

        Debug.Log("BEST" + UIManager.INSTANCE.bestMonsterNum);
        Debug.Log("CUR" + curMonsterNum);

        if ( UIManager.INSTANCE.bestMonsterNum <= curMonsterNum )
        {            
            UIManager.INSTANCE.nextStage.gameObject.SetActive(false);
        }

    }

    public void PrevMonster()
    {
        if (curMonsterNum <= 0)
        {
            curMonsterNum = 0;
            return;
        }
        curMonsterNum -= 1;
        isNewMonster = true;
        NewMonster(curMonsterNum);

        UIManager.INSTANCE.Monster.interactable = true;
        IsDead = false;
        monsterCurHP = monsterMaxHP;
        //
        time = monsterLimitTime;
        //
        UIManager.INSTANCE.GameOverUI.gameObject.SetActive(false);
        UIManager.INSTANCE.GameClearUI.gameObject.SetActive(false);
        UIManager.INSTANCE.RestartButton.gameObject.SetActive(false);
        UIManager.INSTANCE.nextStage.gameObject.SetActive(true);

        if (curMonsterNum == 0)
        {
            UIManager.INSTANCE.prevStage.gameObject.SetActive(false);
        }
    }

    void NewMonster(int num)
    {
        if (num <= 0)
        {
            num = 0;
        }
        if (num >= 4)
        {
            num = 4;
        }
        Debug.Log("num" + num);
        Debug.Log("Monster HP : " + monsterCurHP);

        monsterName = monsterData[num].Name;

        monsterCurHP = monsterData[num].HP;
        monsterMaxHP = monsterCurHP;

        monsterStage = monsterData[num].Stage;

        monsterGiveGold = monsterData[num].GiveGold;

        monsterLimitTime = monsterData[num].LimitTime;

        monsterImage.sprite = monsterData[num].Image;

        UIManager.INSTANCE.time = monsterData[num].LimitTime;
    }

}
