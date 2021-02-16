using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EviToRep : MonoBehaviour
{
    [Header("Alignment")]
    //Range under 1, otherwise too large
    public float topSpace;//Space left at the top of the page
    public float leftSpace;//Space left at the left of the page
    public float lineSpace;//This is normal line space
    
    [Header("Notebook")]
    public List<string> clueList;//List that contains clues.
    public List<string> choosenList;//List that is choosen to report.
    public TextMeshProUGUI Clue;
    public TextMeshProUGUI Repo;
    public int count = 0;
    
    [Header("Test Only")]
    public GameObject book;
    public Dictionary<string, string> evidRepo;
    public GameObject[] arrayofRepo;
    void Start()
    {
        count = 0;
        for(int i = 0; i < clueList.Count; i++)
        {
            //Don't delete these
            TextMeshProUGUI clueText = Instantiate(Clue, new Vector3(book.transform.position.x - book.transform.localScale.x / 2 + leftSpace, 
                                        book.transform.position.y + book.transform.localScale.y / 2 - Clue.transform.localScale.y / 2  
                                        - i * (Clue.transform.localScale.y + lineSpace) - topSpace, 0),
                                        Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            clueText.text = clueList[i];
        }
        //Test Dictionary
        evidRepo = new Dictionary<string, string>();
        evidRepo.Add("Bread", "He ate bread this morning. Well, nothing special.");
        evidRepo.Add("Fish", "Fishing is fun. I can't judge him by that.");
        evidRepo.Add("Killed Tom", "Fianally, He admitted killing Tom. Country owns everything!");
    }

    
    void Update()
    {
        if (count != choosenList.Count)
        {
            arrayofRepo = GameObject.FindGameObjectsWithTag("Repo");
            for(int i = 0; i < arrayofRepo.Length; i++)
            {
                Destroy(arrayofRepo[i].gameObject);
            }
            for (int i = 0; i < choosenList.Count; i++)
            {
                TextMeshProUGUI repoText = Instantiate(Repo, new Vector3(book.transform.position.x + leftSpace,
                                           book.transform.position.y + book.transform.localScale.y / 2
                                           - i * (Repo.transform.localScale.y + lineSpace) - topSpace, 0),
                                           Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                repoText.text = evidRepo[choosenList[i]];
                Debug.Log("Instantiate");
            }
            count = choosenList.Count;
        }
    }
}
