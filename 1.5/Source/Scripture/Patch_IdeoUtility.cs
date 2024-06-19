using HarmonyLib;
using RimWorld;
using Verse;

namespace Atheism.Scripture
{
    [HarmonyPatch(typeof(IdeoUtility))]
    [HarmonyPatch(nameof(IdeoUtility.IdeoChangeToWeight))]
    public static class Patch_IdeoUtility_IdeoChangeToWeight
    {
        public static void Postfix(Pawn pawn, RimWorld.Ideo ideo, ref float __result)
        {
            if (pawn.GetLastReadScriptureIdeo() == ideo)
            {
                __result *= 15f;
            }
        }
    }
}
