using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Atheism.Thoughts
{
    // Remove the source precept for atheist pawn situational thoughts
    [HarmonyPatch(typeof(SituationalThoughtHandler))]
    [HarmonyPatch("TryCreateThought")]
    public static class Patch_SituationalThoughtHandler_TryCreateThought
    {
        public static void Postfix(SituationalThoughtHandler __instance, Thought_Situational __result)
        {
            if (__result != null && __result.sourcePrecept != null && __instance.pawn.ideo.Ideo.IsAtheism() && __instance.pawn.ideo.Ideo.PreceptsListForReading.Contains(__result.sourcePrecept))
            {
                __result.sourcePrecept = null;
            }
        }
    }

    // Remove the source precept for atheist pawn situational thoughts
    [HarmonyPatch(typeof(SituationalThoughtHandler))]
    [HarmonyPatch("TryCreateSocialThought")]
    public static class Patch_SituationalThoughtHandler_TryCreateSocialThought
    {
        public static void Postfix(SituationalThoughtHandler __instance, Thought_SituationalSocial __result)
        {
            if (__result != null && __result.sourcePrecept != null && __instance.pawn.ideo.Ideo.IsAtheism() && __instance.pawn.ideo.Ideo.PreceptsListForReading.Contains(__result.sourcePrecept))
            {
                __result.sourcePrecept = null;
            }
        }
    }
}
