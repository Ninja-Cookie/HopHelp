using System.Collections.Generic;
using UnityEngine;

namespace HopHelp
{
    internal class TransformHandler
    {
        internal Vector3    Position    => GetPosition();
        internal Quaternion Rotation    => GetRotation();
        internal Vector3    Scale       => GetScale();
        internal Mesh       Mesh        => GetMesh();

        internal bool IsNull => Collider == null || Collider is null;

        internal Collider Collider;
        private ColliderTypes ColliderType = ColliderTypes.NONE;
        private enum ColliderTypes
        {
            NONE,
            Box,
            Sphere,
            Capsule,
        }

        private readonly Dictionary<ColliderTypes, string> PrimitiveToResourceStringMap = new Dictionary<ColliderTypes, string>
        {
            {
                ColliderTypes.Box,
                "Cube.fbx"
            },
            {
                ColliderTypes.Sphere,
                "New-Sphere.fbx"
            },
            {
                ColliderTypes.Capsule,
                "New-Capsule.fbx"
            }
        };

        private Vector3 GetPosition()
        {
            switch (ColliderType)
            {
                case ColliderTypes.Box:     return Collider.transform.TransformPoint((Collider as BoxCollider)      .center);
                case ColliderTypes.Sphere:  return Collider.transform.TransformPoint((Collider as SphereCollider)   .center);
                case ColliderTypes.Capsule: return Collider.transform.TransformPoint((Collider as CapsuleCollider)  .center);

                default: return Collider.transform.position;
            }
        }

        private Quaternion GetRotation()
        {
            var rotation = Collider.transform.rotation;
            if (ColliderType != ColliderTypes.Capsule)
                return rotation;

            var collider = Collider as CapsuleCollider;

            if (collider.direction == 2)
                rotation = Quaternion.AngleAxis(90f, collider.transform.right) * collider.transform.rotation;
            else if (collider.direction == 0)
                rotation = Quaternion.AngleAxis(90f, collider.transform.forward) * collider.transform.rotation;

            return rotation;
        }

        private Vector3 GetScale()
        {
            switch (ColliderType)
            {
                case ColliderTypes.Box:     return Vector3.Scale((Collider as BoxCollider).size, Collider.transform.lossyScale);
                case ColliderTypes.Sphere:  return Collider.transform.lossyScale * ((Collider as SphereCollider).radius * 2f);
                case ColliderTypes.Capsule:
                    var collider    = Collider as CapsuleCollider;
                    float y         = collider.height / 2f;
                    float num       = collider.radius / 0.5f;
                return Vector3.Scale(new Vector3(num, y, num), Collider.transform.lossyScale);

                default: return Collider.transform.lossyScale;
            }
        }

        private Mesh GetMesh()
        {
            MeshCollider meshCollider = (Collider as MeshCollider);

            if (meshCollider != null)
            {
                _ = meshCollider.convex;
                return meshCollider.sharedMesh;
            }

            if (ColliderType == ColliderTypes.NONE)
                return null;

            return Resources.GetBuiltinResource<Mesh>(PrimitiveToResourceStringMap[ColliderType]);
        }

        internal TransformHandler(Collider collider)
        {
            switch (collider)
            {
                case BoxCollider        _:  ColliderType = ColliderTypes.Box;       break;
                case SphereCollider     _:  ColliderType = ColliderTypes.Sphere;    break;
                case CapsuleCollider    _:  ColliderType = ColliderTypes.Capsule;   break;

                default: ColliderType = ColliderTypes.NONE; break;
            }

            Collider = collider;
        }
    }
}
