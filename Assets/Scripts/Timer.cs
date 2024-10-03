using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timertext;
    [SerializeField] private float time;
    private bool timerRun = true;
    private WinCheck win;
    void Start()
    {
        win = FindFirstObjectByType<WinCheck>().GetComponent<WinCheck>();
    }
    void Update()
    {
        if (timerRun)
        {
            time -= Time.deltaTime;

            int seconds = (int) (time % 60) % 60;
            int minutes = (int) time / 60;

            timertext.text = string.Format("{0:0}:{1:00}",minutes,seconds);

            if (seconds == 0)
                StopTimer();
        }
        else
        {
            if (!win.HasWon)
                Debug.Log("Time has run out!");
        }
    }
    public void StopTimer()
    {
        timerRun = false;
    }
}
