using BepInEx;
using HarmonyLib;

namespace HopHelp
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string pluginGuid      = "ninjacookie.hops.hophelp";
        public const string pluginName      = "Hop Help";
        public const string pluginVersion   = "1.0.1";

        public void Awake()
        {
            var harmony = new Harmony(pluginGuid);
            harmony.PatchAll();

            DataHandler.Load();
        }
    }
}
