using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    public TextAsset Charmander;

    Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        {"dark",    "012456"}, // Background
        {"red",     "D24753"}, // Alert
        {"green",   "13A10E"}, // Success
        {"yellow",  "C19C00"}, // Response
        {"blue",    "0041FF"}, // System - working
        {"purple",  "AD1DC1"}, // System - title
        {"light",   "3A96DD"}, // Selection
        {"white",   "CCCCCC"}  // Normal
    };


    // ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ 
    TerminalControl control;
    private void Awake()
    {
        control = this.GetComponent<TerminalControl>();
    }

    TerminalControl.Directory previousDirectory = TerminalControl.Directory.Home;
    public void SetPreviousDirectory(TerminalControl.Directory dir)
    {
        previousDirectory = dir;
    }

    TerminalControl.Location currentLocation = TerminalControl.Location.None;


    List<string> response = new List<string>();

    public List<string> Interpret(string userInput, TerminalControl.Directory currentDir)
    {
        response.Clear();

        string[] args = userInput.Split(); // by default, split string by "" spaces

        string inputCommand = args[0].ToLower(); // make string lowercase to interpret

        switch (inputCommand)
        {
            case "list":
                switch (control.GetCurrentDirectory())
                {
                    case TerminalControl.Directory.Home:
                        Output("** System: Home ************", colors["purple"]);
                        Output("Type \"Help\" for a list of commands.", colors["light"]);
                        Output("Type \"Access\" to access a directory.", colors["light"]);
                        Output("****************************", colors["purple"]);
                        break;

                    case TerminalControl.Directory.Archives:
                        Output("** Investigation Archives **", colors["purple"]);
                        Output("Not implemented yet,", colors["red"]);
                        Output("But can be used to house any kind of case info", colors["red"]);
                        Output("****************************", colors["purple"]);
                        break;

                    case TerminalControl.Directory.Customs:
                        Output("*Customs & Border Protection", colors["purple"]);
                        OutputSplitLine("Location", "Select Security Outpost Location", colors["yellow"], colors["light"]);
                        OutputSplitLine("Request", "Border Receipts from selected location", colors["yellow"], colors["light"]);
                        Output("****************************", colors["purple"]);
                        break;
                                                                          
                    case TerminalControl.Directory.Personnel:
                        Output("** Personnel Database ******", colors["purple"]);
                        OutputSplitLine("Search", "Look-up Employee in Database", colors["yellow"], colors["light"]);
                        OutputSplitLine("Key", "Generate Skeleton Key", colors["yellow"], colors["light"]);
                        Output("****************************", colors["purple"]);
                        break;

                    case TerminalControl.Directory.Secret:
                        Output("** You found the Secret! ***", colors["purple"]);
                        Output(" :O  :O  PogChamp  :O  :O", colors["red"]);
                        Output("****************************", colors["purple"]);
                        break;

                    default:
                        OutputSplitLine("/DIR:/", " ", " Unrecognized Directory", colors["white"], colors["red"]);
                        Output("Access a Directory or type \"Home\"", colors["red"]);
                        break;
                }
                return response;

            case "info":
                WriteDirectoryInfo(control.GetCurrentDirectory());
                return response;

            case "help":
                Output("** Basic Terminal Commands ****", colors["purple"]);
                OutputSplitLine("Info", "Description of current directory.");
                OutputSplitLine("List", "Lists all tasks in current directory.");
                OutputSplitLine("Directory", "Lists all accessible directories.");
                OutputSplitLine("Access [directory]", "Access a directory.");
                OutputSplitLine("Print", "Prints the current screen.");
                OutputSplitLine("Back", "Return to the previous directory.");
                OutputSplitLine("Home", "Return to original \"Home\" directory.");
                OutputSplitLine("Help", "Lists all terminal commands.");
                OutputSplitLine("Clear", "Clear the terminal, return to home directory.");
                Output("*******************************", colors["purple"]);

                return response;

            case "access": // ACCESS: Keyword --> Access a Directory
                if (args.Length > 1)
                {
                    switch (args[1].ToString()) // DIRECTORIES ACCESSABLE BY KEYWORD
                    {
                        case "personnel":
                            StartCoroutine(AccessDirectory(TerminalControl.Directory.Personnel));
                            break;

                        case "customs":
                            StartCoroutine(AccessDirectory(TerminalControl.Directory.Customs));
                            break;

                        case "archives":
                            StartCoroutine(AccessDirectory(TerminalControl.Directory.Archives));
                            break;

                        case "secret":
                            StartCoroutine(AccessDirectory(TerminalControl.Directory.Secret));
                            break;

                        default:
                            Output("'" + args[1] + "' " + "is not a valid directory.", colors["red"]);
                            break;
                    }
                }
                else
                {
                    ListDirectories(); // --> list all directories if arg[1] 'string directory' is not specified
                }
                return response;

            case "directory":
                ListDirectories();
                return response;

            case "charmander": // test load file text --> ascii
                LoadTitle("charmander", colors["red"], 1);
                return response;

            case "back":
                StartCoroutine(BackDirectory(previousDirectory));
                return response;

            case "home":
                StartCoroutine(BackDirectory(TerminalControl.Directory.Home));
                return response;

            case "print": // TODO: - maybe a command, maybe a button on window next to 'X' 
                response.Add("<color=#E0E300>Printing Screen...</color>");
                control.Print();
                return response;

            case "clear":
                control.ClearTerminal();
                Output("</dir = H:> Terminal Cleared/:", colors["blue"]);
                return response;

            // The above cases are Always accessable no matter the directory.
            // If the input is none of those, check if it is a keyword within each directory.
            default:
                // CHECK FOR CURRENT DIRECTORY ACTIONS
                switch (currentDir)
                {
                    case TerminalControl.Directory.DIR_ACCESS:
                        switch (inputCommand)
                        {
                            case "1":
                                StartCoroutine(AccessDirectory(TerminalControl.Directory.Personnel));
                                break;
                            case "2":
                                StartCoroutine(AccessDirectory(TerminalControl.Directory.Customs));
                                break;
                            case "3":
                                StartCoroutine(AccessDirectory(TerminalControl.Directory.Archives));
                                break;
                            default:
                                control.ChangeDirectory(previousDirectory);
                                Output("Invalid. To access a directory,", colors["red"]);
                                Output("  type \"Access\" followed by a Directory.", colors["red"]);
                                break;
                        }
                        break;

                    case TerminalControl.Directory.Personnel:
                        switch (inputCommand)
                        {
                            case "search":
                                control.ChangeDirectory(TerminalControl.Directory.EMP_SEARCH);
                                control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#C19C00><Employee Look-Up>:</color>");
                                Output("Type \"All\" for complete list of government employees.", colors["yellow"]);
                                Output("Or enter \"Key\" followed by a known government ID.", colors["yellow"]);
                                break;

                            case "key":
                                control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#C19C00><:Generating Skeleton Key:</color>");
                                Output("Generated ONE TIME USE Login Key for email system.", colors["yellow"]);
                                // TODO:
                                string serialKey = control.GenerateKey();
                                OutputSplitLine("Key", ": ", serialKey, colors["yellow"], colors["green"]);
                                break;

                            default:
                                Output("Invalid. Type \"List\" for commands in <Personnel>", colors["red"]);
                                Output("Type \"Back\" to return to Previous Directory./", colors["yellow"]);
                                break;
                        }
                        break;

                    case TerminalControl.Directory.EMP_SEARCH:
                        switch (inputCommand)
                        {
                            case "all":
                                string[] employeeList = TextAssetToStringArray(ReadTextAssetFromResources("EmployeeList")); // Split full txt file into string[] by line
                                //Read each line and combine into one string and print foreach (line) (Ex: "Chirico, Nick -- Management -- ncc314@nyu.edu")

                                foreach (string e in employeeList)
                                {
                                    string output = "";
                                    string[] eInfo = e.Split(',');
                                    foreach (string s in eInfo)
                                    {
                                        output += (s + ", ");
                                    }
                                    //output = eInfo[0] + ", " + eInfo[1] + " -- " + eInfo[2] + " -- " + eInfo[3];
                                    Output(output, colors["light"]);
                                }
                                break;

                            case "key":
                                // compare args[1] to list of ID's
                                // If match, return corresponding Employee Report
                                // GenerateEmployee(string lastname, string firstname, string section, string contact, string ID?)
                                // generate random ID?
                                Output("Not implemented yet", colors["purple"]);
                                break;

                            case "test":
                                Output("This is a test", colors["green"]);
                                break;

                            default:
                                Output("Invalid. Enter a valid command, or", colors["red"]);
                                Output("type \"Back\" to return to Personnel Directory.", colors["red"]);
                                break;
                        }
                        break;

                    case TerminalControl.Directory.Customs:
                        switch (inputCommand)
                        {
                            case "location":
                                control.ChangeDirectory(TerminalControl.Directory.LOC_SELECT);
                                control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#C19C00><Customs Border Security Locations>:</color>");
                                OutputSplitLine("1", " - ", "Water Bridge", colors["yellow"], colors["light"]);
                                OutputSplitLine("2", " - ", "Main Terminal", colors["yellow"], colors["light"]);
                                OutputSplitLine("3", " - ", "Zeng Outpost", colors["yellow"], colors["light"]);
                                break;

                            case "request":
                                // If location is selected
                                // If followed by "dates" or "passports"
                                // Show dates or Show passports
                                // else: "Must specify dates or passports"
                                // else: "Must select a location"

                                int index;
                                if (args.Length > 1)
                                {
                                    switch (args[1])
                                    {
                                        case "history":
                                            index = 1;
                                            break;
                                        case "passports":
                                            index = 2;
                                            break;
                                        default:
                                            index = -1;
                                            Output("   Type \"request history\" for history of entires.", colors["yellow"]);
                                            Output("Or Type \"request passports\" for list of passports.", colors["yellow"]);
                                            break;
                                    }
                                }
                                else
                                {
                                    Output("   Type \"request history\" for history of entires.", colors["yellow"]);
                                    Output("Or Type \"request passports\" for list of passports.", colors["yellow"]);
                                    break;
                                }

                                switch (currentLocation)
                                {
                                    case TerminalControl.Location.WaterBridge:
                                        if (index == 1)
                                            OutputTextFile("Location1Report", colors["light"]);
                                        else if (index == 2)
                                        {
                                            int numPassports = control.OpenPassportWindows(Resources.LoadAll("Location1Passports", typeof(Sprite)));
                                            if(numPassports > 0)
                                                Output("Opening "+numPassports+" Passport Photos from Water Bridge Database:", colors["purple"]);
                                            else
                                                Output("There are no passports available from Water Bridge.", colors["purple"]);
                                        }
                                        break;

                                    case TerminalControl.Location.MainTerminal:
                                        if (index == 1)
                                            OutputTextFile("Location2Report", colors["light"]);
                                        else if (index == 2)
                                        {
                                            int numPassports = control.OpenPassportWindows(Resources.LoadAll("Location2Passports", typeof(Sprite)));
                                            if (numPassports > 0)
                                                Output("Opening " + numPassports + " Passport Photos from Main Terminal Database:", colors["purple"]);
                                            else
                                                Output("There are no passports available from Main Terminal.", colors["purple"]);
                                        }
                                        break;

                                    case TerminalControl.Location.ZengOutpost:
                                        if (index == 1)
                                            OutputTextFile("Location3Report", colors["light"]);
                                        else if (index == 2)
                                        {
                                            int numPassports = control.OpenPassportWindows(Resources.LoadAll("Location3Passports", typeof(Sprite)));
                                            if (numPassports > 0)
                                                Output("Opening " + numPassports + " Passport Photos from Zeng Outpost Database:", colors["purple"]);
                                            else
                                                Output("There are no passports available from Zeng Outpost.", colors["purple"]);
                                        }
                                        break;

                                    case TerminalControl.Location.None:
                                        Output("No Location Set. Type \"Location\" to select one.", colors["yellow"]);
                                        break;

                                    default:
                                        Output("Inspecting location: \"" + currentLocation.ToString() + "\"", colors["yellow"]);
                                        break;
                                }
                                break;

                            default:
                                OutputSplitLine("Invalid.", " ", "\"List\" for commands in <Customs>", colors["red"], colors["yellow"]);
                                break;
                        }
                        break;

                    case TerminalControl.Directory.LOC_SELECT:
                        switch (inputCommand)
                        {
                            case "1":
                                currentLocation = control.SetGetLocation(TerminalControl.Location.WaterBridge);
                                OutputSplitLine("Location Selected", "Water Bridge", colors["green"], colors["yellow"]);
                                StartCoroutine(BackDirectory(previousDirectory));
                                break;
                            case "2":
                                currentLocation = control.SetGetLocation(TerminalControl.Location.MainTerminal);
                                OutputSplitLine("Location Selected", "Main Terminal", colors["green"], colors["yellow"]);
                                StartCoroutine(BackDirectory(previousDirectory));
                                break;
                            case "3":
                                currentLocation = control.SetGetLocation(TerminalControl.Location.ZengOutpost);
                                OutputSplitLine("Location Selected", "Zeng Outpost", colors["green"], colors["yellow"]);
                                StartCoroutine(BackDirectory(previousDirectory));
                                break;
                            default:
                                Output("Invalid. Enter a valid selection.", colors["red"]);
                                Output("Or type \"Back\" to return to Customs Directory.", colors["red"]);
                                break;
                        }
                        break;

                    default:
                        Output("'" + inputCommand + "' " + "is not a recognized command.", colors["red"]);
                        Output("Type \"help\" for a list of terminal commands.", colors["yellow"]);
                        Output("  or \"list\" for the active directory's contents.", colors["yellow"]);
                        break;

                }
                return response;
        }
    }

    // Interpreter Helper Functions - below
    private void ListDirectories()
    {
        control.ChangeDirectory(TerminalControl.Directory.DIR_ACCESS);
        OutputSplitLine(">#<", "", " Select Directory to Access:", colors["yellow"], colors["purple"]);
        OutputSplitLine(" 1", " - ", "Employee Personnel Database", colors["yellow"], colors["light"]);
        OutputSplitLine(" 2", " - ", "Customs and Border Protection", colors["yellow"], colors["light"]);
        OutputSplitLine(" 3", " - ", "Investigation Archives", colors["yellow"], colors["light"]);
    }

    private void WriteDirectoryInfo(TerminalControl.Directory dir)
    {
        switch (dir)
        {
            case TerminalControl.Directory.Home:
                OutputSplitLine("Home", "\\: ", "Main navigation directory.", colors["yellow"], colors["white"]);
                break;

            case TerminalControl.Directory.Archives:
                OutputSplitLine("Archives", "\\: ", "Database of archived investigations.", colors["yellow"], colors["white"]);
                break;

            case TerminalControl.Directory.Customs:
                OutputSplitLine("Customs", "\\: ", "Border protection and customs reference.", colors["yellow"], colors["white"]);
                break;

            case TerminalControl.Directory.Personnel:
                OutputSplitLine("Personnel", "\\: ", "Database of employees and contracts.", colors["yellow"], colors["white"]);
                break;

            case TerminalControl.Directory.Secret:
                OutputSplitLine("Secret", "\\: ", "Hidden directory! Secret info here.", colors["yellow"], colors["white"]);
                break;

            default:
                // Output
                break;
        }
    }

    private IEnumerator AccessDirectory(TerminalControl.Directory newDir)
    {
        control.ChangeDirectory(TerminalControl.Directory.PROCESS);
        control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#0041FF>Accessing.</color>");
        yield return new WaitForSeconds(0.5f);
        control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#0041FF>Accessing..</color>");
        yield return new WaitForSeconds(0.5f);
        control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#0041FF>Accessing...</color>");
        yield return new WaitForSeconds(1f);
        control.ChangeDirectory(newDir);
        control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#13A10E>Directory Accessed <" + newDir.ToString() + ">: </color>");
    }

    private IEnumerator BackDirectory(TerminalControl.Directory newDir)
    {
        control.ChangeDirectory(TerminalControl.Directory.PROCESS);
        control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#0041FF>Returning...</color>");
        yield return new WaitForSeconds(0.75f);
        control.ChangeDirectory(newDir);
        control.AddDirectoryLine(control.GetCurrentDirectory(), "<color=#13A10E>Directory Accessed <" + newDir.ToString() + ">: </color>");
    }

    public string ColorString(string s, string colorCode)
    {
        string leftTag = "<color=#" + colorCode + ">";
        string rightTag = "</color>";
        return leftTag + s + rightTag;
    }

    // ** OUTPUT TEXT LINE FUNCTIONS **
    private void Output(string s)
    {// OUTPUT: ("Sample Text") default color - white
        response.Add(ColorString(s, colors["white"]));
    }

    private void Output(string s, string colorCode)
    {// OUTPUT: ("Sample Text") param (colorCode)
        response.Add(ColorString(s, colorCode));
    }

    private void OutputSplitLine(string item, string description)
    {// OUTPUT: ("Item : description") default colors
        response.Add(ColorString(item, colors["yellow"]) + " : " + ColorString(description, colors["light"]));
    }

    private void OutputSplitLine(string item, string description, string colorCodeA, string colorCodeB)
    {// OUTPUT: ("ItemA : descriptionB") param (codeA, codeB)
        response.Add(ColorString(item, colorCodeA) + " : " + ColorString(description, colorCodeB));
    }

    private void OutputSplitLine(string item, string divider, string description, string colorCodeA, string colorCodeB)
    {// OUTPUT: ("ItemA --> descriptionB") param (codeA, codeB, "-->")
        response.Add(ColorString(item, colorCodeA) + divider + ColorString(description, colorCodeB));
    }

    /*private void OutputList(string s[], string colors )
    {// OUTPUT: One line of param color (s, colors["red"]);
        response.Add(ColorString(a, colors["orange"]) + " : " + ColorString(b, colors["white"]));
    }*/

    // LOAD: File seperated by new line - Ascii Title
    void LoadTitle(string fileName, string colorCode, int topPadding)
    {
        for (int i = 0; i < topPadding; i++)
        {// Add a line of spacing for topPadding - before print title
            response.Add("");
        }

        // TODO: temporarily hard coded to Public TextAsset Charmander --> make read file path
        string[] lines = TextAssetToStringArray(Charmander);
        foreach (string line in lines)
        {
            Output(line, colorCode);
        }

        // temp:::
        switch (fileName)
        {
            case "pogchamp":
                break;

            default:
                break;
        }
        
    }

    private void OutputTextFile(string filename, string colorCode)
    {
        string[] lines = TextAssetToStringArray(ReadTextAssetFromResources(filename));
        foreach (string line in lines)
        {
            Output(line, colorCode);
        }
    }

    private TextAsset ReadTextAssetFromResources(string filename)
    {
        TextAsset file = (TextAsset)Resources.Load("TextAssets/" + filename) as TextAsset;
        return file;
    }

    // Converts a unity TextAsset (txt file) into a string[] 
    private string[] TextAssetToStringArray(TextAsset file)
    {
        if (file != null)
        {
            string fulltext = file.text;
            string[] lines = file.text.Split(new[] { Environment.NewLine },
            StringSplitOptions.None);
            return lines;
        }
        else
        {
            return new string[] { "Failure to load text file", "fileName error" };
        }
    }
}
