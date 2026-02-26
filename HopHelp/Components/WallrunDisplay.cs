using HopHelp.ExtraCheats;
using UnityEngine;

namespace HopHelp.Components
{
    internal class WallrunDisplay : MonoBehaviour
    {
        private GameObject          Origin;
        private GameObject          WallrunDisplayObject;

        private PlayerMotor         Owner;
        private PlayerMotor_WallRun Wallrun;

        private readonly Vector3 Scale = new Vector3(0.6f, 0.05f, 4f);

        internal void CreateWallrunDisplay()
        {
            Origin = new GameObject("WallrunDisplay_Origin");

            WallrunDisplayObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(WallrunDisplayObject.GetComponent<Collider>());

            var material = Generics.GenerateMaterial(Generics.WarpMaterialShader, TriggerColors.Default * 2f);

            WallrunDisplayObject.GetComponent<Renderer>().material = material;

            WallrunDisplayObject.transform.localScale   = Scale;
            WallrunDisplayObject.transform.position     = Origin.transform.position + new Vector3(0f, 0f, Scale.z * 0.5f);
            WallrunDisplayObject.transform.SetParent(Origin.transform);
        }

        internal void Update()
        {
            if (!Generics.CheatsEnabled || Generics.Player == null)
            {
                if (Origin?.activeSelf == true)
                    Origin?.SetActive(false);

                return;
            }

            var state       = Generics.Player.Motor?.StateMachine?.CurrentState;
            var wallrunning = state?.GetType() == typeof(PlayerMotor_WallRun) || state?.GetType() == typeof(PlayerMotor_ClimbFree);

            if (Origin == null)
                CreateWallrunDisplay();

            if (!Cheat_Wallrun.DisplayWallrun || !wallrunning)
                Origin.SetActive(false);
            else if (Cheat_Wallrun.DisplayWallrun && wallrunning)
                Origin.SetActive(true);

            if (Origin.activeSelf)
            {
                Origin.transform.position = Generics.Player.transform.position;
                Origin.transform.rotation = Quaternion.LookRotation(GetWallrunSpeed().normalized, Owner.GravityNormal);
            }
        }

        internal Vector3 GetWallrunSpeed()
        {
            if (Owner == null)
                Owner = Generics.Player.Motor;

            if (Wallrun == null)
                Wallrun = Owner.GetState<PlayerMotor_WallRun>();

            Vector3 normal = Owner.WallHitInfo.normal;
            Vector3 vector = Vector3.zero;

            float d = Mathf.Lerp(Wallrun.GetValue<float>("jumpForce"), Wallrun.GetValue<float>("repeatMinJumpForce"), (float)Wallrun.GetValue<int>("wallJumpsSinceStableState") / (float)Wallrun.GetValue<int>("numJumpRepeatsToLoseForce"));
            if (Wallrun.GetValue<float>("runDirection") == 0f)
            {
                vector = normal * Wallrun.GetValue<float>("appliedSpeed") + Owner.GravityNormal * d;
            }
            else
            {
                Vector3 vector2 = Vector3.ProjectOnPlane(Owner.MoveDir, Owner.GravityNormal);
                vector = ((vector2.normalized + normal) / 2f).normalized * Mathf.Max(vector2.magnitude, Wallrun.GetValue<float>("appliedSpeed"));
                vector += Owner.GravityNormal * d;
            }

            return vector * Owner.JumpModLens;
        }

        internal void OnDestroy()
        {
            if (Origin != null && !(Origin is null))
                Destroy(Origin);
        }
    }
}
