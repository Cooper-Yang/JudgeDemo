using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cmd
{
    public class CmdControl : MonoBehaviour {
        public static CmdControl Main;
        public GameObject LRBase;
        public GameObject LRPrefab;
        public int MaxLine;
        public List<Vector2> LRPositions;
        [Space]
        public List<LineRenderer> LRs;
        public LineRenderer ActiveLR;
        [Space]
        public Image ImageRenderer;
        public FadeControl PixelFade;
        [Space]
        public File StartFile;
        public File CurrentFile;
        public Application CurrentApp;

        public void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (StartFile)
                LoadFile(StartFile);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnInput(string Key)
        {
            if (CurrentApp)
                CurrentApp.Execute(Key);
        }

        public void Execute(string Key)
        {
            if (CurrentApp)
                CurrentApp.Execute(Key);
        }

        public void NewLine(string Text)
        {
            NewLine(Text, -1);
        }

        public void NewLine(string Text, int Index)
        {
            string C = "";
            string S = Text;
            bool FieldActive = false;
            while (S.IndexOf("{") != -1)
            {
                C += S.Substring(0, S.IndexOf("{"));
                S = S.Substring(S.IndexOf("{") + 1);
                if (S.IndexOf("{") == -1)
                    break;
                string Key = S.Substring(0, S.IndexOf("{"));
                S = S.Substring(S.IndexOf("{") + 1);
                if (Key == "FieldActive")
                {
                    C += "";
                    FieldActive = true;
                }
                else
                    Execute(Key);
            }
            C += S;
            if (Index == -1)
                AddLine(C, FieldActive, "");
            else
                AddLine(C, FieldActive, "", Index);
        }

        public void AddLine(string Text, bool FieldActive, string FieldText)
        {
            LineRenderer LR = Instantiate(LRPrefab, LRBase.transform).GetComponent<LineRenderer>();
            LRs.Add(LR);
            LR.Ini(Text, FieldActive, FieldText);
            UpdateLRPositions();
        }

        public void AddLine(string Text, bool FieldActive, string FieldText, int Index)
        {
            while (LRs.Count < Index)
                AddLine("", false, "");
            AddLine(Text, FieldActive, FieldText);
        }

        public void UpdateLRPositions()
        {
            if (LRs.Count <= MaxLine)
            {
                for (int i = 0; i < LRs.Count; i++)
                    LRs[i].transform.localPosition = LRPositions[i];
            }
            else
            {
                for (int i = 1; i <= LRs.Count; i++)
                {
                    if (i <= MaxLine)
                        LRs[LRs.Count - i].transform.localPosition = LRPositions[LRPositions.Count - i];
                    else
                        LRs[LRs.Count - i].transform.localPosition = new Vector3(9990, 9990, 0);
                }
            }
        }

        public void ClearLines()
        {
            for (int i = LRs.Count - 1; i >= 0; i--)
            {
                LRs[i].Death();
                LRs.RemoveAt(i);
            }
        }

        public void LoadFile(File F)
        {
            CurrentFile = F;
            CurrentApp = F.App;
            if (F.App)
                F.App.Process();
        }
    }
}