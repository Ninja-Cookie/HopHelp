using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Timescale
    {
        private static float _timescale;
        internal static float Timescale { get => _timescale; set { _timescale = Mathf.Clamp(value, 0f, 200f); } }

        [CheatMenu]
        public static void SetTimescale(string scale)
        {
            if (float.TryParse(scale, out var result))
            {
                Timescale = result;
                DevCheats.Log($"[SetTimescale] Set Scale to {Timescale}");

                Generics.LoadManager?.gameObject.AddComponentIfMissing<Components.TimescaleManager>();
            }
            else if (string.IsNullOrEmpty(scale))
            {
                DevCheats.Log($"[SetTimescale] Scale is {Timescale}");
            }
        }
    }
}
