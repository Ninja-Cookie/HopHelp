using HopHelp.Components;
using System.Linq;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal class Cheat_Triggers
    {
        public static bool TriggersVisible = false;

        [CheatMenu]
        public static void ToggleShowTriggers()
        {
            if (TriggersVisible = !TriggersVisible)
            {
                AddTriggerDisplays(GameObject.FindObjectsOfType<Collider>());
                Generics.LoadManager?.gameObject.AddComponentIfMissing<TriggerDisplayUpdate>();
            }

            DevCheats.Log($"[ToggleShowTriggers] Triggers {(TriggersVisible ? "Enabled" : "Disabled")}");
        }

        internal static void AddTriggerDisplays(Collider[] colliders)
        {
            if (colliders == null)
                return;

            var triggers = colliders.Where
            (x =>
                x.GetComponent<TriggerDisplay>()        == null &&
                x.GetComponent<VisionConeGenerator>()   == null &&
                x.isTrigger                                     &&
                (
                    x.gameObject.layer == 0     ||
                    x.gameObject.layer == 11    ||
                    x.gameObject.layer == 25
                )
            ).Select(x => x.gameObject);

            foreach (var trigger in triggers)
                trigger.AddComponent(typeof(TriggerDisplay));
        }
    }
}
