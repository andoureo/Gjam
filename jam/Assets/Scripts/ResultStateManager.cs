using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultStateManager : MonoBehaviour
{

    // 時間を表示するテキスト
    public TextMeshProUGUI TimerText;



    /// <summary>
    /// ゲームで経過した時間を表示する
    /// </summary>
    public void SetTimerText(int timer)
    {
        this.TimerText.text = timer.ToString();
    }
}