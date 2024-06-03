using HarmonyLib;
using System;
using GameNetcodeStuff;

namespace hivebombnetcode
{
    [HarmonyPatch(typeof(GrabbableObject))]
    internal class beeupdate
    {
        public static int Framecount = 0;
        public static readonly Random getrandom = new Random();

        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void beehiveexplode(GrabbableObject __instance)
        {
            if (GameNetworkManager.Instance.localPlayerController.isHostPlayerObject)
            {
                if (Config.Instance.Enabled.Value == true)
                {

                    if (__instance.name == "RedLocustHive(Clone)")
                    {

                        //finalhivebomb.Logger.LogInfo("found");
                        if (Framecount <= 0)
                        {
                            Framecount = 10;
                            if ((getrandom.Next(800) <= getrandom.Next(10)))
                            {
                                HiveMindManager.ExplodeAtServerRpc(__instance.transform, getrandom.Next(50), Config.Instance.KnockbackEnabled.Value, Config.Instance.VisibleExplosions.Value, Config.Instance.MaxPlayerDamage.Value, Config.Instance.Radius.Value);
                            }
                        }
                        else if (Framecount > 0)
                        {
                            Framecount -= 1;
                        }
                    }
                }
            }
        }
    }
}
