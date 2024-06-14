using HarmonyLib;
using Verse;

namespace Atheism.Ideo
{
    // Don't return a ritual sound for atheism
    [HarmonyPatch(typeof(RimWorld.Ideo))]
    [HarmonyPatch("get_SoundOngoingRitual")]
    public static class Patch_Ideo
    {
        public static void Postfix(RimWorld.Ideo __instance, ref SoundDef __result)
        {
            if (__instance.foundation is IdeoFoundation_Atheism)
            {
                __result = null;
            }
        }
    }
}
