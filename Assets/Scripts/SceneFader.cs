using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
/// <summary>
/// 用于场景切换的过渡
/// </summary>
public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    private void Start()
    {
        // 协同程序
        StartCoroutine(FadeIn()); // 游戏进入的过渡效果
    }

    // 场景切换的过渡效果   scene 为将要切换为的场景
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void FadeToByIndex(int index)
    {
        StartCoroutine(FadeOutByIndex(index));
    }

    // 游戏进入的过渡效果
    IEnumerator FadeIn()
    {
        float t = 1f;
        
        while(t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);  // 颜色通过循环由黑色逐渐透明度减小 直到为 0
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
            img.color = new Color(0, 0, 0, a);  // 颜色通过循环由透明逐渐透明度增加 直到为 1
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
            img.color = new Color(0, 0, 0, a);  // 颜色通过循环由黑色逐渐透明度减小 直到为 0
            yield return 0;
        }

        SceneManager.LoadScene(index);
    }
}
