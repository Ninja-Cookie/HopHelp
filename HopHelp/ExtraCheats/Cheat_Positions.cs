using System;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Positions
    {
        public enum Slots
        {
            Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8, Slot9, Slot10, Slot11, Slot12, Slot13, Slot14, Slot15, Slot16
        }

        public static Vector3[] SavedPositions = new Vector3[Enum.GetValues(typeof(Slots)).Length];

        [CheatMenu]
        public static void SetPosition(string x, string y, string z)
        {
            if (Generics.Player == null)
            {
                DevCheats.Log($"[SetPosition] Player not found...");
                return;
            }

            Generics.Player.transform.position = ParsePosition(x, y, z);
        }

        [CheatMenu]
        public static void SavePositionExact(string x, string y, string z, Slots slot = Slots.Slot1)
        {
            SavePosition(slot, ParsePosition(x, y, z));
        }

        [CheatMenu]
        public static void SavePosition(Slots slot = Slots.Slot1)
        {
            if (!TryGetPlayerPosition(out var position))
            {
                DevCheats.Log($"[SavePosition] Player not found...");
                return;
            }

            SavePosition(slot, position);
            DevCheats.Log($"[SavePosition] Position saved at \"{(Vector3)position}\" to {slot}");
        }

        [CheatMenu]
        public static void LoadPosition(Slots slot = Slots.Slot1)
        {
            if (Generics.Player == null)
            {
                DevCheats.Log($"[LoadPosition] Player not found...");
                return;
            }

            Generics.Player.Motor.SnapToLocation(SavedPositions[(int)slot], Generics.Player.transform.rotation);
        }

        [CheatMenu]
        public static void GetPosition()
        {
            if (!TryGetPlayerPosition(out var position))
            {
                DevCheats.Log($"[GetPosition] Player not found...");
                return;
            }

            DevCheats.Log($"[GetPosition] {(Vector3)position}");
        }

        private static bool TryGetPlayerPosition(out Vector3 position)
        {
            if (Generics.Player == null)
            {
                position = default(Vector3);
                return false;
            }

            position = Generics.Player.transform.position;
            return true;
        }

        private static Vector3 ParsePosition(string x, string y, string z)
        {
            float pos_x = float.TryParse(x, out var new_x) ? new_x : 0f;
            float pos_y = float.TryParse(y, out var new_y) ? new_y : 0f;
            float pos_z = float.TryParse(z, out var new_z) ? new_z : 0f;

            return new Vector3(pos_x, pos_y, pos_z);
        }

        private static void SavePosition(Slots slot, Vector3 position)
        {
            SavedPositions[(int)slot] = position;
            DataHandler.Save();
        }
    }
}
