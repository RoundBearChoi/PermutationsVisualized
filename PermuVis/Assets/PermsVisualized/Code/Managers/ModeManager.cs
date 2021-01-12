using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum GameModes
    {
        NORMAL,
        MANUAL_CLICK,
    }

    public static class ModeManager
    {
        public static GameModes mode = GameModes.NORMAL;
    }
}