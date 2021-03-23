using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class File : MonoBehaviour
{
    public enum FileType { User, Folder, Document, Photo, Audio, Email };

    [SerializeField] private string FileName;
    [SerializeField] private FileType Type;
    [SerializeField] private File UpFile;

    public string GetFileType()
    {
        return Type.ToString();
    }

    public virtual void Open()
    {
        // OPEN THE FILE
    }
}


