using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class Debugger
    {
        private static DebuggerObj debuggerObj = null;

        public static DebuggerObj INSTANCE
        {
            get
            {
                if (debuggerObj == null)
                {
                    debuggerObj = UnityEngine.MonoBehaviour.FindObjectOfType<DebuggerObj>();
                }

                return debuggerObj;
            }
        }

        public static void Log(object message)
        {
            INSTANCE.Log(message);
        }
    }
}