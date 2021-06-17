using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 管理得分，倒计时 由于每个关卡的分数的初始值源于上一关，因此需要避免因为重新加载场景而被重置
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject failPanel;
    public GameObject successPanel;

    Text scoreText;
    [SerializeField]
    private GameObject scoreGO;

    [SerializeField]
    private float countdown = 60; // 倒计时
    private float initialCountdown;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;         
    }

    private void Start()
    {
        scoreText = scoreGO.GetComponent<Text>();  // 得分内容
        initialCountdown = countdown;
    }

    private void Update()
    {
        UpdateCountdown(); // 更新计时器    
    }


    bool timeRunOutNeedCheck = true, countDownNeedCheck = true;
    // 更新计时器
    void UpdateCountdown()
    {
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // 限制 countdown 大于等于 0  

        if ((int)countdown == 10 && timeRunOutNeedCheck)
        {
            timeRunOutNeedCheck = false;
            FindObjectOfType<AudioManager>().Play("TimeRunOut");
        }

        // 如果倒计时为 0，关卡结束
        if (countdown == 0 && countDownNeedCheck)
        {
            countDownNeedCheck = false;

            // 停止所有音效
            FindObjectOfType<AudioManager>().StopAllAudios();

            // 结束音效
            FindObjectOfType<AudioManager>().Play("End");
            EndLevel();
        }
    }

    // 结束关卡
    void EndLevel()
    {
        if (ScoreManager.GetInstance().GetScore() < ScoreManager.GetInstance().GetTargetScore())
        {
            failPanel.SetActive(true);
        }
        else
        {
            successPanel.SetActive(true);
        }
    }

    // 分数增加
    public void Score(int value)
    {
        // 更新分数
        scoreText.text = "$" + value;

        // 分数显示
        scoreGO.SetActive(true);

        Invoke(nameof(ScoreHide), 1f); // 一秒钟后分数隐藏

        ScoreManager.GetInstance().Score(value);   
    }

    private void ScoreHide()
    {
        scoreGO.SetActive(false);
    }

    // 

    // 获得当前分数
    public string GetScore()
    {
        return "Score: " + ScoreManager.GetInstance().GetScore();
    }

    // 获得当前计时器时间
    public int GetCountdown()
    {
        return (int)countdown;
    }

    // 获得当前关卡的目标分数
    public int GetTargetScore()
    {
        return ScoreManager.GetInstance().GetTargetScore();
    }
}
