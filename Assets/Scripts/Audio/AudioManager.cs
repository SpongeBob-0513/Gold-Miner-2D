using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        // 确保场景中只有一个 AudioManager
        if(instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 为了防止场景切换时需要连续播放的音效被重头开始播放，保留之前场景中的 AudioManager
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 将 Audio Manaer 上的添加的 sound 属性添加到创建的 AudioSource 组件上
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!"); // 检查是不是 name 写错了
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!"); // 检查是不是 name 写错了
            return;
        }
        s.source.Stop();
    }

    // 停止所有声音
    public void StopAllAudios()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
}
