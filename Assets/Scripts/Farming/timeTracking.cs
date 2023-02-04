using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script was written by Hamish Hill github: @HamishHill-wk
//this script tracks the passage of in game time.

public class timeTracking : MonoBehaviour
{
    public struct timeData
    {
        public int year;
        public int monthNum;
        public int day;
        public int hour;
    }

    public Text txt;
    private int year = 0;
    private int day = 0;
    private int hour = 0;
    private int minute = 0; // time minutes counts up every frame. 
    public int speedFactor = 1; 
                //^^^
    //the rate at which time passes.
    //Scale goes from 1 to 10.

    enum month { January = 0, February, March, April, May, June, July, August, September, October, November, December };
    month currentMonth = month.January;

    public timeData getCurrentTime()
    {
        timeData timeData1;

        timeData1.year = year;
        timeData1.monthNum = (int)currentMonth;
        timeData1.day = day;
        timeData1.hour = hour;

        return timeData1;
    }

    void updateText()
    {
        timeData timeData2 = getCurrentTime();
        txt.text = "Day: " + timeData2.day.ToString() +  " Month: " + currentMonth.ToString() + " Year: " + timeData2.year.ToString() ;
    }

    public void setRateSlow()
    {
        speedFactor = 1;
    }    
    
    public void setRateMed()
    {
        speedFactor = 5;
    }    
    
    public void setRateFast()
    {
        speedFactor = 10;
    }

    public void setRateZero()
    {
        speedFactor = 0;
    }

    void Start()
    {
        //PlayerData data = new PlayerData(null, null, this);  //load time data from binary file and update variables 

        //year = data.currentYear;
        //currentMonth = (month)data.currentMonth;
        //day = data.day;
        //hour = data.hour;

    }

    void Update()
    {
        updateText();

        minute += speedFactor;

        if(minute >= 10)
        {
            hour++;
            minute = 0;
        }

        if(hour == 6)
        {
            day++;
            hour = 0;
        }
                
        if (currentMonth == month.February)
        {
            if (day >= 28)
            {
                currentMonth++;
                day = 0;
            }
        }

        if (currentMonth != month.February)
        {
            if (day >= 30)
            {
                if (currentMonth == month.April || currentMonth == month.June || currentMonth == month.September || currentMonth == month.November)
                {
                    currentMonth++;
                    day = 0;
                }

                if (day == 31)
                {
                    if( currentMonth == month.December)
                    {
                        currentMonth = 0;
                        year++;
                        day = 0;
                        return;
                    }

                    if (currentMonth == month.January || currentMonth == month.March || currentMonth == month.May || currentMonth == month.July 
                        || currentMonth == month.August || currentMonth == month.October )
                    {
                        currentMonth++;
                        day = 0;
                    }
                }
            }
        }
    }
}
