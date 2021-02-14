using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform dragBarTransform;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        if (dragBarTransform == null)
        {
            dragBarTransform = this.transform.parent.GetComponent<RectTransform>();
        }
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
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragBarTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
