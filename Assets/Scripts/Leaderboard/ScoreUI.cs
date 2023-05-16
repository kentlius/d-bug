using UnityEngine;
using System.Linq;
using System;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;
    private TimeSpan savedTime;
    void Start()
    {
        Debug.Log(Finish.isGameFinished);
        if (Finish.isGameFinished) 
        {
            string savedTimeString = PlayerPrefs.GetString("SavedTime"); 
            if (TimeSpan.TryParseExact(savedTimeString, "mm':'ss'.'ff", null, out savedTime))
            {
                scoreManager.AddScore(new Score(NameManager.playerName, savedTime, "16-05-2023"));
            }
        }
        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.time.text = scores[i].time.ToString(@"mm\:ss\.ff");
            row.date.text = scores[i].date;
        }
        Finish.isGameFinished = false;
    }
}



