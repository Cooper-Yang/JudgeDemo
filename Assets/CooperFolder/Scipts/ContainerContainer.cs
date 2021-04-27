using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> _dropZones = new List<GameObject>();
    private void Start()
    {
        foreach (GameObject container in GameObject.FindGameObjectsWithTag("Container"))
        {
            _dropZones.Add(container);
        }
    }

    public void ClearAllEvidence()
    {
        foreach (GameObject _drop in _dropZones)
        {
            if (_drop.transform.childCount>0)
            {
                Destroy(_drop.transform.GetChild(0).gameObject);
                //_drop.GetComponent<DropZone>().evidenceKeyword.text = "";
                _drop.transform.parent.parent.parent.GetComponent<CrimialEvidence>().theEvidenceContained.Clear();
            }
        }
    }
}
