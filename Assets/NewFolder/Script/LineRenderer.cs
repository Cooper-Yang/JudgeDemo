using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cmd
{
    public class LineRenderer : MonoBehaviour {
        public TextMeshProUGUI TEXT;
        public TMP_InputField Field;
        public float FieldPosition;
        public bool InputActive;

        public void Ini(string Text, bool FieldActive, string FieldText)
        {
            TEXT.text = Text;
            Field.gameObject.SetActive(FieldActive);
            Field.text = FieldText;
            Render();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
            InputUpdate();
        }

        public void Render()
        {
            TEXT.ForceMeshUpdate();
            float x = TEXT.transform.localPosition.x + TEXT.preferredWidth + FieldPosition;
            Field.transform.localPosition = new Vector3(x, Field.transform.localPosition.y, Field.transform.localPosition.z);
        }

        public void InputUpdate()
        {
            InputActive = Field.gameObject.activeInHierarchy && this == CmdControl.Main.LRs[CmdControl.Main.LRs.Count - 1];
            Field.readOnly = !InputActive;

            if (InputActive && Input.GetKeyDown(KeyCode.Return))
            {
                CmdControl.Main.OnInput(Field.text);
                Field.DeactivateInputField();
            }
        }
    }
}