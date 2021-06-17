using UnityEngine;
/// <summary>
/// ����ű����ڱ���÷ֺ�Ŀ��֣�����Ҫ����
/// ����ÿ���ؿ��ķ����ĳ�ʼֵԴ����һ�أ����ⵥ�����÷�дһ���ű������Ҳ�����Ϊ���¼��س�����������
/// </summary>
public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;    
    
    [SerializeField]
    private int score = 0, targetScore = 650, level = 1;

    public static ScoreManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        // ȷ��������ֻ��һ�� ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ϊ�˷�ֹ�����л�ʱ��Ҫ�������ŵ���Ч����ͷ��ʼ���ţ�����֮ǰ�����е� AudioManager
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetTargetScore()
    {
        return targetScore;
    }

    // ������һ�صĲ�������
    public void NextLevelAdjust()
    {    
        targetScore += 1000 + level * 100;
        level++;
    }

    // ���¿�ʼʱ���õ÷ֺ�Ŀ���
    public void InitialValue()
    {
        score = 0;
        targetScore = 650;
        level = 1;
    }

    // �÷�
    public void Score(int value)
    {
        score += value;
    }
}
