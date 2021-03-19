using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    [SerializeField]
    private Camera m_Orthographic;
    [SerializeField]
    private float zoomInMag;

    private Vector3 tempPos;
    private bool zoomedIn;

    private void Start()
    {
        m_Orthographic = Camera.main;
    }
    void Update()
    {
        //var ct = Camera.main.transform;
        if (Input.GetMouseButtonDown(1) && !zoomedIn)
        {
            tempPos = Camera.main.transform.position;
            Vector3 mousePos = Input.mousePosition;
            Vector3 transferredMousePos = m_Orthographic.ScreenToWorldPoint(mousePos);
            m_Orthographic.orthographicSize = m_Orthographic.orthographicSize * zoomInMag;
            m_Orthographic.transform.position = new Vector3(transferredMousePos.x, transferredMousePos.y, m_Orthographic.transform.position.z);
            zoomedIn = true;
        }
        else if (zoomedIn && Input.GetMouseButtonUp(1))
        {
            zoomedIn = false;
            m_Orthographic.orthographicSize = m_Orthographic.orthographicSize / zoomInMag;
            m_Orthographic.transform.position = tempPos;
        }
    }
}
