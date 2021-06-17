using UnityEngine;
/// <summary>
/// 这个脚本用于保存得分和目标分，不需要挂载
/// 由于每个关卡的分数的初始值源于上一关，随意单独将得分写一个脚本，并且不会因为重新加载场景而被重置
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
        // 确保场景中只有一个 ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 为了防止场景切换时需要连续播放的音效被重头开始播放，保留之前场景中的 AudioManager
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

    // 进行下一关的参数调整
    public void NextLevelAdjust()
    {    
        targetScore += 1000 + level * 100;
        level++;
    }

    // 重新开始时重置得分和目标分
    public void InitialValue()
    {
        score = 0;
        targetScore = 650;
        level = 1;
    }

    // 得分
    public void Score(int value)
    {
        score += value;
    }
}
