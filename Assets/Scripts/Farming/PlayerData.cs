using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeData
{
    public float currentYield;
    public int monthPlanted;
    public int currentYear;
    public int currentMonth;
    public int day;
    public int hour;
    public bool planted;

    public TreeData(soil playerSoil, timeTracking playerTime)
    {
        if (playerSoil != null)
        {
            monthPlanted = playerSoil.monthPlanted;
            planted = playerSoil.planted;
            currentYield = playerSoil.yield;
        }

        if (playerTime != null)
        {
            currentYear = playerTime.getCurrentTime().year;
            currentMonth = playerTime.getCurrentTime().monthNum;
            day = playerTime.getCurrentTime().day;
            hour = playerTime.getCurrentTime().hour;
        }
    }
}