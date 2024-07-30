using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace MyFirstPlugin3
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly ConfigEntry<KeyCode> _hotKey;
        public Plugin()
        {

            _hotKey = Config.Bind("General", "HotKey", KeyCode.F10, "Press to activate mod");
        }

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(_hotKey.Value))
            {
                int pre = Money.ToCopper();
                Money.MinusPrice(-10000);
                int post = Money.ToCopper();
                Logger.LogInfo(string.Format("Added 1Gold {0} -> {1}", pre, post));

            }
        }

    }
}
