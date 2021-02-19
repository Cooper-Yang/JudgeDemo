using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
[CustomEditor (typeof(EviToRep))]
[CanEditMultipleObjects]
public class EviToRepEditor : Editor
{
    Dictionary<string, string> evidRepoEditor;

    public override void OnInspectorGUI()
    {
        evidRepoEditor = new Dictionary<string, string>();
        EviToRep eviToRep = (EviToRep)target;
        evidRepoEditor = eviToRep.evidRepo;
        if(evidRepoEditor != null)
        {
            foreach(KeyValuePair<string,string> kvp in evidRepoEditor)
            {
                EditorGUILayout.TextField(kvp.Key, kvp.Value);
            }
        }
        DrawDefaultInspector();
    }
}*/
