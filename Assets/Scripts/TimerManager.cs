using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{

    // 時間表示
    public TextMeshProUGUI TimerText;

    /// <summary>
    /// タイマーのテキストの設定
    /// </summary>
    public void SetText(int time)
    {

        this.TimerText.text = "Time : " + time;
    }
}