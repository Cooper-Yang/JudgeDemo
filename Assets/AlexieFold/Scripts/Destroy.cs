using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public EviToRep eviToRep;
    void Start()
    {
        eviToRep = GameObject.Find("NoteManager").GetComponent<EviToRep>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eviToRep.count != eviToRep.choosenList.Count)
            Destroy(gameObject);
    }
}
