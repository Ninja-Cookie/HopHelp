using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Bind
    {
        internal const string USAGE_BIND = "[Bind] Usage: Bind [Key] \"[Full Command]; [Another Command]\"";

        [CheatMenu]
        public static void Bind(KeyCode key, string command)
        {
            command = string.IsNullOrEmpty(command) ? string.Empty : Regex.Replace(command.Trim(), " +", " ");
            string[] totalCommands = command.Split(';');

            List<StringHash> hash = new List<StringHash>();

            foreach (var singleCommand in totalCommands)
            {
                string[] parts = singleCommand.Trim().Split(' ');

                if (parts.Length == 0 || parts.First() == string.Empty || parts.First() == "♥")
                {
                    var existingKey = HopHelp.Bind.Commands.FirstOrDefault(x => x.Key == key);
                    string existingCommand = existingKey == null ? string.Empty : string.Join(" ", existingKey.ExecutedCommand);

                    DevCheats.Log(string.IsNullOrEmpty(existingCommand) ? $"[Bind] Error: A command was not provided...\r\n{USAGE_BIND}" : $"[Bind] {key} = {existingCommand}");
                    return;
                }

                List<DevCheats.AutocompleteResult> validCommands = DevCheats.Autocomplete(singleCommand);
                var isValidCommand = validCommands.Where(x => string.Equals(x.Words?.FirstOrDefault().ToString(), parts[0], StringComparison.OrdinalIgnoreCase)).Count() > 0;

                if (!isValidCommand)
                {
                    DevCheats.Log($"[Bind] Error: Invalid command ({singleCommand})...");
                    DevCheats.Log(USAGE_BIND);
                    return;
                }

                parts = parts.Append(";").ToArray();

                foreach (var part in parts)
                    hash.Add(new StringHash(part, 0, part.Length));
            }

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
