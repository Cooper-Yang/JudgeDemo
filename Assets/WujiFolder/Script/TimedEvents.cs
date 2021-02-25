using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct tEvent{
    public int date;
    public int hour;
    public int min;

    public bool generate;
    public bool trigger;
    
    public GameObject toGenerate;
    public UnityEvent toTrigger;

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
    
    public List<tEvent> events;
    TimeManage T;
    int lastMin=0;
    // Start is called before the first frame update
    void Start()
    {
        T = FindObjectOfType<TimeManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastMin != T.min)
        {
            lastMin = T.min;
            foreach (tEvent te in events)
            {
                if (te.date == T.date && te.hour == T.hour && te.min == T.min)
                {
                    if (te.trigger)
                    {
                        te.toTrigger.Invoke();
                    }

                    if (te.generate)
                    {
                        Instantiate(te.toGenerate,te.position,te.rotation,te.dady);
                    }
                }
            }
        }
        
    }
}


