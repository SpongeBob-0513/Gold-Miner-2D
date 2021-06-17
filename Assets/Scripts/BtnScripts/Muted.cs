using UnityEngine;
using UnityEngine.UI;

public class Muted : MonoBehaviour
{
    //--------------- 静音设置 --------------------------------------------------

    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

    private void Start()
    {
        // 如果游戏开始时没有初始化 是否静音，则默认为不静音
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

    // 把玩家对是否静音的设置通过 PlayerPrefs 储存起来，由于只支持 Float Int String      所以要将 Bool 转换为 Int 储存起来
    private void Load()  // 获取 muted
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save() // 保存 muted
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
