using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Attach This To Any \"Drag\" Object")]
    [Range(0, 0)] public int MustBeChildOfACanvas;

    private Canvas canvas;
    private RectTransform draggableTransform;
    private RectTransform canvasRect;
    private float yOffset = 0;
    private float xOffset = 0;
    private bool isComputerWindow;

    private Vector3 smallSize = new Vector3(1, 1, 1);
    private Vector3 bigSize = new Vector3(1.5f, 1.5f, 1);

    private void Awake()
    {
        if (canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform != null)
            {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if (canvas != null)
                    break;
                testCanvasTransform = testCanvasTransform.parent;
            }
            canvasRect = canvas.GetComponent<RectTransform>();
        }

        if (canvas.CompareTag("Computer"))
            isComputerWindow = true;
        else
            isComputerWindow = false;

        if (draggableTransform == null)
        {
            if (isComputerWindow)
            {
                draggableTransform = this.transform.parent.GetComponent<RectTransform>();
                yOffset = (this.transform.GetComponent<RectTransform>().position - this.transform.parent.GetComponent<RectTransform>().position).y;
            }
            else
            {
                draggableTransform = this.transform.GetComponent<RectTransform>();
                yOffset = 0;
            }
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, data.position, data.pressEventCamera, out globalMousePos))
        {
            xOffset = draggableTransform.position.x - globalMousePos.x;
        }

        if (!isComputerWindow)
        {
            LerpScale(draggableTransform, true);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, data.position, data.pressEventCamera, out globalMousePos))
        {
            Vector3 location = new Vector3(globalMousePos.x + xOffset, globalMousePos.y - yOffset, globalMousePos.z);
            draggableTransform.position = location;
            draggableTransform.rotation = canvasRect.rotation;
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        // END DRAGGING - Assign new canvas if dropped onto different canvas?
        if (!isComputerWindow)
        {
            LerpScale(draggableTransform, false);
        }
    }

    private void LerpScale(RectTransform dragTrans, bool makeBig)
    {
        if(makeBig)
            dragTrans.localScale = Vector3.Lerp(smallSize, bigSize, 1);
        else
            dragTrans.localScale = Vector3.Lerp(bigSize, smallSize, 1);

    }
}
