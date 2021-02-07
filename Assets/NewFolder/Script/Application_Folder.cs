using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application_Folder : Application {
        public List<File> SubFiles;

        public override void Process()
        {
            CmdControl.Main.NewLine("");
            StartCoroutine("ProcessIE");
        }

        public virtual IEnumerator ProcessIE()
        {
            string s = "/" + SourceFile.Key;
            Application_Folder Folder = this;
            while (Folder && Folder.SourceFile.UpFile)
            {
                s = "/" + Folder.SourceFile.UpFile.Key + s;
                Folder = Folder.SourceFile.UpFile.GetComponent<Application_Folder>();
            }
            if (SourceFile.UpFile)
                CmdControl.Main.NewLine("Load Folder: " + s);
            else
                CmdControl.Main.NewLine("Directory: " + s);
            yield return LineDelay(0.5f);

            bool Empty = true;
            foreach (File F in SubFiles)
            {
                Empty = false;
                CmdControl.Main.NewLine("   / [" + F.Type + "] " + F.Key);
                yield return LineDelay(0.3f);
            }
            if (Empty)
            {
                CmdControl.Main.NewLine("   (Empty Folder)");
                yield return LineDelay(0.3f);
            }

            CmdControl.Main.NewLine("");
            CmdControl.Main.NewLine(">{FieldActive{");
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
                foreach (File F in SubFiles)
                {
                    if (F.Key == Key)
                    {
                        CmdControl.Main.LoadFile(F);
                        return;
                    }
                }
            }
            StartCoroutine(EmptyIE(Key));
        }

        public override IEnumerator BackIE()
        {
            if (!SourceFile.UpFile)
            {
                CmdControl.Main.NewLine("Error: Directonary invalid");
                yield return LineDelay(0.3f);
                CmdControl.Main.NewLine("");
                CmdControl.Main.NewLine(">{FieldActive{");
            }
            else
                CmdControl.Main.LoadFile(SourceFile.UpFile);
            yield return 0;
        }

        public override IEnumerator EmptyIE(string Key)
        {
            yield return base.EmptyIE(Key);
            CmdControl.Main.NewLine(">{FieldActive{");
        }
    }
}