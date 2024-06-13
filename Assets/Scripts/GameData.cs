using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    // Removed the static keyword
    public int completedRaces = 0;
    public List<float> raceTimes = new List<float>();

    [SerializeField]
    private Text completedRacesText;
    [SerializeField]
    private Text raceTimesText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        AssignUIElements();
        UpdateUI();
    }

    public void AddRaceTime(float raceTime)
    {
        completedRaces++;
        raceTimes.Add(raceTime);
        UpdateUI();
    }

    public static GameData Instance
    {
        get { return instance; }
    }

    private void UpdateUI()
{
    if (completedRacesText != null)
    {
        completedRacesText.text = "Completed Races: " + completedRaces;
    }

    if (raceTimesText != null)
    {
        // Sort the race times in ascending order
        raceTimes.Sort();

        // Take the top 10 times
        int topTimesCount = Math.Min(10, raceTimes.Count);
        List<float> topTimes = raceTimes.GetRange(0, topTimesCount);

        raceTimesText.text = "Top 10 Race Times:\n";
        for (int i = 0; i < topTimesCount; i++)
        {
            float time = topTimes[i];
            TimeSpan timePlaying = TimeSpan.FromSeconds(time);
            string timeString = timePlaying.ToString("mm\\:ss\\:ff");
            raceTimesText.text += $"{i + 1}. {timeString}\n";
        }
    }
}

    public void SetUIElements(Text completedRacesText, Text raceTimesText)
    {
        this.completedRacesText = completedRacesText;
        this.raceTimesText = raceTimesText;
        UpdateUI();
    }

    private void AssignUIElements()
    {
        if (completedRacesText == null)
        {
            completedRacesText = GameObject.Find("CompletedRacesText")?.GetComponent<Text>();
        }

        if (raceTimesText == null)
        {
            raceTimesText = GameObject.Find("RaceTimesText")?.GetComponent<Text>();
        }
    }
}