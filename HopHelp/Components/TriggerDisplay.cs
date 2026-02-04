using System;
using System.Collections.Generic;
using UnityEngine;

namespace HopHelp.Components
{
    internal class TriggerDisplay : MonoBehaviour
    {
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

        private GameObject TriggerObject = null;

        private readonly Dictionary<PrimitiveType, string> primitiveToResourceStringMap = new Dictionary<PrimitiveType, string>
        {
            {
                PrimitiveType.Cube,
                "Cube.fbx"
            },
            {
                PrimitiveType.Sphere,
                "New-Sphere.fbx"
            },
            {
                PrimitiveType.Capsule,
                "New-Capsule.fbx"
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
            if (TriggerObject == null || gameObject == null)
                return;

            if (!gameObject.activeSelf)
            {
                TriggerObject.SetActive(false);
                return;
            }

            if (!ExtraCheats.Cheat_Triggers.TriggersVisible && TriggerObject != null && TriggerObject.activeSelf)
                TriggerObject.SetActive(false);
            else if (ExtraCheats.Cheat_Triggers.TriggersVisible && TriggerObject != null && !TriggerObject.activeSelf)
                TriggerObject.SetActive(true);
        }

        private class TriggerData
        {
            internal Vector3    Position    { get; }
            internal Quaternion Rotation    { get; }
            internal Vector3    Scale       { get; }
            internal Mesh       Mesh        { get; }

            internal TriggerData(Vector3 position, Quaternion rotation, Vector3 scale, Mesh mesh)
            {
                Position    = position;
                Rotation    = rotation;
                Scale       = scale;
                Mesh        = mesh;
            }
        }

        private GameObject BuildTrigger()
        {
            TriggerData triggerData = FixCollider();
            if (triggerData == null)
                return null;

            var gameObject = new GameObject("DevCheatTrigger", typeof(MeshFilter), typeof(MeshRenderer));

            gameObject.GetComponent<MeshFilter>     ().mesh         = triggerData.Mesh;
            gameObject.GetComponent<MeshRenderer>   ().material     = Generics.GenerateMaterial(Generics.TriggerMaterialShader, TriggerColor);

            gameObject.transform.position   = triggerData.Position;
            gameObject.transform.rotation   = triggerData.Rotation;
            gameObject.transform.localScale = triggerData.Scale;

            gameObject.transform.SetParent(base.transform);

            return gameObject;
        }


        // Taken and modified from "CollisionDebugger > OnDrawGizmos"
        private TriggerData FixCollider()
        {
            var collider = base.GetComponent<Collider>();

            Transform       transform       = collider.transform;
            Vector3         position        = transform.position;
            Quaternion      quaternion      = transform.rotation;
            Vector3         lossyScale      = transform.lossyScale;
            Mesh            mesh            = null;

            MeshCollider    meshCollider    = collider as MeshCollider;

            if (meshCollider != null)
            {
                _       = meshCollider.convex;
                mesh    = meshCollider.sharedMesh;
            }

            PrimitiveType key = PrimitiveType.Sphere;

            switch (collider)
            {
                case BoxCollider boxCollider:
                    position += transform.TransformVector(boxCollider.center);
                    lossyScale.Scale(boxCollider.size);
                    key = PrimitiveType.Cube;
                break;

                case SphereCollider sphereCollider:
                    position    += transform.TransformVector(sphereCollider.center);
                    lossyScale  *= sphereCollider.radius * 2f;
                    key = PrimitiveType.Sphere;
                break;

                case CapsuleCollider capsuleCollider:
                    position        += transform.TransformVector(capsuleCollider.center);
                    float   y       = capsuleCollider.height / 2f;
                    float   num     = capsuleCollider.radius / 0.5f;
                    Vector3 scale   = new Vector3(num, y, num);
                    lossyScale.Scale(scale);

                    if (capsuleCollider.direction == 2)
                        quaternion = Quaternion.AngleAxis(90f, transform.right) * quaternion;
                    else if (capsuleCollider.direction == 0)
                        quaternion = Quaternion.AngleAxis(90f, transform.forward) * quaternion;

                    key = PrimitiveType.Capsule;
                break;

                default:
                    lossyScale.Scale(collider.bounds.size);
                return new TriggerData(collider.transform.position, collider.transform.rotation, lossyScale, mesh);
            }

            if (mesh == null)
                mesh = Resources.GetBuiltinResource<Mesh>(primitiveToResourceStringMap[key]);

            return new TriggerData(position, quaternion, lossyScale, mesh);
        }
    }
}
