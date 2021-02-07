using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application : MonoBehaviour {
        [HideInInspector] public File SourceFile;

        public virtual void Awake()
        {
            SourceFile = GetComponent<File>();
        }

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual void Process()
        {

        }

        public virtual void Execute(string Key)
        {

        }

        public IEnumerator LineDelay(float Value)
        {
            float a = 0;
            while (a < Value)
            {
                yield return 0;
                a += Time.deltaTime;
            }
        }

        public virtual IEnumerator BackIE()
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

        public virtual IEnumerator EmptyIE(string Key)
        {
            CmdControl.Main.NewLine("Error: Invalid command '" + Key + "'");
            yield return LineDelay(0.3f);
            CmdControl.Main.NewLine("");
        }
    }
}