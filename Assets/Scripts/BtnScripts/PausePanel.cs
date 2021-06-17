using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    public SceneFader sceneFader;

    public void ContinueBtn()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // ��Ϸʱ��ָ�����
        AudioListener.pause = false; // ���½�������
    }

    public void RestartBtn()
    {
        Time.timeScale = 1f; // ��Ϸʱ��ָ�����

        // ��ֹͣ�����������ٲ��ű�������
        FindObjectOfType<AudioManager>().StopAllAudios();
        FindObjectOfType<AudioManager>().Play("BGMusic");

        ScoreManager.GetInstance().InitialValue(); // ���÷�����Ŀ���
        sceneFader.FadeTo("Level01");
    }

    public void HomeBtn()
    {
        Time.timeScale = 1f; // ��Ϸʱ��ָ�����

        // ��ֹͣ�����������ٲ��ű�������
        FindObjectOfType<AudioManager>().StopAllAudios();
        FindObjectOfType<AudioManager>().Play("BGMusic");

        ScoreManager.GetInstance().InitialValue(); // ���÷�����Ŀ���
        sceneFader.FadeTo("MainMenu");
    }
}
