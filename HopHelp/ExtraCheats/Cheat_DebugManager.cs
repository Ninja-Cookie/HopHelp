using HopHelp.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_DebugManager
    {
        [CheatMenu]
        public static void ToggleFastMode()
        {
            Patch_DebugManager.Override_ToggleFastMode();
        }

        [CheatMenu]
        public static void PlayQueuedAnimation()
        {
            Patch_DebugManager.Override_PlayQueuedAnimation();
        }

        [CheatMenu]
        public static void EndNoClip()
        {
            Patch_DebugManager.Override_EndNoClip();
        }

        [CheatMenu]
        public static void ToggleConsole()
        {
            PanelDevCheatConsole panel = Singleton<UIManager>.Instance.GetPanel<PanelDevCheatConsole>();
            panel.gamepadMode = false;
            panel.gameObject.SetActive(!panel.gameObject.activeSelf);
        }
    }
}
