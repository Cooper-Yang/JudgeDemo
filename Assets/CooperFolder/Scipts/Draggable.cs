using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler ,IPointerDownHandler{

    [HideInInspector]
    public Transform parentToReturnTo = null;
    public Transform parentToReturnToCompare = null;
    public Vector2 originalAnchor;
    public Transform originalParent;
    [HideInInspector]
    public Transform placeHolderParent = null;

    private GameObject placeHolder = null;

    private GameObject theClone;
    
    
    private RectTransform draggableTransform;
    private RectTransform canvasRect;
    private float yOffset = 0;
    private float xOffset = 0;
    private bool isComputerWindow;
    
    
    
    [SerializeField] private Canvas canvas;




    private void Awake()
    {
        if (GetComponent<LayoutElement>() == null)
        {
            LayoutElement lE = gameObject.AddComponent<LayoutElement>();
            lE.preferredWidth = 100;
            lE.preferredHeight = 100;
            lE.flexibleHeight = 0;
            lE.flexibleWidth = 0;
            lE.layoutPriority = 1;
        }

        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
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
        }
        originalAnchor = draggableTransform.anchoredPosition;
        originalParent = this.transform.parent;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // RectTransform t = dragTransform;
        // while (t.parent != null)
        // {
        //     if (t.parent.CompareTag(" "))
        //     {
        //         break;
        //     }
        //     t = t.parent.GetComponent<RectTransform>();
        // }
        
        //this for the set as last sibling 
        
        
        draggableTransform.SetAsLastSibling();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        theClone = Instantiate(gameObject, transform.position, Quaternion.identity,canvas.transform);
        Destroy(theClone.GetComponent<Draggable>());
        Destroy(theClone.GetComponent<LayoutElement>());
        Destroy(theClone.GetComponent<CanvasGroup>());
        Debug.Log("instante!");
        placeHolder = new GameObject();
        placeHolder.transform.SetParent( this.transform.parent );
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex() );

        parentToReturnTo = this.transform.parent;
        
        parentToReturnToCompare = this.transform.parent;
        
        placeHolderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        draggableTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
        //this.transform.position = eventData.position;
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);

        if (placeHolder.transform.parent != placeHolderParent)
        {
            placeHolder.transform.SetParent(placeHolderParent);
        }
        
        int newSiblingIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (this.transform.localPosition.x < placeHolderParent.GetChild(i).position.x)
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
        if (parentToReturnTo == parentToReturnToCompare || parentToReturnTo.CompareTag("Container") || parentToReturnTo.CompareTag("TrashContainer") )
        {
            Destroy(theClone);
        }

        if (!parentToReturnTo.CompareTag("Container") )
        {
            draggableTransform.anchoredPosition = originalAnchor;
            
        }
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
    }
}
