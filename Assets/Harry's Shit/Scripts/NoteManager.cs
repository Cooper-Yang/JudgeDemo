using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public bool inPreview;

    public bool inView;//this is used if we want "when u put the cursor on the notebook edge, only a bit shows up"
                        //when u press the notebook, the whole book come out.

    public float timeElapsed;

    public float lerpDuration;

    public bool canLerp;// this is used for an older version of the lerpnig, could be used differently

    public GameObject noteBook;

    public GameObject originPos;

    public GameObject previewPos;

    public GameObject finalPos;

    //public bool isLerping;
    
    private Vector3 originVec;
    private Vector3 previewVec;// not used for now.
    private Vector3 finalVec;
    // Start is called before the first frame update
    void Start()
    {
        originVec = originPos.transform.position;
        previewVec = previewPos.transform.position;
        finalVec = finalPos.transform.position;
        canLerp = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(noteBook.transform.position);
        if (timeElapsed >= 1)
        {
            timeElapsed = 1;
        }
        if (timeElapsed <= 0)
        {
            timeElapsed = 0;
        }

        if (inPreview)
        {
            timeElapsed += 1 * Time.deltaTime;
        }
        else
        {
            timeElapsed -= 1 *Time.deltaTime;
        }
//        if (inPreview)
//        {
        noteBook.transform.position = Vector3.Lerp(originVec, finalVec, easeOutBack(timeElapsed / lerpDuration));
        //timeElapsed += Time.deltaTime;
            //StartCoroutine(Lerp(noteBook, noteBook.transform.position, finalVec));
        //}
//        else
//        {
//            noteBook.transform.position = Vector3.Lerp(noteBook.transform.position, originVec, easeOutBack(timeElapsed / lerpDuration));
//            timeElapsed += Time.deltaTime;
//            //StartCoroutine(Lerp(noteBook, noteBook.transform.position, originVec));
//        }

//        if (timeElapsed >= lerpDuration)
//        {
//            canLerp = true;
//        }
//
//        if (canLerp)
//        {
//            if (Input.GetKeyDown(KeyCode.A))
//            {
//                canLerp = false;
//                StartCoroutine(Lerp(noteBook, originVec, finalVec));
//            }
//            
//            if (Input.GetKeyDown(KeyCode.D))
//            {
//                canLerp = false;
//                StartCoroutine(Lerp(noteBook, finalVec, originVec));
//            }
//        }

    }

    IEnumerator Lerp(GameObject it, Vector3 startValue, Vector3 endValue)
    {
        timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            it.transform.position = Vector3.Lerp(startValue,endValue,easeOutBack(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //canLerp = true;
        it.transform.position = endValue;
        
        
    }
    
    //below i give credit to https://easings.net/#easeOutBack
    public float easeOutBack(float t)
    {
        float f1 = 1.70158f;
        float f2 = f1 + 1;
        return 1 + f2 * Mathf.Pow(t - 1, 3) + f1 * Mathf.Pow(t - 1, 2);
    }
}
