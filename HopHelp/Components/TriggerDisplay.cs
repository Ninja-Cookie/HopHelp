using System.Collections.Generic;
using UnityEngine;

namespace HopHelp.Components
{
    internal class TriggerDisplay : MonoBehaviour
    {
        private GameObject          TriggerObject       = null;
        private TransformHandler    TransformHandler    = null;

        private readonly Dictionary<string, Color> TriggerColors = new Dictionary<string, Color>
        {
            {
                "CollisionDebugger",
                HopHelp.TriggerColors.Trigger
            },

            {
                "NPCItem",
                HopHelp.TriggerColors.NPC
            },

            {
                "SceneDoor",
                HopHelp.TriggerColors.Door
            },

            {
                "DoorFade",
                HopHelp.TriggerColors.Door * 0.5f
            },

            {
                "RespawnCheckpointZone",
                HopHelp.TriggerColors.Respawn
            },

            {
                "DarkBitPickup",
                HopHelp.TriggerColors.DarkBit
            },

            {
                "BugItem",
                HopHelp.TriggerColors.Bug
            },

            {
                "PickupableItem",
                HopHelp.TriggerColors.Interactable
            },

            {
                "CoinPickup",
                HopHelp.TriggerColors.Coin
            }
        };

        private Color TriggerColor = HopHelp.TriggerColors.Default;

        public void Awake()
        {
            foreach (var comp in base.GetComponentsInParent<Component>())
            {
                if (TriggerColors.TryGetValue(comp.GetType().ToString(), out var color))
                {
                    TriggerColor = color;
                    break;
                }
            }

            (TriggerObject = BuildTrigger())?.SetActive(gameObject.activeSelf);
        }

        public void Update()
        {
            HandleActiveState(base.gameObject?.activeSelf ?? false);
        }

        private void ActParented()
        {
            TriggerObject.transform.position    = TransformHandler.Position;
            TriggerObject.transform.rotation    = TransformHandler.Rotation;
            TriggerObject.transform.localScale  = TransformHandler.Scale;
        }

        private GameObject BuildTrigger()
        {
            TransformHandler = new TransformHandler(base.GetComponent<Collider>());

            var gameObject = new GameObject("DevCheatTrigger", typeof(MeshFilter), typeof(MeshRenderer));
            gameObject.GetComponent<MeshFilter>     ().mesh     = TransformHandler.Mesh;
            gameObject.GetComponent<MeshRenderer>   ().material = Generics.GenerateMaterial(Generics.TriggerMaterialShader, TriggerColor);

            gameObject.transform.position   = TransformHandler.Position;
            gameObject.transform.rotation   = TransformHandler.Rotation;
            gameObject.transform.localScale = TransformHandler.Scale;

            return gameObject;
        }

        public void OnDisable()
        {
            HandleActiveState(false);
        }

        public void OnDestry()
        {
            Destroy(TriggerObject);
        }

        private void HandleActiveState(bool active)
        {
            if (TriggerObject == null)
                return;

            if (!Generics.CheatsEnabled)
                active = false;

            TriggerObject.SetActive(active && ExtraCheats.Cheat_Triggers.TriggersVisible && TransformHandler?.IsNull == false);
            if (TriggerObject.activeSelf && TransformHandler?.IsNull == false)
                ActParented();
        }
    }
}
