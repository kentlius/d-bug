using System;
using UnityEngine; 

public class Score
{
    public string name; 
    public TimeSpan time;
    public string date;

    public Score(string name, TimeSpan time, string date)
    {
        this.name = name;
        this.time = time;
        this.date = date;
    }


}
