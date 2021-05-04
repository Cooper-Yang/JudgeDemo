using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public SubmitArea sub;
    public MatArea mat;
    // Start is called before the first frame update
    void Start()
    {
        sub = FindObjectOfType<SubmitArea>();
        mat = FindObjectOfType<MatArea>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PrintDocument>()&& collision.gameObject.GetComponent<Draggable>())
        {
            if (collision.gameObject.transform.localScale.x<=1)
            {
                //SoundMan.me.EvidenceDropSound(Vector3.zero);
                mat.inArea.Remove(collision.GetComponent<RectTransform>());
                sub.inArea.Remove(collision.GetComponent<RectTransform>());
                Destroy(collision.gameObject);
            }

        }
    }

}
