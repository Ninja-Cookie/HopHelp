using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HopHelp
{
    internal static class Bind
    {
        internal readonly static List<Command> Commands = new List<Command>();

        internal class Command
        {
            internal KeyCode            Key             { get; }
            internal List<StringHash>   ExecutedCommand { get; }

            internal Command(KeyCode key, List<StringHash> command)
            {
                Key             = key;
                ExecutedCommand = command;
            }
        }

        internal static void Register(KeyCode key, List<StringHash> command)
        {
            RemoveExistingBind(key);
            Commands.Add(new Command(key, command));
            DataHandler.Save();
        }

        internal static bool Unregister(KeyCode key)
        {
            return RemoveExistingBind(key);
        }

        private static bool RemoveExistingBind(KeyCode key)
        {
            var existingBind    = Commands.FirstOrDefault(x => x.Key == key);
            var removed         = Commands.Remove(existingBind);
            DataHandler.Save();
            return removed;
        }

        internal static void RunCommands()
        {
            foreach (var command in Commands)
            {
                if (Input.GetKeyDown(command.Key))
                {
                    List<StringHash> toExecute = new List<StringHash>();
                    foreach (var hash in command.ExecutedCommand)
                    {
                        if (hash.Text != ";")
                        {
                            toExecute.Add(hash.Text);
                            continue;
                        }

                        DevCheats.ExecuteCommand(toExecute);
                        toExecute.Clear();
                    }
                }
            }
        }
    }
}
