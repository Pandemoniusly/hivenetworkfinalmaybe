using HarmonyLib;
using Unity.Netcode;

namespace hivebombnetcode
{
    [HarmonyPatch(typeof(GameNetworkManager))]
    internal class HiveMindInterface
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        static void InterfaceHiveMind(ref GameNetworkManager __instance)
        {
            __instance.GetComponent<NetworkManager>().AddNetworkPrefab(Plugin.instance.HiveMindPrefab);
        }
    }
}