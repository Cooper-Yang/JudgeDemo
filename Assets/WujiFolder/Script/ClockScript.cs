using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    TimeManage T;
    public GameObject pointerMin;
    public GameObject pointerHour;
    float maxMin = 60;
    float maxHour = 12;
    // Start is called before the first frame update
    void Start()
    {
        T = FindObjectOfType<TimeManage>();
    }

    // Update is called once per frame
    void Update()
    {
        pointerMin.transform.eulerAngles = new Vector3(0, 0, -(T.min / maxMin) * 360);
        pointerHour.transform.eulerAngles = new Vector3(0, 0, -(T.hour / maxHour) * 360);
    }
}
