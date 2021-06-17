using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 面板上 得分 倒计时的更新
/// </summary>
public class ScoreCountdownUI : MonoBehaviour
{
    public Text scoreText;
    public Text countdownText;
    public Text targetText;

    private void Start()
    {
        targetText.text = "Score: " + GameManager.GetInstance().GetTargetScore();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.GetInstance().GetScore();
        countdownText.text = GameManager.GetInstance().GetCountdown().ToString();
    }
}
