using HopHelp.Components;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Warp
    {
        public  static bool         WarpVisible     = false;
        private static GameObject   WarpObject      = null;
        public  static bool         WarpObjectAlive => !(WarpObject == null || WarpObject is null);

        [CheatMenu]
        public static void ToggleShowDeathWarp()
        {
            WarpVisible = !WarpVisible;
            if (WarpObjectAlive)
            {
                if (!WarpObject.activeSelf && WarpVisible)
                    WarpObject.SetActive(true);

                return;
            }

            if (Generics.Player == null)
            {
                DevCheats.Log($"[ToggleShowDeathWarp] Player not found...");
                return;
            }

            CreateDeathWarpDisplay();
        }

        public static void CreateDeathWarpDisplay()
        {
            WarpObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            GameObject.Destroy(WarpObject.GetComponent<Collider>());
            WarpObject.AddComponent<DeathWarpDisplay>();
            Generics.LoadManager.gameObject.AddComponentIfMissing<DeathWarpDisplayUpdate>();
        }
    }
}
