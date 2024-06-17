using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Atheism.Conversion
{
    [HarmonyPatch(typeof(InteractionWorker_ConvertIdeoAttempt))]
    [HarmonyPatch(nameof(InteractionWorker_ConvertIdeoAttempt.CertaintyReduction))]
    public static class Patch_InteractionWorker_ConvertIdeoAttempt_CertaintyReduction
    {
        public static void Postfix(Pawn initiator, ref float __result)
        {
            if (initiator.Ideo != null && initiator.Ideo.IsAtheism())
            {
                __result *= AtheismSettings.AtheismConversionPower;
            }
        }
    }
}
