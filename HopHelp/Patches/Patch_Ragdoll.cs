using HarmonyLib;
using HopHelp.ExtraCheats;
using Luckshot.FSM;
using System.Linq;
using System.Reflection;

namespace HopHelp.Patches
{
    internal class Patch_Ragdoll : HarmonyPatch
    {
        /*
        [HarmonyPatch]
        public class Patch_StateMachine_IsInState
        {
            public static MethodBase TargetMethod()
            {
                return typeof(StateMachine)
                    .GetMethods()
                    .Where(m => m.Name == "IsInState" && m.IsGenericMethodDefinition)
                    .First()
                    .MakeGenericMethod(typeof(PlayerMotor_Dead));
            }

            public static void Postfix(ref bool __result)
            {
                if (Cheat_Ragdoll.Ragdoll)
                    __result = true;
            }
        }
        */
    }
}
