using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Bind
    {
        internal const string USAGE_BIND = "[Bind] Usage: Bind [Key] \"[Full Command]\"";

        [CheatMenu]
        public static void Bind(KeyCode key, string command)
        {
            command = string.IsNullOrEmpty(command) ? string.Empty : Regex.Replace(command.Trim(), " +", " ");
            string[] parts = command.Split(' ');

            if (parts.Length == 0 || parts.First() == string.Empty || parts.First() == "♥")
            {
                DevCheats.Log($"[Bind] Error: A command was not provided...");
                DevCheats.Log(USAGE_BIND);
                return;
            }

            List<DevCheats.AutocompleteResult> validCommands = DevCheats.Autocomplete(command);
            var isValidCommand = validCommands.Where(x => string.Equals(x.Words?.FirstOrDefault().ToString(), parts[0], StringComparison.OrdinalIgnoreCase)).Count() > 0;

            if (!isValidCommand)
            {
                DevCheats.Log($"[Bind] Error: Invalid command ({command})...");
                DevCheats.Log(USAGE_BIND);
                return;
            }

            List<StringHash> hash = new List<StringHash>();
            foreach (var part in parts)
                hash.Add(new StringHash(part, 0, part.Length));

            HopHelp.Bind.Register(key, hash);
        }

        [CheatMenu]
        public static void Unbind(KeyCode key)
        {
            if (HopHelp.Bind.Unregister(key))
                DevCheats.Log($"[Unbind] Key \"{key}\" removed...");
            else
                DevCheats.Log($"[Unbind] Error: Key \"{key}\" is not bound or is locked...");
        }
    }
}
