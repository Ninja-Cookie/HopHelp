using System.Linq;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Lookup
    {
        [CheatMenu]
        public static void GetNamesOf(string type)
        {
            var objType = Cheat_Poke.Assembly.GetType(type, false, true);
            if (objType == null)
            {
                DevCheats.Log($"[GetNamesOf] Type \"{type}\" not found...");
                return;
            }

            try
            {
                GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.GetComponent(objType) != null).ToArray();
                DevCheats.Log($"{objs.Length}");
                foreach (var obj in objs)
                    DevCheats.Log($"# {obj.name}");
            }
            catch
            {
                DevCheats.Log($"[GetNamesOf] Something went wrong...");
            }
        }

        [CheatMenu]
        public static void ForceActive(string name)
        {
            ForceState(name, false, true);
        }

        [CheatMenu]
        public static void ForceUnactive(string name)
        {
            ForceState(name, false, false);
        }

        [CheatMenu]
        public static void ForceActiveToggle(string name)
        {
            ForceState(name, true);
        }

        private static void ForceState(string name, bool toggle, bool state = true)
        {
            GameObject obj = Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name.Equals(name, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (obj == null)
            {
                DevCheats.Log($"[ForceState] Object not found...");
                return;
            }

            obj.SetActive(!toggle ? state : !obj.activeSelf);
        }
    }
}
