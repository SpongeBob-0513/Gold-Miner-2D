using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
/// <summary>
/// ���ڳ����л��Ĺ���
/// </summary>
public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    private void Start()
    {
        // Эͬ����
        StartCoroutine(FadeIn()); // ��Ϸ����Ĺ���Ч��
    }

    // �����л��Ĺ���Ч��   scene Ϊ��Ҫ�л�Ϊ�ĳ���
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void FadeToByIndex(int index)
    {
        StartCoroutine(FadeOutByIndex(index));
    }

    // ��Ϸ����Ĺ���Ч��
    IEnumerator FadeIn()
    {
        float t = 1f;
        
        while(t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);  // ��ɫͨ��ѭ���ɺ�ɫ��͸���ȼ�С ֱ��Ϊ 0
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);  // ��ɫͨ��ѭ����͸����͸�������� ֱ��Ϊ 1
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

    IEnumerator FadeOutByIndex(int index)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);  // ��ɫͨ��ѭ���ɺ�ɫ��͸���ȼ�С ֱ��Ϊ 0
            yield return 0;
        }

        SceneManager.LoadScene(index);
    }
}
