using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application_Audio : Application {
        public AudioClip Clip;
        public float PlayTime;
        public bool Pause;
        public bool Playing;

        public override void Process()
        {
            CmdControl.Main.NewLine("");
            StartCoroutine("ProcessIE");
        }

        public IEnumerator ProcessIE()
        {
            CmdControl.Main.NewLine("Loading Audio: " + SourceFile.Key + ".adr");
            yield return LineDelay(Random.Range(0.5f, 1.2f));
            CmdControl.Main.NewLine(".");
            yield return LineDelay(Random.Range(0.5f, 1.2f));
            CmdControl.Main.NewLine("..");
            yield return LineDelay(Random.Range(0.5f, 1.2f));
            CmdControl.Main.NewLine("...");
            yield return LineDelay(1f);
            yield return AudioProcess();
        }

        public IEnumerator AudioProcess()
        {
            CmdControl.Main.ClearLines();
            CmdControl.Main.NewLine(">{FieldActive{", CmdControl.Main.MaxLine);
            PlayTime = 0f;
            Pause = false;
            Playing = true;
            CmdControl.Main.AudioCamera.gameObject.SetActive(true);
            AudioSource AS = CmdControl.Main.AS;
            AS.clip = Clip;
            AS.Play();
            while (Playing)
            {
                if (Pause)
                    CmdControl.Main.NewLine(SourceFile.Key + " (Pause)", CmdControl.Main.MaxLine - 1);
                else
                {
                    CmdControl.Main.NewLine(SourceFile.Key + " (" + TimeToString(PlayTime) + ")", CmdControl.Main.MaxLine - 1);
                    PlayTime += Time.deltaTime;
                }
                yield return 0;
            }
        }

        public void Replay()
        {
            PlayTime = 0f;
            Pause = false;
            AudioSource AS = CmdControl.Main.AS;
            AS.Stop();
            AS.Play();
            CmdControl.Main.NewLine(">{FieldActive{", CmdControl.Main.MaxLine);
        }

        public void TryPause()
        {
            if (Pause)
            {
                Pause = false;
                CmdControl.Main.AS.UnPause();
            }
            else
            {
                Pause = true;
                CmdControl.Main.AS.Pause();
            }
            CmdControl.Main.NewLine(">{FieldActive{", CmdControl.Main.MaxLine);
        }

        public override void Execute(string Key)
        {
            if (Key == "Back" || Key == "back")
                StartCoroutine(BackIE());
            else if ((Key == "Pause" || Key == "pause") && !Pause)
                TryPause();
            else if ((Key == "Play" || Key == "play") && Pause)
                TryPause();
            else if (Key == "Replay" || Key == "replay")
                Replay();
            else
                StartCoroutine(EmptyIE(Key));
        }

        public override IEnumerator BackIE()
        {
            Playing = false;
            AudioSource AS = CmdControl.Main.AS;
            AS.Stop();
            AS.clip = null;
            yield return 0;
            CmdControl.Main.ClearLines();
            CmdControl.Main.AudioCamera.gameObject.SetActive(false);
            yield return LineDelay(1f);
            if (!SourceFile.UpFile)
                yield break;
            else
                CmdControl.Main.LoadFile(SourceFile.UpFile);
            yield return 0;
        }

        public string TimeToString(float Value)
        {
            string a = "";
            int A = (int)Value / 60;
            if (A < 10)
                a = "0" + A;
            else
                a = A.ToString();
            string b = "";
            int B = (int)Value % 60;
            if (B < 10)
                b = "0" + B;
            else
                b = B.ToString();
            return a + ":" + b;
        }
    }
}