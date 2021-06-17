using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]  // ʹ���Զ�������� inspector ���汻����
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume; // ����
    [Range(.1f, 3f)]
    public float pitch; // ����

    public bool loop;

    public bool playOnAwake;

    [HideInInspector]  // ͨ�� Audio Manager �����ã�����Ҫ���������ʾ
    public AudioSource source;
}
