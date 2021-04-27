using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(disableThis());
    }
    
    IEnumerator disableThis()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
