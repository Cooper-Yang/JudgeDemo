using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application_Email : Application {
        public List<Application_ActualEmail> Emails;

        public override void Process()
        {
            CmdControl.Main.NewLine("");
            StartCoroutine("ProcessIE");
        }

        public IEnumerator ProcessIE()
        {
            CmdControl.Main.NewLine("Initializing: Email Reader");
            yield return LineDelay(1f);
            CmdControl.Main.NewLine("Please enter address:{FieldActive{");
        }

        public override void Execute(string Key)
        {
            if (Key == "Back" || Key == "back")
            {
                StartCoroutine(BackIE());
                return;
            }
            else
            {
                foreach (Application_ActualEmail E in Emails)
                {
                    if (E.Address == Key)
                    {
                        CmdControl.Main.LoadFile(E.SourceFile);
                        return;
                    }
                }
            }
            StartCoroutine(EmptyIE(Key));
        }

        public override IEnumerator EmptyIE(string Key)
        {
            CmdControl.Main.NewLine("Error: Invalid address");
            yield return LineDelay(0.3f);
            CmdControl.Main.NewLine("");
            yield return base.EmptyIE(Key);
            CmdControl.Main.NewLine("Please enter address:{FieldActive{");
        }
    }
}