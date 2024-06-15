using HarmonyLib;
using UnityEngine;
using Verse;

namespace Atheism.Ideo
{
    // Don't return a ritual sound for atheism
    [HarmonyPatch(typeof(RimWorld.Ideo))]
    [HarmonyPatch("get_SoundOngoingRitual")]
    public static class Patch_Ideo_get_SoundOngoingRitual
    {
        public static void Postfix(RimWorld.Ideo __instance, ref SoundDef __result)
        {
            if (__instance.foundation is IdeoFoundation_Atheism)
            {
                __result = null;
            }
        }
    }

    // Always use white for atheism ideo color
    [HarmonyPatch(typeof(RimWorld.Ideo))]
    [HarmonyPatch("get_Color")]
    public static class Patch_Ideo_get_Color
    {
        public static bool Prefix(RimWorld.Ideo __instance, ref Color __result)
        {
            if (__instance.IsAtheism())
            {
                __result = Color.white;
                return false;
            }
            return true;
        }
    }
}
