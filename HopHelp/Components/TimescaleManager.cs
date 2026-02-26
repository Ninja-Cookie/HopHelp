using HopHelp.Patches;
using UnityEngine;
using HopHelp.ExtraCheats;

namespace HopHelp.Components
{
    internal class TimescaleManager : MonoBehaviour
    {
        internal void Awake()
        {
            UpdateTimescale();
        }

        internal void Update()
        {
            UpdateTimescale();
        }

        internal void UpdateTimescale()
        {
            if (Cheat_Timescale.Timescale == 1f)
                return;

            if (Generics.CheatsEnabled && !Generics.DevPanelActive && Time.timeScale != Cheat_Timescale.Timescale)
            {
                Time.timeScale = Cheat_Timescale.Timescale;
            }
            else if (!Generics.CheatsEnabled)
            {
                Time.timeScale              = 1f;
                Cheat_Timescale.Timescale   = 1f;
            }
        }
    }
}
