using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CrimeBlock : MonoBehaviour
{
    public GameObject toPrint;
    public TMP_Text date;
    public TMP_Text location;
    public TMP_Text content1;
    public TMP_Text content2;
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void print()
    {
        FindObjectOfType<Printer>().Print(this);
    }
}
