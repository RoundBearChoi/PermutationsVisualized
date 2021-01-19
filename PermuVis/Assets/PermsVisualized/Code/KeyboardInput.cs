using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class KeyboardInput : MonoBehaviour
    {
        UIController uiController = null;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (uiController == null)
                {
                    uiController = FindObjectOfType<UIController>();
                }

                uiController.OnClickGo();
            }
        }
    }
}