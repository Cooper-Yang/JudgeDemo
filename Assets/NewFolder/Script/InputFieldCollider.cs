using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cmd
{
    public class InputFieldCollider : MonoBehaviour {
        public TMP_InputField Field;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnMouseDown()
        {
            Field.ActivateInputField();
        }
    }
}