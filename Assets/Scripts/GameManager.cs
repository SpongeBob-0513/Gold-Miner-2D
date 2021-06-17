using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ����÷֣�����ʱ ����ÿ���ؿ��ķ����ĳ�ʼֵԴ����һ�أ������Ҫ������Ϊ���¼��س�����������
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
    private float countdown = 60; // ����ʱ
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
        scoreText = scoreGO.GetComponent<Text>();  // �÷�����
        initialCountdown = countdown;
    }

    private void Update()
    {
        UpdateCountdown(); // ���¼�ʱ��    
    }


    bool timeRunOutNeedCheck = true, countDownNeedCheck = true;
    // ���¼�ʱ��
    void UpdateCountdown()
    {
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // ���� countdown ���ڵ��� 0  

        if ((int)countdown == 10 && timeRunOutNeedCheck)
        {
            timeRunOutNeedCheck = false;
            FindObjectOfType<AudioManager>().Play("TimeRunOut");
        }

        // �������ʱΪ 0���ؿ�����
        if (countdown == 0 && countDownNeedCheck)
        {
            countDownNeedCheck = false;

            // ֹͣ������Ч
            FindObjectOfType<AudioManager>().StopAllAudios();

            // ������Ч
            FindObjectOfType<AudioManager>().Play("End");
            EndLevel();
        }
    }

    // �����ؿ�
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

    // ��������
    public void Score(int value)
    {
        // ���·���
        scoreText.text = "$" + value;

        // ������ʾ
        scoreGO.SetActive(true);

        Invoke(nameof(ScoreHide), 1f); // һ���Ӻ��������

        ScoreManager.GetInstance().Score(value);   
    }

    private void ScoreHide()
    {
        scoreGO.SetActive(false);
    }

    // 

    // ��õ�ǰ����
    public string GetScore()
    {
        return "Score: " + ScoreManager.GetInstance().GetScore();
    }

    // ��õ�ǰ��ʱ��ʱ��
    public int GetCountdown()
    {
        return (int)countdown;
    }

    // ��õ�ǰ�ؿ���Ŀ�����
    public int GetTargetScore()
    {
        return ScoreManager.GetInstance().GetTargetScore();
    }
}
