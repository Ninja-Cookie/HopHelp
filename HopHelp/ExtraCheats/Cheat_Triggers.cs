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

        [CheatMenu]
        public static void TestTriggers()
        {
            var colliders = GameObject.FindObjectsOfType<Collider>().Where(x => x.name.Contains("Cube (2)")).ToArray();

            foreach (var collider in colliders)
            {
                Debug.Log("adding...");

                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject.Destroy(go.GetComponent<Collider>());
                go.AddComponentIfMissing<Renderer>().material = null;
                //go.transform.position += collider.transform.TransformVector(collider.bounds.center);
                //go.transform.rotation = collider.transform.rotation;
                //go.transform.lossyScale.Scale(collider.bounds.size);
                //go.transform.position = collider.bounds.center;
                go.transform.position = collider.transform.TransformPoint((collider as BoxCollider).center);
                go.transform.rotation = collider.transform.rotation;
                go.transform.localScale = Vector3.Scale((collider as BoxCollider).size, collider.transform.lossyScale);
                //go.transform.lossyScale.Scale(collider.bounds.size);
            }
        }

        public static GameObject BuildWorldTrigger(BoxCollider collider)
        {
            var trigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(trigger.GetComponent<Collider>());

            trigger.transform.position      = collider.transform.TransformPoint(collider.center);
            trigger.transform.rotation      = collider.transform.rotation;
            trigger.transform.localScale    = Vector3.Scale(collider.size, collider.transform.lossyScale);

            return trigger;
        }
    }
}
