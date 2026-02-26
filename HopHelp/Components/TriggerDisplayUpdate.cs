using UnityEngine;

namespace HopHelp.Components
{
    internal class TriggerDisplayUpdate : MonoBehaviour
    {
        float UpdateTriggersMax = 1f;
        float UpdateTriggers    = 0f;

        public void Update()
        {
            if (Generics.CheatsEnabled && ExtraCheats.Cheat_Triggers.TriggersVisible && (UpdateTriggers += Time.deltaTime) >= UpdateTriggersMax)
            {
                ExtraCheats.Cheat_Triggers.AddTriggerDisplays(GameObject.FindObjectsOfType<Collider>());
                UpdateTriggers = 0f;
            }
        }
    }
}
