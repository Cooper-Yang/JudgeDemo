using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimeManage : MonoBehaviour
{
    public GameObject dayButton;
    public TextMeshProUGUI timeText;
    public int date = 1;
    int endDate = 31;
    public int month =11;
    int endMonth = 13;
    public int year = 94;
    public int hour = 0;
    public int startHour = 9;
    public int endHour = 21;
    public int min = 0;
    int endMin = 60;
    public float timeCounter;
    public float timeUnit = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        hour = startHour;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= timeUnit)
        {
            if (hour != endHour)
            {
                min++;
            }
            timeCounter = 0;
        }

        if (min >= endMin)
        {
            hour++;
            min = 0;
        }

        hour = Mathf.Min(hour, endHour);

        dayButton.SetActive(hour == endHour);

        timeText.SetText(month.ToString().PadLeft(2, '0') + "/"+date.ToString().PadLeft(2, '0') + "/"+year+" - "+hour.ToString().PadLeft(2, '0') + ":"+min.ToString().PadLeft(2,'0'));
    }

    public void endDay()
    {
        if (hour == endHour)
        {
            hour = startHour;
            date++;
        }

        if (date >= endDate)
        {
            date = 1;
            month++;
        }

        if (month >= endMonth)
        {
            month = 1;
            year++;
        }
    }
}
