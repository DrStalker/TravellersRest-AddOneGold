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
                // Use reflection to get the Money class
                var moneyType = typeof(Money);

                // Get the static field OLKBFCHCMMA (even if it's private)
                var fieldInfo = moneyType.GetField("OLKBFCHCMMA", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

                if (fieldInfo != null)
                {
                    var moneyInstance = fieldInfo.GetValue(null); // Get the instance of Money

                    if (moneyInstance != null)
                    {
                        // Use reflection to get the balance field
                        var balanceField = moneyType.GetField("balance", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                        if (balanceField != null)
                        {
                            var balanceInstance = balanceField.GetValue(moneyInstance);

                            // Use reflection to access the Gold property
                            var goldProperty = balanceInstance.GetType().GetProperty("Gold");
                            if (goldProperty != null)
                            {
                                int preGold = (int)goldProperty.GetValue(balanceInstance);
                                goldProperty.SetValue(balanceInstance, preGold + 1); // Add 1 gold
                                int postGold = (int)goldProperty.GetValue(balanceInstance);

                                Logger.LogInfo($"Added 1 Gold: {preGold} -> {postGold}");
                            }
                            else
                            {
                                Logger.LogError("Could not find Gold property.");
                            }
                        }
                        else
                        {
                            Logger.LogError("Could not find balance field in Money.");
                        }
                    }
                    else
                    {
                        Logger.LogError("Money.OLKBFCHCMMA is null!");
                    }
                }
                else
                {
                    Logger.LogError("Could not find OLKBFCHCMMA field in Money class.");
                }
            }
        }

    }
}
