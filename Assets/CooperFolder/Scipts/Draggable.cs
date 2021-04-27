using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

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
        if (!isComputerWindow)
        {
            LerpScale(draggableTransform, false);
        }
        //transform.parent.SetAsFirstSibling();
        GameObject.Find("Submit Area").GetComponent<SubmitArea>().RectOverlaps();
        GameObject.Find("Material Area").GetComponent<MatArea>().RectOverlaps();
        
    }

    private void LerpScale(RectTransform dragTrans, bool makeBig)
    {
        if(makeBig)
            dragTrans.localScale = Vector3.Lerp(smallSize, bigSize, 1);
        else
            dragTrans.localScale = Vector3.Lerp(bigSize, smallSize, 1);

    }

    /*[HideInInspector]
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
    //public Vector3 theOriginScale;
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
            //theOriginScale = draggableTransform.localScale;
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
        this.transform.GetChild(0).gameObject.SetActive(true);
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
            //draggableTransform.localScale = theOriginScale * .1f;
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
        this.transform.SetAsLastSibling();
        Destroy(placeHolder);
        //draggableTransform.localScale = theOriginScale;
    }*/
}
