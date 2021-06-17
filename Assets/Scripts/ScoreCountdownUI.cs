using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ����� �÷� ����ʱ�ĸ���
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
