using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [HideInInspector]
    public Transform parentToReturnTo = null;
    [HideInInspector]
    public Transform placeHolderParent = null;

    private GameObject placeHolder = null;

    private Canvas canvas;
    private RectTransform draggableTransform;
    private RectTransform canvasRect;
    private float yOffset = 0;
    private float xOffset = 0;
    private bool isComputerWindow;
    
    //for return
    private Vector3 theOrigin;
    public Transform theOriginParent;
    private void Awake()
    {
        if (GetComponent<LayoutElement>() == null)
        {
            LayoutElement lE = gameObject.AddComponent<LayoutElement>();
            lE.preferredHeight = 100f;
            lE.preferredWidth = 100f;
            lE.flexibleHeight = 0;
            lE.flexibleWidth = 0;
        }

        if (GetComponent<CanvasGroup>() == null)
        {
            CanvasGroup cG = gameObject.AddComponent<CanvasGroup>();
            cG.alpha = 1;
            cG.interactable = true;
            cG.blocksRaycasts = true;
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

            theOrigin = draggableTransform.position;
            theOriginParent = this.transform.parent;
        }
    }

    public void GetBack()
    {
        draggableTransform.position = theOrigin;

        parentToReturnTo = theOriginParent;
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
    }
    
    public void OnBeginDrag(PointerEventData eventData) {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            xOffset = draggableTransform.position.x - globalMousePos.x;
        }
        
        
        
        
        placeHolder = new GameObject();
        placeHolder.transform.SetParent( this.transform.parent );
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex() );

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        //this.transform.SetParent(this.transform.parent.parent);

        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            Vector3 location = new Vector3(globalMousePos.x + xOffset, globalMousePos.y - yOffset, globalMousePos.z);
            draggableTransform.position = location;
            draggableTransform.rotation = canvasRect.rotation;
        }
        
        
        
        
        //this.transform.position = eventData.position;

        if (placeHolder.transform.parent != placeHolderParent)
        {
            placeHolder.transform.SetParent(placeHolderParent);
        }
        
        int newSiblingIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                
                break;
            }
        }

        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData) {
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
    }
}
