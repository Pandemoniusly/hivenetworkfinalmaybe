using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

namespace hivebombnetcode
{
    [BepInPlugin(modGUID, modName, modVersion)]
    //    [BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string modGUID = "Pandemonius.BeehiveBomb";
        public const string modName = "BeehiveBomb";
        public const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        static internal Plugin instance;

        static internal ManualLogSource mls;

        public static ConfigFile BepInExConfig()
        {
            return instance.Config;
        }

        public static AssetBundle bundle;

        public GameObject HiveMindPrefab;

        public void Awake()
        {
            // entry point when mod load
            instance = this;
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }

            string assetlocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "hivebombing");
            bundle = AssetBundle.LoadFromFile(assetlocation);

            HiveMindPrefab = bundle.LoadAsset<GameObject>("Assets/TheHiveMind.prefab");
            HiveMindPrefab.AddComponent<HiveMindManager>();

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            hivebombnetcode.Config.Instance.Setup();
            mls.LogMessage("Plugin " + modName + " loaded!");
            harmony.PatchAll(typeof(beeupdate));
        }

        //  [HarmonyPatch]
        //  public class NetworkObjectManager
        //  {
        //
        //      [HarmonyPostfix, HarmonyPatch(typeof(GameNetworkManager), nameof(GameNetworkManager.Start))]
        //      public static void Init()
        //      {
        //          if (networkPrefab != null)
        //             return;
        //          
        //          networkPrefab = (GameObject)MainAssetBundle.LoadAsset("ExampleNetworkHandler");
        //      }
        //
        //      static GameObject networkPrefab;
        //  }
    }
}