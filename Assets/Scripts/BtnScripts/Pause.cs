using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public void PauseBtn()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // ����Ϸʱ����ͣ
        AudioListener.pause = true; // ����ֹͣ����
    }
}
