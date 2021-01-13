using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ShowVersionNumber : MonoBehaviour
    {
        UnityEngine.UI.Text versionText = null;

        void Start()
        {
            versionText = this.gameObject.GetComponent<UnityEngine.UI.Text>();
            versionText.text = "v" + Application.version;
        }
    }
}