using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuspectList : MonoBehaviour
{
    private static SuspectList _instance;
    public List<GameObject> susList;
    public TMP_Dropdown dropDown;
    [SerializeField]
    TMP_Text label;
    // Start is called before the first frame update
    public static SuspectList Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SuspectList>();
            }

            return _instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < transform.childCount; i++)
        {
            susList.Add(transform.GetChild(i).gameObject);
        }
        RefreshDropDown();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshDropDown();
    }

    void RefreshDropDown()
    {
        dropDown.options.Clear();
        for (int i = 0; i < susList.Count; i++)
        {
            TMP_Dropdown.OptionData da = new TMP_Dropdown.OptionData(susList[i].name);
            dropDown.options.Add(da);
        }
    }

    public void AddSuspect(GameObject toAdd)
    {
        susList.Add(toAdd);
        RefreshDropDown();
    }

    public void RemoveSuspect(GameObject toRemove)
    {

        dropDown.value = 1;

        susList.Remove(toRemove);
        //Destroy(toRemove);
        toRemove.GetComponent<CrimialEvidence>().clear();

        RefreshDropDown();
        dropDown.value = 0;
        if (dropDown.options.Count == 0)
        {
            label.text = "";
        }
    }
}
