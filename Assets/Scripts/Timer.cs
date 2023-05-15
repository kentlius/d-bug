using System;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private Stopwatch stopwatch;
    public TimeSpan ElapsedTime { get; private set; }
    public TextMeshProUGUI timeText;

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

        if (PauseMenu.GameIsPaused)
        {
            stopwatch.Stop();
        }
        else
        {
            stopwatch.Start();
        }
        ElapsedTime = stopwatch.Elapsed;
        timeText.text = ElapsedTime.ToString(@"mm\:ss\.ff");
    }
}
