using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application_Password : Application {
        public File TargetFile;
        public string Password;

        public override void Process()
        {
            CmdControl.Main.NewLine("");
            StartCoroutine("ProcessIE");
        }

        public IEnumerator ProcessIE()
        {
            CmdControl.Main.NewLine("Error: Access Restriction");
            yield return LineDelay(1f);
            CmdControl.Main.NewLine("Please enter password:{FieldActive{");
        }

        public override void Execute(string Key)
        {
            if (Key == "Back" || Key == "back")
                StartCoroutine(BackIE());
            else if (Key == Password)
                StartCoroutine(Success());
            else
                StartCoroutine(Failed());
        }

        public IEnumerator Failed()
        {
            CmdControl.Main.NewLine("");
            CmdControl.Main.NewLine("Error: Access Denied");
            yield return LineDelay(1f);
            CmdControl.Main.NewLine("");
            CmdControl.Main.NewLine("Please enter password:{FieldActive{");
        }

        public IEnumerator Success()
        {
            CmdControl.Main.NewLine("");
            CmdControl.Main.NewLine("Access Granted");
            yield return LineDelay(1f);
            CmdControl.Main.NewLine("");
            CmdControl.Main.LoadFile(TargetFile);
        }
    }
}