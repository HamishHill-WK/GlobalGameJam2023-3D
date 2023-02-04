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

    public Text SpeedText;
    public Text txt;    //legacy text ui element
    private int year = 0;
    private int day = 0;
    private int hour = 0;
    private float minute = 0; // time minutes counts up every frame. 
    public float speedFactor = 1;//the rate at which time passes. Scale goes from 1 to 10.
    private GameObject Slider;

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
        SpeedText.text = "Speed: " + speedFactor;
        timeData timeData2 = getCurrentTime();
        txt.text = "Day: " + timeData2.day.ToString() +  " Month: " + currentMonth.ToString() + " Year: " + timeData2.year.ToString() ;
    }

    public void updateSpeed()
    {
        speedFactor = Slider.GetComponent<Slider>().value;
    }

    private void Start()
    {
        Slider = GameObject.Find("TimeSlider");
        speedFactor = Slider.GetComponent<Slider>().value;
    }

    void Update()
    {
        updateText();

        minute += speedFactor;

        if(minute >= 10){
            hour++;
            minute = 0;
        }

        if(hour == 6){
            day++;
            hour = 0;
        }
                
        if (currentMonth == month.February && day >= 28){
            updateMonth();
            return;
        }

        if (currentMonth != month.February)
        {
            if (day >= 30)
            {
                if (currentMonth == month.April || currentMonth == month.June 
                    || currentMonth == month.September || currentMonth == month.November)
                    updateMonth();
                
                if (day == 31)
                {
                    if( currentMonth == month.December)
                    {
                        year++;
                        currentMonth = 0;
                        day = 0;
                        return;
                    }

                    if (currentMonth == month.January || currentMonth == month.March || currentMonth == month.May 
                        || currentMonth == month.July || currentMonth == month.August || currentMonth == month.October )
                    {
                        updateMonth();
                    }
                }
            }
        }
    }

    private void updateMonth()
    {
        currentMonth++;
        day = 0;
    }
}

