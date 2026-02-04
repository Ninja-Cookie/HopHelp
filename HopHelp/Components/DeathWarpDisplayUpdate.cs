using UnityEngine;
using static HopHelp.ExtraCheats.Cheat_Warp;

namespace HopHelp.Components
{
    internal class DeathWarpDisplayUpdate : MonoBehaviour
    {
        public void Update()
        {
            if (WarpVisible && !WarpObjectAlive && Generics.Player != null)
                CreateDeathWarpDisplay();
        }
    }
}
