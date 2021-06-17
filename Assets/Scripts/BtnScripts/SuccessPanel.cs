using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessPanel : MonoBehaviour
{
    public SceneFader sceneFader;

    int randomLevel = 1, lastLevel = 1;

    public void NextLevelBtn()
    {
        ScoreManager.GetInstance().NextLevelAdjust(); // 调整分数

        do
        {
            randomLevel = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        }
        while (randomLevel == lastLevel); // 确保随机生成的关卡数与之前的关卡数不一样
        lastLevel = randomLevel;

        sceneFader.FadeToByIndex(randomLevel);
        
        FindObjectOfType<AudioManager>().Play("BGMusic"); // 播放背景音乐
    }

    public void MenuBtn()
    {
        ScoreManager.GetInstance().InitialValue(); // 重置分数和目标分

        sceneFader.FadeTo("MainMenu");
        FindObjectOfType<AudioManager>().Play("BGMusic"); // 播放背景音乐
    }
}
