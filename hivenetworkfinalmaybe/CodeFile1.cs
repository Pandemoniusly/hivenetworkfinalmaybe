using HarmonyLib;
using System;
using GameNetcodeStuff;
using UnityEngine;
using Unity.Netcode;

namespace hivebombnetcode
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class HiveCreator
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void beehiveexplode(StartOfRound __instance)
        {
            if (__instance.IsHost)
            {
                GameObject TheHiveMindIsReal = GameObject.Instantiate(Plugin.instance.HiveMindPrefab);
                TheHiveMindIsReal.GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}