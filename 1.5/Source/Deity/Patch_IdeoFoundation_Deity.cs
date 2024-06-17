using HarmonyLib;
using RimWorld;
using System.Linq;

namespace Atheism.Deity
{
    // Only worry about duplicate deity names within the same ideology
    [HarmonyPatch(typeof(IdeoFoundation_Deity))]
    [HarmonyPatch("<FillDeity>b__19_3")]
    public static class Patch_IdeoFoundation_Deity_FillDeity_b__19_3
    {
        public static bool Prefix(IdeoFoundation_Deity __instance, string x, ref bool __result)
        {
            __result = !__instance.DeitiesListForReading.Any(d => d.name == x);
            return false;
        }
    }
}
