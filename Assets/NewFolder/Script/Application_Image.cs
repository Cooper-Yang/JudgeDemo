using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cmd
{
    public class Application_Image : Application {
        public Sprite image;

        public override void Process()
        {
            CmdControl.Main.NewLine("");
            StartCoroutine("ProcessIE");
        }

        public IEnumerator ProcessIE()
        {
            CmdControl.Main.NewLine("Loading Image: " + SourceFile.Key + ".igf");
            yield return LineDelay(Random.Range(0.5f, 1.2f));
            CmdControl.Main.NewLine(".");
            yield return LineDelay(Random.Range(0.5f, 1.2f));
            CmdControl.Main.NewLine("..");
            yield return LineDelay(Random.Range(0.5f, 1.2f));
            CmdControl.Main.NewLine("...");
            yield return LineDelay(1f);
            yield return LoadImage();
        }

        public IEnumerator LoadImage()
        {
            CmdControl.Main.ClearLines();
            CmdControl.Main.ImageRenderer.sprite = image;
            CmdControl.Main.ImageRenderer.SetNativeSize();
            CmdControl.Main.ImageRenderer.gameObject.SetActive(true);
            CmdControl.Main.ImageRenderer.material = new Material(CmdControl.Main.ImageRenderer.material);
            CmdControl.Main.PixelFade.PixelMaterial = CmdControl.Main.ImageRenderer.material;
            CmdControl.Main.PixelFade.StartCoroutine(CmdControl.Main.PixelFade.FadeIn());
            yield return LineDelay(1f);
            CmdControl.Main.NewLine(">{FieldActive{", CmdControl.Main.MaxLine);
        }

        public override void Execute(string Key)
        {
            if (Key == "Back" || Key == "back")
                StartCoroutine(BackIE());
            else
                StartCoroutine(EmptyIE(Key));
        }

        public override IEnumerator BackIE()
        {
            CmdControl.Main.ClearLines();
            CmdControl.Main.PixelFade.StartCoroutine(CmdControl.Main.PixelFade.FadeOut());
            yield return LineDelay(1f);
            CmdControl.Main.ImageRenderer.gameObject.SetActive(false);
            if (!SourceFile.UpFile)
                yield break;
            else
                CmdControl.Main.LoadFile(SourceFile.UpFile);
            yield return 0;
        }
    }
}