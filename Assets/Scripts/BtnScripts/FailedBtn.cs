using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedBtn : MonoBehaviour
{
    public SceneFader sceneFader;

    public void TryAgainBtn()
    {
        ScoreManager.GetInstance().InitialValue(); // 重置分数和目标分

        sceneFader.FadeTo("Level01");
        FindObjectOfType<AudioManager>().Play("BGMusic"); // 播放背景音乐
    }

    public void MenuBtn()
    {
        sceneFader.FadeTo("MainMenu");
        FindObjectOfType<AudioManager>().Play("BGMusic"); // 播放背景音乐
    }
}
