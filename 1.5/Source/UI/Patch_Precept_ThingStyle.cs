using Atheism.Discovery;
using Atheism.Ideo;
using HarmonyLib;
using RimWorld;

namespace Atheism.UI
{
    // Change the altar/ideogram and relic precept UI info when atheism is active
    [HarmonyPatch(typeof(Precept_ThingStyle))]
    [HarmonyPatch("get_UIInfoFirstLine")]
    public static class Patch_Precept_ThingStyle_get_UIInfoFirstLine
    {
        public static bool Prefix(Precept_ThingStyle __instance, ref string __result)
        {
            if (Atheism.Active && (__instance.IsAltar() || __instance.IsRelic()))
            {
                __result = __instance.LabelCap;
                return false;
            }
            return true;
        }
    }

    // Change the altar/ideogram and relic precept UI info when atheism is active
    [HarmonyPatch(typeof(Precept_ThingStyle))]
    [HarmonyPatch("get_UIInfoSecondLine")]
    public static class Patch_Precept_ThingStyle_get_UIInfoSecondLine
    {
        public static bool Prefix(Precept_ThingStyle __instance, ref string __result)
        {
            if (Atheism.Active && (__instance.IsAltar() || __instance.IsRelic()))
            {
                __result = __instance.GetDiscoveryProgress().GetStatusString();
                return false;
            }
            return true;
        }
    }
}
