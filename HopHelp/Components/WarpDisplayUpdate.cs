using UnityEngine;
using static HopHelp.ExtraCheats.Cheat_Warp;

namespace HopHelp.Components
{
    internal class WarpDisplayUpdate : MonoBehaviour
    {
        public void Update()
        {
            if (Generics.CheatsEnabled && WarpVisible && !WarpObjectAlive && Generics.Player != null)
                CreateWarpDisplay();
        }
    }
}
