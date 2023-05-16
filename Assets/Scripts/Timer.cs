using System;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private Stopwatch stopwatch;
    public TimeSpan ElapsedTime { get; private set; }
    public TextMeshProUGUI timeText;
    private TimeSpan savedTime;



    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    void Update()
    {
        if (PlayerHealth.GameIsOver)
        {
            stopwatch.Stop();
        }
        if (Finish.isGameFinished)
        {
            stopwatch.Stop();
            PlayerPrefs.SetString("SavedTime", ElapsedTime.ToString(@"mm\:ss\.ff"));
        }
        if (PauseMenu.GameIsPaused)
        {
            stopwatch.Stop();
        }
        else if (!Finish.isGameFinished && !PlayerHealth.GameIsOver)
        {
            stopwatch.Start();
        }
        ElapsedTime = stopwatch.Elapsed;
        timeText.text = ElapsedTime.ToString(@"mm\:ss\.ff");
    }
}
