using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Poke
    {
        internal readonly static Assembly Assembly = typeof(PlayerItem).Assembly;

        [CheatMenu]
        public static void Poke(string chain)
        {
            object obj = TryChainCommand(chain.Split('.'));
            if (obj == null)
                return;

            DevCheats.Log(obj.ToString());
        }

        private static object TryChainCommand(params string[] types)
        {
            if (types.Length == 0)
                return null;

            try
            {
                var type = Assembly.GetType(types[0], false, true);
                if (type == null)
                {
                    DevCheats.Log($"\"{types[0]}\" was invalid...");
                    return null;
                }

                object obj = GameObject.FindFirstObjectByType(type, FindObjectsInactive.Include);
                if (obj == null)
                {
                    DevCheats.Log($"\"{type}\" could not be found...");
                    return null;
                }

                if (types.Length > 1)
                {
                    for (int i = 1; i < types.Length; i++)
                    {
                        obj = obj.GetValue<object>(types[i]);
                        if (obj == null)
                        {
                            DevCheats.Log($"\"{types[i]}\" could not be found...");
                            break;
                        }
                    }
                }

                return obj;
            }
            catch
            {
                return null;
            }
        }
    }
}
