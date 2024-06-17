using Atheism.Ideo;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace Atheism.Deity
{
    // Force non-atheist ideo deity count within range in settings
    [HarmonyPatch(typeof(RimWorld.Ideo))]
    [HarmonyPatch("get_DeityCountRange")]
    public static class Patch_Ideo_get_DeityCountRange
    {
        public static void Postfix(RimWorld.Ideo __instance, ref IntRange __result)
        {
            if (!__instance.IsAtheism())
            {
                __result.min = Mathf.Max(__result.min, AtheismSettings.DeityCountRange.min);
                __result.max = Mathf.Min(__result.max, AtheismSettings.DeityCountRange.max);
            }
        }
    }
}
