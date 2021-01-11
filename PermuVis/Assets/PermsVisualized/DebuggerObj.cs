using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DebuggerObj : MonoBehaviour
    {
        public void Log(object message)
        {
            Debug.Log(message);
        }
    }
}