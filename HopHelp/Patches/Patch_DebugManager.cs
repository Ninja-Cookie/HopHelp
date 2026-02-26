using HarmonyLib;
using UnityEngine;

namespace HopHelp.Patches
{
    internal class Patch_DebugManager : HarmonyPatch
    {
        [HarmonyPatch(typeof(DebugManager), "Update")]
        public static class Patch_DebugManager_Update
        {
            public static void Prefix()
            {
                if (Generics.CheatsEnabled && !Generics.DevPanelActive)
                    Bind.RunCommands();
            }
        }

        [HarmonyPatch(typeof(PlayerMotorState), "TriggerEnter")]
        public static class Patch_PlayerMotorState_TriggerEnter
        {
            public static void Prefix(Collider collider)
            {
                //Debug.Log($"Collider: {collider} | Layer: {collider.gameObject.layer}");

                /*
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
                */
            }
        }
    }
}
