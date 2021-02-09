using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Camera mainCam;
    public float rayLen;

    public NoteManager notManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin,ray.direction * rayLen, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Debug.Log("hit: " + hit.collider);
//                if (hit.collider.tag == "Box")
//                {
//                    Debug.Log("It's a box");
//                }
//                else if (hit.collider.tag == "Sephere")
//                {
//                    Debug.Log("It's a Sephere");
//                }
                if(hit.collider.tag == "NoteBook Trigger")
                {
                    Debug.Log("The Trigger");
                    notManager.inPreview = true;
                }
                else
                {
                    Debug.Log("some others");
                    notManager.inPreview = false;
                }
            }
            else
            {
                Debug.Log("hit nothing");
            }
        }
    }
}
