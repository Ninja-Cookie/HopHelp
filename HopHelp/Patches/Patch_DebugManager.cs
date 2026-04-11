using HarmonyLib;
using UnityEngine;

namespace HopHelp.Patches
{
    internal class Patch_DebugManager : HarmonyPatch
    {
        private static bool AllowCommand = false;

        internal static void Override_ToggleFastMode()
        {
            AllowCommand = true;
            Singleton<DebugManager>.Instance.ToggleFastMode();
        }

        internal static void Override_PlayQueuedAnimation()
        {
            AllowCommand = true;
            Singleton<DebugManager>.Instance.InvokeMethod("PlayQueuedAnimation");
        }

        internal static void Override_ToggleNoClip()
        {
            AllowCommand = true;
            Singleton<DebugManager>.Instance.ToggleNoClip();
        }

        internal static void Override_EndNoClip()
        {
            AllowCommand = true;
            Singleton<DebugManager>.Instance.EndNoClip();
        }

        [HarmonyPatch(typeof(DebugManager), "Update")]
        public static class Patch_DebugManager_Update
        {
            public static void Prefix()
            {
                if (Generics.CheatsEnabled)
                    Bind.RunCommands();
            }
        }

        [HarmonyPatch(typeof(DebugManager), "ToggleFastMode")]
        public static class Patch_DebugManager_ToggleFastMode
        {
            public static bool Prefix()
            {
                bool allow = AllowCommand;
                AllowCommand = false;
                return allow;
            }
        }

        [HarmonyPatch(typeof(DebugManager), "PlayQueuedAnimation")]
        public static class Patch_DebugManager_PlayQueuedAnimation
        {
            public static bool Prefix()
            {
                bool allow = AllowCommand;
                AllowCommand = false;
                return allow;
            }
        }

        [HarmonyPatch(typeof(DevCheats), "ToggleNoClip")]
        public static class Patch_DevCheats_ToggleNoClip
        {
            public static bool Prefix()
            {
                Override_ToggleNoClip();
                return false;
            }
        }

        [HarmonyPatch(typeof(DebugManager), "ToggleNoClip")]
        public static class Patch_DebugManager_ToggleNoClip
        {
            public static bool Prefix()
            {
                bool allow = AllowCommand;
                AllowCommand = false;
                return allow;
            }
        }

        [HarmonyPatch(typeof(DebugManager), "EndNoClip")]
        public static class Patch_DebugManager_EndNoClip
        {
            public static bool Prefix()
            {
                bool allow = AllowCommand;
                AllowCommand = false;
                return allow;
            }
        }

        [HarmonyPatch(typeof(DebugManager), "HitDevCheatsButton")]
        public static class Patch_DebugManager_HitDevCheatsButton
        {
            public static bool Prefix(out bool gamepadMode, out bool __result)
            {
                if (Input.GetKeyDown(KeyCode.F8))
                {
                    gamepadMode = false;
                    __result = true;
                    return false;
                }

                gamepadMode = default;
                __result = false;
                return true;
            }
        }

        // DEBUG // ------------------------------------------------------ //

        /*
        [HarmonyPatch(typeof(WidgetFPS), "Start")]
        public static class Patch_WidgetFPS_Start
        {
            public static void Prefix(WidgetFPS __instance)
            {
                __instance.gameObject?.SetActive(false);
            }
        }

        [HarmonyPatch(typeof(PlayerMotorState), "TriggerEnter")]
        public static class Patch_PlayerMotorState_TriggerEnter
        {
            public static void Prefix(Collider collider)
            {
                //Debug.Log($"Collider: {collider} | Layer: {collider.gameObject.layer}");

                if (!collider.isTrigger || collider.name.Contains("PathShape"))
                    return;

                Debug.Log("-----------------");
                Debug.Log($"Object: {collider.gameObject}");
                Debug.Log($"Layer: {collider.gameObject.layer}");
                Debug.Log($"Tag: {collider.gameObject.tag}");
                foreach (var comp in collider.gameObject.GetComponentsInParent<Component>())
                {
                    Debug.Log(comp);
                }
                Debug.Log("-----------------");
            }
        }
        */
    }
}
