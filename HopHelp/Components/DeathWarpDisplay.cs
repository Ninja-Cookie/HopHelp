using System.Linq;
using UnityEngine;

namespace HopHelp.Components
{
    internal class DeathWarpDisplay : MonoBehaviour
    {
        private GameObject SecondVisual;

        public void Awake()
        {
            SetUpVisuals();
            SetToSafePosition();
        }

        private void SetUpVisuals()
        {
            SetUpSecondVisual();

            base        .GetComponent<Renderer>().material = Generics.GenerateMaterial(Generics.WarpMaterialShader,     TriggerColors.Default * 1.5f);
            SecondVisual.GetComponent<Renderer>().material = Generics.GenerateMaterial(Generics.TriggerMaterialShader,  TriggerColors.Respawn * 1.5f);

            var scale = new Vector3(1.7f, 0.8f, 1.7f);
            base.transform.localScale = scale;

            SecondVisual.transform.localScale = new Vector3(SecondVisual.transform.localScale.x, 0.1f, SecondVisual.transform.localScale.z);

            var pos = base.transform.position;
            pos.y = (pos.y - base.transform.localScale.y) + SecondVisual.transform.localScale.y;
            SecondVisual.transform.position = pos;
        }

        private void SetUpSecondVisual()
        {
            SecondVisual = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Destroy(SecondVisual.GetComponent<Collider>());
            SecondVisual.transform.position = base.transform.position;
            SecondVisual.transform.parent = base.transform;
        }

        private void SetToSafePosition()
        {
            var pos = Generics.Player?.Motor.LastSafePos ?? Vector3.zero;
            pos.y += base.transform.localScale.y;
            base.transform.position = pos;
        }

        public void Update()
        {
            if (base.gameObject.activeSelf && !ExtraCheats.Cheat_Warp.WarpVisible)
            {
                base.gameObject.SetActive(false);
                return;
            }

            if (ExtraCheats.Cheat_Warp.WarpVisible)
                SetToSafePosition();
        }
    }
}
