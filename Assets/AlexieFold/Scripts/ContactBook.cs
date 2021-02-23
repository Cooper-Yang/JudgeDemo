using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContactBook : MonoBehaviour
{
    [System.Serializable]
    //I don't know if serializable is needed for class, lazy to delete, maybe later.
    //This is the contact class.
    public class Contact
    {
        public string name;
        public string phone;
        public string email;
    }
    //some testing stuff
    private Contact temp;
    private List<string> contacter;
    private Dictionary<string, string> contactDic;
    //Contacter part
    [Header("Contacter")]
    public List<Contact> contactList;
    public GameObject book;

    //textPool part
    public TextMeshProUGUI textPrefab;
    private TextMeshProUGUI[] textPool;

    [Header("Alignment")]
    //Range under 1, otherwise too large
    public float topSpace;//Space left at the top of the page
    public float leftSpace;//Space left at the left of the page
    public float lineSpace;//This is normal line space
    void Start()
    {
        //This is the text pool we'll use. For the sake of garbage collection.
        textPool = new TextMeshProUGUI[20];
        for(int i = 0; i < 20; i++)
        {
            TextMeshProUGUI textbar = Instantiate(textPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
            textPool[i] = textbar;
            textbar.gameObject.SetActive(false);
        }

        //This is the Contacter list we are using. Add contacter and printing them as shown.
        //There's one problem that is you add null after adding a full info, the null will replace the full info.
        AddContacter("Ivory", null, null);
        AddContacter("Ivory", "Iemail", "Iphone");
        //AddContacter("Ivory", null, null);
        AddContacter("Papaya", "Pemail", "Pphone");
        AddContacter("Andy", "AndyEmail", "AndyPhone");

        //This is the printing part. Will be changed.
        for (int i = 0;i < contactList.Count; i++)
        {
            textPool[i].transform.position = new Vector3(book.transform.position.x - book.transform.localScale.x / 2 + leftSpace,
                                           book.transform.position.y + book.transform.localScale.y / 2
                                           - i * (textPrefab.transform.localScale.y + lineSpace) - topSpace, 0);
            textPool[i].text = contactList[i].name + "      " + contactList[i].email + "      " + contactList[i].phone;
            textPool[i].gameObject.SetActive(true);
        }
    }


    void Update()
    {
        
    }

    //This is Class version
    public void AddContacter(string name, string email, string phone)
    {
        Contact someone = new Contact();
        someone.name = name;
        someone.email = email;
        someone.phone = phone;
        SearchContact(someone);
        contactList.Add(someone);
    }
    //This is Search and then add part. While you already know the name
    public void SearchContact(Contact someone)
    {
        for (int i = 0; i < contactList.Count; i++)
        {
            if (contactList[i].name == someone.name)
            {
                contactList.Remove(contactList[i]);
            }
        }
        for (int i = 0; i < contactList.Count; i++)
        {
            if (contactList[i].email == someone.email)
            {
                contactList.Remove(contactList[i]);
            }
        }
        for (int i = 0; i < contactList.Count; i++)
        {
            if (contactList[i].phone == someone.phone)
            {
                contactList.Remove(contactList[i]);
            }
        }
    }
    //This is dictionary version
    /*
    public void AddContacter(string name, string email)
    {
        temp.name = name;
        temp.email = email;
        if(temp.name != null)
        {
            if(contactDic.ContainsKey(temp.name) == false)
            {
                contactDic.Add(temp.name, temp.email);
            }
            else
            {
                contactDic[temp.name] = temp.email;
            }
            contacter.Add(temp.name);
        }

    }
    public void clearTemp()
    {
        temp.name = null;
        temp.phone = null;
        temp.email = null;
    }
    */
}
