using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Debug
    {
        [CheatMenu]
        public static void SetFPS(string fps)
        {
            if (string.IsNullOrEmpty(fps))
                return;

            if (int.TryParse(fps, out var realFPS))
            {
                realFPS = Mathf.Max(realFPS, 1);
                QualitySettings.vSyncCount  = 0;
                Application.targetFrameRate = realFPS;
            }
        }
    }
}
