using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultStateManager : MonoBehaviour
{

    // 時間を表示するテキスト
    public TextMeshProUGUI TimerText;

    public TextMeshProUGUI ScoreText;
    /// <summary>
    /// ゲームで経過した時間を表示する
    /// </summary>
    public void SetTimerText(int timer)
    {
        this.TimerText.text = "Time: " + timer.ToString();

    }
    public void setscoretext(int score)
    {
        this.ScoreText.text = "Score: " + score.ToString();
    }
}