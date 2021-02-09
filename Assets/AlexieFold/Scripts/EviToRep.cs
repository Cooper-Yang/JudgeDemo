using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EviToRep : MonoBehaviour
{
    [Header("NoteSpace")]
    //Range under 1, otherwise too large
    public float topSpace;//Space left at the top of the page
    public float leftSpace;//Space left at the left of the page
    public float lineSpace;//This is normal line space

    public List<string> clueList;
    public Text Clue;
    public GameObject book;
    void Start()
    {
        for(int i = 0; i < clueList.Count; i++)
        {
            Text clueText = Instantiate(Clue, new Vector3(book.transform.position.x - book.transform.localScale.x / 2 + leftSpace, 
                                        book.transform.position.y + book.transform.localScale.y / 2 - Clue.transform.localScale.y / 2  
                                        - i * (Clue.transform.localScale.y + lineSpace) - topSpace, 0),
                                        Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            clueText.text = clueList[i];
        }
        
    }

    
    void Update()
    {
        
    }
}
