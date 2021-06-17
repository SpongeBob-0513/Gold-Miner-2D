using UnityEngine;
using UnityEngine.UI;

public class Muted : MonoBehaviour
{
    //--------------- �������� --------------------------------------------------

    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

    private void Start()
    {
        // �����Ϸ��ʼʱû�г�ʼ�� �Ƿ�������Ĭ��Ϊ������
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void MutedBtn()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }

        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    // ����Ҷ��Ƿ���������ͨ�� PlayerPrefs ��������������ֻ֧�� Float Int String      ����Ҫ�� Bool ת��Ϊ Int ��������
    private void Load()  // ��ȡ muted
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save() // ���� muted
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
