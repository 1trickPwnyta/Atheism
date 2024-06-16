using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Atheism.Thoughts
{
    // Fix vanilla bug that shows (ideoligion) as part of the social opinion
    /* 
     * More info: (ideoligion) is appended to the social thought description for thoughts related to precepts, 
     * but there is no check for whether classic mode is turned on. If classic mode is turned on, the concept 
     * of an ideoligion does not exist, and therefore it should not say this. Or maybe precept-related thoughts 
     * shouldn't be used at all in classic mode. I dunno.
     */
    [HarmonyPatch(typeof(ThoughtWorker_Precept_Social))]
    [HarmonyPatch(nameof(ThoughtWorker_Precept_Social.PostProcessLabel))]
    public static class Patch_ThoughtWorker_Precept_Social
    {
        public static void Postfix(Pawn p, ref string __result)
        {
            if (Find.IdeoManager.classicMode || p.Ideo == null || p.Ideo.IsAtheism())
            {
                __result = __result.Replace(" (" + "Ideo".Translate() + ")", "");
            }
        }
    }
}
