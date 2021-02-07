using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            AddLine(C, FieldActive, "");
        }

        public void AddLine(string Text, bool FieldActive, string FieldText)
        {
            LineRenderer LR = Instantiate(LRPrefab, LRBase.transform).GetComponent<LineRenderer>();
            LRs.Add(LR);
            LR.Ini(Text, FieldActive, FieldText);
            UpdateLRPositions();
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

        public void LoadFile(File F)
        {
            CurrentFile = F;
            CurrentApp = F.App;
            if (F.App)
                F.App.Process();
        }
    }
}