using HopHelp.Components;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Wallrun
    {
        internal static bool DisplayWallrun { get; private set; } = false;
        private static GameObject DisplayObject;

        [CheatMenu]
        public static void ToggleShowWallrun()
        {
            DisplayWallrun = !DisplayWallrun;
            if (DisplayWallrun && DisplayObject == null)
                DisplayObject = Generics.LoadManager?.gameObject.AddComponentIfMissing<WallrunDisplay>().gameObject;

            DevCheats.Log($"[ToggleShowWallrun] Wallrun Display {(DisplayWallrun ? "Enabled" : "Disabled")}");
        }
    }
}
