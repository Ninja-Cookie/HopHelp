using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace HopHelp
{
    internal static class DataHandler
    {
        private readonly static string  PATH_Dir                = Paths.ConfigPath;
        private const string            PATH_FolderName         = "EnableDebugConsole";
        private const string            PATH_FileName           = "data";
        private const string            PATH_FileType           = "frog";
        internal const int              VERSION                 = 1;

        internal readonly static string PATH_FullDir     = Path.Combine(PATH_Dir, PATH_FolderName);
        internal readonly static string PATH_FullPath    = Path.Combine(PATH_FullDir, $"{PATH_FileName}.{PATH_FileType}");

        private readonly static DataContractSerializer Serializer = new DataContractSerializer(typeof(Data));

        [DataContract]
        private class CommandWrapper
        {
            [DataMember]
            private int Key;
            internal KeyCode KeyCode => (KeyCode)Key;

            [DataMember]
            private string[] Parts;
            internal List<StringHash> Command => Parts.Select(x => new StringHash(x, 0, x.Length)).ToList();

            internal CommandWrapper(KeyCode key, List<StringHash> command)
            {
                if (command == null || command.Count <= 0)
                    return;

                var parts = new List<string>();
                foreach (var part in command)
                    parts.Add(part.Text);

                Key     = (int)key;
                Parts   = parts.ToArray();
            }
        }

        [DataContract]
        private class Data
        {
            [DataMember]
            internal int Version = VERSION;

            [DataMember]
            internal Vector3[] SavedPositions = Array.Empty<Vector3>();

            [DataMember]
            internal CommandWrapper[] Binds = Array.Empty<CommandWrapper>();

            internal Data(Vector3[] savedPositions, CommandWrapper[] binds)
            {
                SavedPositions  = savedPositions;
                Binds           = binds;
            }
        }

        public static void Save()
        {
            Data data = new Data(ExtraCheats.Cheat_Positions.SavedPositions, Bind.Commands.Select(x => new CommandWrapper(x.Key, x.ExecutedCommand)).ToArray());

            try
            {
                if (!Directory.Exists(PATH_FullDir))
                    Directory.CreateDirectory(PATH_FullDir);

                if (Directory.Exists(PATH_FullDir))
                    using (FileStream stream = new FileStream(PATH_FullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                        Serializer.WriteObject(stream, data);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public static void Load()
        {
            if (!File.Exists(PATH_FullPath))
                return;

            try
            {
                Data data = null;
                using (FileStream stream = new FileStream(PATH_FullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    data = (Data)Serializer.ReadObject(stream);
                }

                if (data.Version != VERSION || data.SavedPositions == null || data.Binds == null)
                    return;

                ExtraCheats.Cheat_Positions.SavedPositions = data.SavedPositions;
                foreach (var bind in data.Binds)
                    Bind.Register(bind.KeyCode, bind.Command, false);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
