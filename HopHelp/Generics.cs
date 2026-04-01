using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HopHelp
{
    internal static class Generics
    {
        private     static PlayerItem _player = null;
        internal    static PlayerItem Player => _player == null || _player is null ? GetPlayer() : _player;

        private     static LoadManager _loadManager = null;
        internal    static LoadManager LoadManager => _loadManager == null || _loadManager is null ? GetLoadManager() : _loadManager;

        internal readonly static Shader TriggerMaterialShader   = Shader.Find("Particles/Standard Unlit");
        internal readonly static Shader WarpMaterialShader      = Shader.Find("GUI/Text Shader");

        private     static PanelDevCheatConsole _panelDevCheatConsole;
        private     static BigHopsPrefs         _bigHopsPrefs;
        internal    static bool DevPanelActive => GetPanelState();
        internal    static bool CheatsEnabled  => GetCheatState();

        private class ShaderCache
        {
            internal string     Name        { get; }
            internal Color      Color       { get; }
            internal Material   Material    { get; }

            internal ShaderCache(string name, Color color, Material material)
            {
                Name        = name;
                Color       = color;
                Material    = material;
            }
        }

        private static List<ShaderCache> ShaderCaches = new List<ShaderCache>();

        private static bool GetPanelState()
        {
            if (_panelDevCheatConsole != null)
                return _panelDevCheatConsole.gameObject.activeSelf;

            return (_panelDevCheatConsole = Singleton<UIManager>.Instance?.GetPanel<PanelDevCheatConsole>())?.gameObject.activeSelf ?? false;
        }

        private static bool GetCheatState()
        {
            if (_bigHopsPrefs != null)
                return _bigHopsPrefs.EnableCheats;

            return (_bigHopsPrefs = ScriptableObjectSingleton<BigHopsPrefs>.Instance)?.EnableCheats ?? false;
        }

        private static PlayerItem GetPlayer()
        {
            SingletonPropertyItem<PlayerManager>.Instance?.TryGetPlayer(out _player);
            return _player;
        }

        private static LoadManager GetLoadManager()
        {
            return _loadManager = SingletonPropertyItem<LoadManager>.Instance;
        }

        internal static Material GenerateMaterial(Shader shader, Color color)
        {
            ShaderCache cache = ShaderCaches.FirstOrDefault(x => x.Name == shader.name && x.Color == color);
            if (cache != default(ShaderCache))
                return cache.Material;

            var mat = new Material(shader);
            MakeMaterialTransparent(mat, color);
            ShaderCaches.Add(new ShaderCache(shader.name, color, mat));
            return mat;
        }

        private static void MakeMaterialTransparent(Material mat, Color color)
        {
            if (mat == null)
                return;

            mat.SetFloat("_Mode", 3);
            mat.SetInt  ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt  ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt  ("_ZWrite", 0);

            mat.DisableKeyword  ("_ALPHATEST_ON");
            mat.DisableKeyword  ("_ALPHAPREMULTIPLY_ON");
            mat.EnableKeyword   ("_ALPHABLEND_ON");

            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            if (mat.HasProperty("_Color"))
                mat.color = color;
        }
    }
}
