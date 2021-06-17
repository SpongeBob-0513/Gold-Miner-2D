using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]  // 使得自定义的类在 inspector 里面被看见
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume; // 音量
    [Range(.1f, 3f)]
    public float pitch; // 音调

    public bool loop;

    public bool playOnAwake;

    [HideInInspector]  // 通过 Audio Manager 来设置，不需要在面板中显示
    public AudioSource source;
}
