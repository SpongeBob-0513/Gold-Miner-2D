using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public void PauseBtn()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // 让游戏时间暂停
        AudioListener.pause = true; // 声音停止接收
    }
}
