using HopHelp.Components;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Warp
    {
        public  static WarpTypes    WarpType        { get; private set; } = WarpTypes.Death;
        public  static bool         WarpVisible     { get; private set; } = false;
        private static GameObject   WarpObject      = null;
        public  static bool         WarpObjectAlive => !(WarpObject == null || WarpObject is null);

        public enum WarpTypes
        {
            Death,
            Drip
        }

        [CheatMenu]
        public static void ToggleShowWarp(WarpTypes warpType = WarpTypes.Death)
        {
            if (Generics.Player == null)
            {
                DevCheats.Log($"[ToggleShowWarp] Player not found...");
                return;
            }

            if (warpType == WarpType || !WarpVisible)
                WarpVisible = !WarpVisible;
            WarpType = warpType;

            if (!WarpObjectAlive)
                CreateWarpDisplay();

            if (WarpObjectAlive && !WarpObject.activeSelf && WarpVisible)
                WarpObject.SetActive(true);

            DevCheats.Log("[ToggleShowWarp] " + (WarpVisible ? $"Showing {WarpType} Enabled" : $"Showing {WarpType} Disabled"));
        }

        public static void CreateWarpDisplay()
        {
            WarpObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            GameObject.Destroy(WarpObject.GetComponent<Collider>());
            WarpObject.AddComponent<WarpDisplay>();
            Generics.LoadManager?.gameObject.AddComponentIfMissing<WarpDisplayUpdate>();
        }
    }
}
