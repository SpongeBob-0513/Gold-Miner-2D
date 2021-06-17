using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    public SceneFader sceneFader;

    public void ContinueBtn()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // 游戏时间恢复正常
        AudioListener.pause = false; // 重新接收声音
    }

    public void RestartBtn()
    {
        Time.timeScale = 1f; // 游戏时间恢复正常

        // 先停止所有声音，再播放背景音乐
        FindObjectOfType<AudioManager>().StopAllAudios();
        FindObjectOfType<AudioManager>().Play("BGMusic");

        ScoreManager.GetInstance().InitialValue(); // 重置分数和目标分
        sceneFader.FadeTo("Level01");
    }

    public void HomeBtn()
    {
        Time.timeScale = 1f; // 游戏时间恢复正常

        // 先停止所有声音，再播放背景音乐
        FindObjectOfType<AudioManager>().StopAllAudios();
        FindObjectOfType<AudioManager>().Play("BGMusic");

        ScoreManager.GetInstance().InitialValue(); // 重置分数和目标分
        sceneFader.FadeTo("MainMenu");
    }
}
