using HarmonyLib;
using UnityEngine;

namespace HopHelp.Patches
{
    internal class Patch_DebugManager : HarmonyPatch
    {
        private static PanelDevCheatConsole _panelDevCheatConsole;
        private static BigHopsPrefs _bigHopsPrefs;
        private static bool DevPanelActive  => GetPanelState();
        private static bool CheatsEnabled   => GetCheatState();

        private static bool GetPanelState()
        {
            if (_panelDevCheatConsole != null)
                return _panelDevCheatConsole.gameObject.activeSelf;

            return (_panelDevCheatConsole = Singleton<UIManager>.Instance?.GetPanel<PanelDevCheatConsole>())?.gameObject.activeSelf ?? false;
        }

        private static bool GetCheatState()
        {
            if (_bigHopsPrefs != null)
                return _bigHopsPrefs.EnableCheats;

            return (_bigHopsPrefs = ScriptableObjectSingleton<BigHopsPrefs>.Instance)?.EnableCheats ?? false;
        }

        [HarmonyPatch(typeof(DebugManager), "Update")]
        public static class Patch_DebugManager_Update
        {
            public static void Prefix()
            {
                if (CheatsEnabled && !DevPanelActive)
                    Bind.RunCommands();
            }
        }
    }
}
