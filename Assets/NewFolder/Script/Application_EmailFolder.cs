using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application_EmailFolder : Application_Folder {

        public override IEnumerator ProcessIE()
        {
            CmdControl.Main.NewLine("Browsing: " + SourceFile.Key + " (" + GetComponentInParent<Application_ActualEmail>().Address + ")");
            yield return LineDelay(0.5f);

            bool Empty = true;
            foreach (File F in SubFiles)
            {
                Empty = false;
                CmdControl.Main.NewLine("    > " + F.Key);
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