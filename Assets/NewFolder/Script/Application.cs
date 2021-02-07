using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cmd
{
    public class Application : MonoBehaviour {
        public File SourceFile;

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
    }
}