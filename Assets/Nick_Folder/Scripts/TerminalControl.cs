using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TerminalControl : MonoBehaviour
{
    public enum Directory { Home, Personnel, Customs, Archives, Secret, PROCESS, DIR_ACCESS, EMP_SEARCH, LOC_SELECT};
    public enum Location { None, WaterBridge, MainTerminal, ZengOutpost };

    public GameObject directoryLine;
    public GameObject responseLine;
    public GameObject userInputLine;
    public GameObject userInputLineDir;

    public TMP_InputField terminalInput;
    public ScrollRect scroll;
    public GameObject msgList;

    public float lineHeight = 20;
    public float scrollVelocity = 450;

    [SerializeField] private Directory currentDirectory;
    Interpreter interpreter;
    EmailManager emailManager;

    float charPixelWidth = 6.15f; // for size 18 font. MANUALLY CHANGE FOR FONT SIZE
    float dirPixelOffset = 14f; // for size 18 font. Offset after string's lenght is accounted for

    public FileWindow NewWindowPrefab;
    public GameObject ComputerPanel;

    public Location CustomsLocation;

    private void Start()
    {
        interpreter = this.GetComponent<Interpreter>();
        emailManager = this.GetComponent<EmailManager>();

    }


    private void OnGUI()
    {
        if (terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            SoundMan.me.ConsoleSound();
            
            // store whatever the user entered
            string userInput = terminalInput.text;

            ClearInputField();
            AddDirectoryLine(currentDirectory, userInput);

            // add interpreter lines
            int lines = AddInterpreterLines(interpreter.Interpret(userInput, currentDirectory));
            ScrollToBottom(lines);

            // move user input line back to bottom
            MoveCursorDown();

            // refocus the input field automatically
            terminalInput.ActivateInputField();
            terminalInput.Select();
            
        }
    }

    private void ClearInputField()
    {
        terminalInput.text = "";
    }

    public void MoveCursorDown()
    {
        userInputLine.GetComponentInChildren<TextMeshProUGUI>().text = DirectoryToString(currentDirectory);
        userInputLineDir.GetComponent<RectTransform>().sizeDelta = new Vector2(DirectoryToString(currentDirectory).Length * charPixelWidth + dirPixelOffset, 20);
        userInputLine.transform.SetAsLastSibling();
    }

    public void AddDirectoryLine(Directory dir, string userInput)
    {
        // resizing command line container so scroll rect works properly
        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + lineHeight);

        // instantiate directory line prefab
        GameObject msg = Instantiate(directoryLine, msgList.transform);
        msg.transform.SetAsLastSibling();
        msg.GetComponentsInChildren<RectTransform>()[1].sizeDelta = new Vector2(DirectoryToString(currentDirectory).Length * charPixelWidth + dirPixelOffset, 20);
        msg.GetComponentsInChildren<TextMeshProUGUI>()[0].text = DirectoryToString(dir);
        msg.GetComponentsInChildren<TextMeshProUGUI>()[1].text = userInput;
        MoveCursorDown();
        ScrollToBottom(1);
    }

    public int AddInterpreterLines(List<string> interpretation)
    {
        for (int i = 0; i < interpretation.Count; i++)
        {
            // instantiate response line
            GameObject res = Instantiate(responseLine, msgList.transform);
            res.transform.SetAsLastSibling();

            // resize message list
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + lineHeight);

            res.GetComponentInChildren<TextMeshProUGUI>().text = interpretation[i];
        }

        return interpretation.Count;
    }

    private void AddInputLine()
    {
        //GameObject line = Instantiate(InputLine, msgList.transform)
    }

    private void ScrollToBottom(int numLines)
    {
        if (numLines > 5)
        {
            scroll.velocity = new Vector2(0, scrollVelocity * 1.5f);
        }
        else
        {
            scroll.velocity = new Vector2(0, scrollVelocity);
        }
    }

    private string DirectoryToString(Directory dir)
    {
        string name = dir.ToString();
        string leftTag = "H:\\";
        string rightTag = ">:";

        return leftTag+name+rightTag;
    }

    public void ChangeDirectory(Directory newDirectory)
    {
        if(currentDirectory != Directory.DIR_ACCESS && currentDirectory != Directory.PROCESS && currentDirectory != Directory.EMP_SEARCH && currentDirectory != Directory.LOC_SELECT)
            interpreter.SetPreviousDirectory(currentDirectory);

        currentDirectory = newDirectory;
    }

    public Directory GetCurrentDirectory()
    {
        return currentDirectory;
    }

    public void ClearTerminal()
    {
        // Destory each line in the Terminal, except the input line
        Destroyer[] linesToRemove = msgList.GetComponentsInChildren<Destroyer>();
        foreach (Destroyer line in linesToRemove)
        {
            Destroy(line.gameObject);
        }

        // Set directory to home and add a directory line
        ChangeDirectory(Directory.Home);

        // Resize the winsow
        Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, lineHeight);
    }

    public Location SetGetLocation(Location loc)
    {
        CustomsLocation = loc;
        return CustomsLocation;
    }

    public string GenerateKey()
    {
        System.Random rand = new System.Random();
        string key = string.Format("{0:X5}", rand.Next(0x100000)); // = "#A197B9"
        emailManager.SetSkeletonKey(key);
        return key;
    }

    public int OpenPassportWindows(object[] array)
    {
        Sprite[] sprites = new Sprite[array.Length];
        for (int x = 0; x < array.Length; x++)
        {
            sprites[x] = (Sprite)array[x];
        }

        StartCoroutine(StaggerCreatePassportWindows(sprites, 0.2f));

        return array.Length;
    }

    IEnumerator StaggerCreatePassportWindows(Sprite[] sprites, float delay)
    {
        int offset = 32;
        int i = 0;
        foreach (Sprite s in sprites)
        {
            FileWindow newWindow = Instantiate(NewWindowPrefab, ComputerPanel.transform);
            newWindow.LoadContents(s, s.name);
            newWindow.Stagger(i, offset);
            newWindow.transform.SetAsLastSibling();
            i++;
            yield return new WaitForSeconds(delay);
        }
    }

    public void Print()
    {
        Destroyer[] allLines = msgList.GetComponentsInChildren<Destroyer>();
        List<Destroyer> linesToPrint = new List<Destroyer>();
        List<string> lines = new List<string>();

        int size = 13;
        if (allLines.Length < 13)
            size = allLines.Length;

        for (int i = 1; i < size; i++)
        {
            linesToPrint.Add(allLines[allLines.Length - i]);
        }

        foreach (Destroyer line in linesToPrint)
        {
            string realString;
            if (line.directoryText != null)
            {
                realString = line.directoryText.text + " " + line.realText.text;
            }
            else
            {
                realString = line.realText.text;
            }

            lines.Add(realString);
        }

        Printer printControl = this.GetComponent<Printer>();
        string[] arr = lines.ToArray();
        Array.Reverse(arr);
        printControl.Print(arr);
    }
}
