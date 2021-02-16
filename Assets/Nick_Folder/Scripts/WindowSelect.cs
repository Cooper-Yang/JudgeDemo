using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowSelect : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RectTransform WINDOW;

    public void OnPointerDown(PointerEventData eventData)
    {
        WINDOW.SetAsLastSibling();
    }
}
