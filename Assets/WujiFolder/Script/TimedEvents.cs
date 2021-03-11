using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct tEvent{
    public bool on;
    //the time the event is triggered
    public int year;
    public int month;
    public int date;
    public int hour;
    public int min;
    //if the event generate something, or triggered a methods
    public bool generate;
    public bool trigger;
    //the game object to generate, and the methods to trigger
    public GameObject toGenerate;
    public UnityEvent toTrigger;
    //the information for generating objects
    public Vector3 position;
    public Quaternion rotation;
    public Transform dady;
}

[System.Serializable]
public struct variousPara
{
    public int interger1;
    public int interger2;
    public int interger3;

    public float float1;
    public float float2;
    public float float3;

    public bool bool1;
    public bool bool2;
    public bool bool3;

    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;

    public Vector3 vector31;
    public Vector3 vector32;
    public Vector3 vector33;
}
public class TimedEvents : MonoBehaviour
{
    //the list that stores all those events
    public List<tEvent> events;
    //link to the timer
    TimeManage T;
    //counter
    int lastMin=0;
    // Start is called before the first frame update
    void Start()
    {
        T = FindObjectOfType<TimeManage>();
    }

    // Update is called once per frame
    void Update()
    {
        //check when min passes
        if (lastMin != T.min)
        {
            lastMin = T.min;
            //check all events
            foreach (tEvent te in events)
            {
                //if time matches
                if (te.year==T.year&&te.month==T.month&&te.date == T.date && te.hour == T.hour && te.min == T.min&&te.on)
                {
                    //trigger the methods
                    if (te.trigger)
                    {
                        te.toTrigger.Invoke();
                    }
                    //generate the object
                    if (te.generate)
                    {
                        Instantiate(te.toGenerate,te.position,te.rotation,te.dady);
                    }
                }
            }
        }
        
    }

    public void switchEvent(int i)
    {
        //using this since cannot change directly
        tEvent temp = events[i];
        temp.on = !temp.on;
        events[i] = temp;
    }

    //the methods for days latter event
    public void triggerEvent_Next(int i, int days)
    {
        tEvent temp = events[i];
        temp.on = true;
        temp.date = T.date + days;
        events[i] = temp;
    }

    //the methods for time waiting event
    public void triggerEvent_Wait(int i, int mins)
    {
        
        tEvent temp = events[i];
        temp.on = true;

        int hours = mins / 60;
        mins = mins - hours * 60;
        int days = hours / 24;
        hours = hours - days * 24;
        int month = days / 30;
        days = days - month * 30;

        temp.min = T.min + mins;
        temp.hour = T.hour + hours;
        temp.date = T.date + days;
        temp.month = T.month + month;

        events[i] = temp;
    }
}


