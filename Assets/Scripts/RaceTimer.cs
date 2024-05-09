using System;
using UnityEngine;
using UnityEngine.UI;

public class RaceTimer : MonoBehaviour
{
    public bool raceStarted = false;
    public static float time = 0;
    private TimeSpan timePlaying;
    public Text timerText;

    private void OnEnable()
    {
        GameEvents.OnRaceStart += StartTimer;
        GameEvents.OnRaceStop += StopTimer;
    }

    private void OnDisable()
    {
        GameEvents.OnRaceStart -= StartTimer;
        GameEvents.OnRaceStop -= StopTimer;
    }

    private void StartTimer()
    {
        if (!raceStarted)
        {
            raceStarted = true;
            time = 0;
            InvokeRepeating("UpdateTimer", 0.0f, 0.01f);
        }
    }

    private void StopTimer()
    {
        if (raceStarted)
        {
            raceStarted = false;
            CancelInvoke("UpdateTimer");
        }
    }

    private void UpdateTimer()
    {
        time += 0.01f;
        timePlaying = TimeSpan.FromSeconds(time);
        string timeString = timePlaying.ToString("mm\\:ss\\:ff");
        if (timerText != null)
        {
            timerText.text = timeString;
        }
    }

    // New method to increment the timer by one second
    public void IncrementTime()
    {
        time += 1f;
    }
}