using UnityEngine;

namespace HopHelp
{
    internal static class TriggerColors
    {
        private const float Alpha = 0.3f;

        internal static readonly Color Default      = new Color(1.00f, 0.00f, 0.00f, Alpha * 0.80f);
        internal static readonly Color Trigger      = new Color(0.00f, 1.00f, 0.00f, Alpha);
        internal static readonly Color Door         = new Color(0.00f, 1.00f, 1.00f, Alpha);
        internal static readonly Color Respawn      = new Color(0.00f, 0.00f, 1.00f, Alpha);
        internal static readonly Color NPC          = new Color(1.00f, 1.00f, 1.00f, Alpha);
        internal static readonly Color DarkBit      = new Color(0.68f, 0.24f, 0.74f, Alpha);
        internal static readonly Color Bug          = new Color(0.09f, 0.56f, 0.14f, Alpha);
        internal static readonly Color Interactable = new Color(1.00f, 0.00f, 1.00f, Alpha);
        internal static readonly Color Coin         = new Color(1.00f, 0.81f, 0.00f, Alpha);
    }
}
