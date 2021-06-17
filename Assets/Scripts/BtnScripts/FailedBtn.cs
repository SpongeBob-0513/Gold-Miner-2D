using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedBtn : MonoBehaviour
{
    public SceneFader sceneFader;

    public void TryAgainBtn()
    {
        ScoreManager.GetInstance().InitialValue(); // ���÷�����Ŀ���

        sceneFader.FadeTo("Level01");
        FindObjectOfType<AudioManager>().Play("BGMusic"); // ���ű�������
    }

    public void MenuBtn()
    {
        sceneFader.FadeTo("MainMenu");
        FindObjectOfType<AudioManager>().Play("BGMusic"); // ���ű�������
    }
}
