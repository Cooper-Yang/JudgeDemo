using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application_ActualEmail : Application_Folder {
        public string Address;

        public override void Process()
        {
            CmdControl.Main.NewLine("");
            StartCoroutine("ProcessIE");
        }

        public override IEnumerator ProcessIE()
        {
            CmdControl.Main.NewLine("Browsing: " + Address);
            yield return LineDelay(0.5f);

            bool Empty = true;
            foreach (File F in SubFiles)
            {
                Empty = false;
                CmdControl.Main.NewLine("    > " + F.Key + " (" + F.GetComponent<Application_EmailFolder>().SubFiles.Count + ")");
                yield return LineDelay(0.3f);
            }
            if (Empty)
            {
                CmdControl.Main.NewLine("   (No Item)");
                yield return LineDelay(0.3f);
            }

            CmdControl.Main.NewLine("");
            CmdControl.Main.NewLine(">{FieldActive{");
        }
    }
}