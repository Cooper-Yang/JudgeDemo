using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public bool Active = false;

    public float delayTimer = 5f;

    private void Awake()
    {
        if (Active)
        {
            StartCoroutine(DestoryAfterTime(delayTimer));
        }
    }

    IEnumerator DestoryAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
