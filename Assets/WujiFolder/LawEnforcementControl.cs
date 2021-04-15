using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawEnforcementControl : MonoBehaviour
{
    public GameObject[] Panels = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePanel(int count)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i == count)
            {
                if (!Panels[i].activeSelf)
                    Panels[i].SetActive(true);
            }
            else
            {
                if (Panels[i].activeSelf)
                    Panels[i].SetActive(false);
            }
        }
    }
}
