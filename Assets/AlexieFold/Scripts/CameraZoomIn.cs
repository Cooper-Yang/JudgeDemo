using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    [SerializeField]
    private float zoomInMag;
    [SerializeField]
    private float sensitive;
    [SerializeField]
    private float rotateUpBorder;
    [SerializeField]
    private float rotateDownBorder;
    [SerializeField]
    private float rotateLeftBorder;
    [SerializeField]
    private float rotateRightBorder;

    private Vector2 currentaRotation;
    private bool zoomedIn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ct = Camera.main.transform;
        if (Input.GetMouseButtonDown(1) && !zoomedIn)
        {
            zoomedIn = true;
            ct.position = new Vector3(ct.position.x, ct.position.y, ct.position.z * zoomInMag);
        }
        else if (zoomedIn && Input.GetMouseButtonDown(1))
        {
            zoomedIn = false;
            ct.position = new Vector3(ct.position.x, ct.position.y, ct.position.z / zoomInMag);
            ct.rotation = Quaternion.Euler(0, 0, 0);
            currentaRotation = new Vector2(0, 0);
        }

        if (zoomedIn)
        {
            currentaRotation.x += Input.GetAxis("Mouse X") * sensitive;
            currentaRotation.y -= Input.GetAxis("Mouse Y") * sensitive;
            currentaRotation.y = Mathf.Clamp(currentaRotation.y, rotateUpBorder, rotateDownBorder);
            currentaRotation.x = Mathf.Clamp(currentaRotation.x, rotateLeftBorder, rotateRightBorder);
            ct.rotation = Quaternion.Euler(currentaRotation.y, currentaRotation.x, 0);
        }
    }
}
