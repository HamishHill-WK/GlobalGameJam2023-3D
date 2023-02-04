using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeData
{
    public float currentYield;
    public int currentYear;
    public int currentMonth;
    public int day;
    public int hour;

    public TreeData(timeTracking playerTime)
    {
        if (playerTime != null)
        {
            currentYear = playerTime.getCurrentTime().year;
            currentMonth = playerTime.getCurrentTime().monthNum;
            day = playerTime.getCurrentTime().day;
            hour = playerTime.getCurrentTime().hour;
        }
    }
}