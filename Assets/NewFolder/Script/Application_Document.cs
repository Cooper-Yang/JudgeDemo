using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application_Document : Application {
        public string Title;
        public int CurrentPage;
        public int MaxPage;
        public List<Application_Document_Page> Pages;

        public override void Process()
        {
            CurrentPage = 0;
            CmdControl.Main.NewLine("");
            StartCoroutine("ProcessIE");
        }

        public IEnumerator ProcessIE()
        {
            CmdControl.Main.NewLine("Load Document: " + Title);
            yield return LineDelay(1f);
            yield return LoadCurrentPage();
        }

        public IEnumerator LoadCurrentPage()
        {
            CmdControl.Main.NewLine("-----------------------------------------");
            Application_Document_Page P = Pages[CurrentPage];
            foreach (string s in P.Lines)
            {
                CmdControl.Main.NewLine(s);
                yield return LineDelay(0.28f);
            }
            CmdControl.Main.NewLine("-----------------------------------------");
            CmdControl.Main.NewLine("(Page" + (CurrentPage + 1) + "/" + (MaxPage + 1) + "):{FieldActive{");
        }

        public override void Execute(string Key)
        {
            if (Key == "Back" || Key == "back")
                StartCoroutine(BackIE());
            else if (Key == "Next" || Key == "next" || Key == "NextPage")
                NextPage();
            else if (Key == "Previous" || Key == "previous" || Key == "PreviousPage")
                PreviousPage();
            else if (Key == "First" || Key == "first" || Key == "FirstPage")
                FirstPage();
            else if (Key == "Last" || Key == "last" || Key == "LastPage")
                LastPage();
            else if ((Key.Substring(0, 4) == "Page" || Key.Substring(0, 4) == "page") && Key.Length > 4)
            {
                if (int.TryParse(Key.Substring(4), out int a))
                    GoToPage(a - 1);
                else
                    EmptyPage();
            }
            else
                StartCoroutine(EmptyIE(Key));
        }

        public void EmptyPage()
        {
            StartCoroutine(EmptyPageIE());
        }

        public IEnumerator EmptyPageIE()
        {
            CmdControl.Main.NewLine("Error: Page Invalid");
            yield return LineDelay(0.5f);
            CmdControl.Main.NewLine("(Page" + (CurrentPage + 1) + "/" + (MaxPage + 1) + "):{FieldActive{");
        }

        public void NextPage()
        {
            if (CurrentPage + 1 <= MaxPage)
            {
                CurrentPage++;
                StartCoroutine(LoadCurrentPage());
            }
            else
                EmptyPage();
        }

        public void PreviousPage()
        {
            if (CurrentPage - 1 >= 0)
            {
                CurrentPage--;
                StartCoroutine(LoadCurrentPage());
            }
            else
                EmptyPage();
        }

        public void FirstPage()
        {
            CurrentPage = 0;
            StartCoroutine(LoadCurrentPage());
        }

        public void LastPage()
        {
            CurrentPage = MaxPage;
            StartCoroutine(LoadCurrentPage());
        }

        public void GoToPage(int Value)
        {
            if (Value >= 0 && Value <= MaxPage)
            {
                CurrentPage = Value;
                StartCoroutine(LoadCurrentPage());
            }
            else
                EmptyPage();
        }

        public override IEnumerator EmptyIE(string Key)
        {
            yield return base.EmptyIE(Key);
            CmdControl.Main.NewLine("(Page" + (CurrentPage + 1) + "/" + (MaxPage + 1) + "):{FieldActive{");
        }
    }
}