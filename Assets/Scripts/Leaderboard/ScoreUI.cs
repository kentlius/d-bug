using UnityEngine;
using System.Linq;
using System; 

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;

    void Start()
    {
        // scoreManager.AddScore(new Score("Kent", new TimeSpan(0, 1, 35), "16-05-2023"));
        scoreManager.AddScore(new Score("Kent", new TimeSpan(0, 0, 49), "16-05-2023"));
        scoreManager.AddScore(new Score("Kent", new TimeSpan(0, 2, 22), "16-05-2023"));
        scoreManager.AddScore(new Score("Kent", new TimeSpan(0, 1, 34), "16-05-2023"));
        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.time.text = scores[i].time.ToString(@"mm\:ss\.ff");
            row.date.text = scores[i].date;
        }
    }
}



