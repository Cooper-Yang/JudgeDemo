using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crimes : MonoBehaviour
{
    [SerializeField]
    List<CrimeBlock> blocks;
    [SerializeField]
    Vector3 iniPosition;
    [SerializeField]
    float difference;//the distance between each
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(CrimeBlock block in blocks)
        {
            block.transform.SetParent(this.transform);
        }
        for(int i = 0; i < blocks.Count; i++)
        {
            if (i < 3)
            {
                blocks[i].gameObject.SetActive(true);
                blocks[i].gameObject.GetComponent<RectTransform>().localPosition = iniPosition - new Vector3(0, i * difference, 0);
            }
            else
            {
                blocks[i].gameObject.SetActive(false);
            }
        }
    }

    public void GoUp()
    {
        //print("dodo");
        CrimeBlock temp = blocks[0];
        
        for(int i = 0; i < blocks.Count - 1; i++)
        {
            blocks[i] = blocks[i + 1];
        }
        blocks[blocks.Count - 1] = temp;
    }

    public void GoDown()
    {
        CrimeBlock temp = blocks[blocks.Count-1];

        for (int i = blocks.Count-1; i > 0; i--)
        {
            blocks[i] = blocks[i - 1];
        }
        blocks[0] = temp;
    }
}
