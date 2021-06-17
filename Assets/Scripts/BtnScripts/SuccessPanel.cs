using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessPanel : MonoBehaviour
{
    public SceneFader sceneFader;

    int randomLevel = 1, lastLevel = 1;

    public void NextLevelBtn()
    {
        ScoreManager.GetInstance().NextLevelAdjust(); // ��������

        do
        {
            randomLevel = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        }
        while (randomLevel == lastLevel); // ȷ��������ɵĹؿ�����֮ǰ�Ĺؿ�����һ��
        lastLevel = randomLevel;

        sceneFader.FadeToByIndex(randomLevel);
        
        FindObjectOfType<AudioManager>().Play("BGMusic"); // ���ű�������
    }

    public void MenuBtn()
    {
        ScoreManager.GetInstance().InitialValue(); // ���÷�����Ŀ���

        sceneFader.FadeTo("MainMenu");
        FindObjectOfType<AudioManager>().Play("BGMusic"); // ���ű�������
    }
}
