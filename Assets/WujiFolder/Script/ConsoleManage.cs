using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManage : MonoBehaviour
{
    public GameObject gamePrototype;
    public MiniGame theGame;
    public GameObject theResult;

    public float reponseTime=1;
    // Start is called before the first frame update
    void Start()
    {
        theGame = Instantiate(gamePrototype,transform.position,transform.rotation,this.gameObject.transform).GetComponent<MiniGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theGame!=null&&theGame.gameOver)
        {
            StartCoroutine( TheEnd());
        }
    }

    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(reponseTime);
        if (theGame.win)
        {
            
            Destroy(theGame.gameObject);
            theGame = null;
            Instantiate(theResult, transform.position, transform.rotation, this.gameObject.transform);
        }
        else
        {
            Destroy(theGame.gameObject);
            theGame = Instantiate(gamePrototype, transform.position, transform.rotation, this.gameObject.transform).GetComponent<MiniGame>();
        }
    }
}
