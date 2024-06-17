using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace Atheism.IdeologicalVariation
{
    // For non-player factions, allow a pawn to spawn with any ideology, but much more likely with the faction's primary ideology
    [HarmonyPatch(typeof(FactionIdeosTracker))]
    [HarmonyPatch(nameof(FactionIdeosTracker.GetRandomIdeoForNewPawn))]
    public static class Patch_FactionIdeosTracker
    {
        public static bool Prefix(FactionIdeosTracker __instance, Faction ___faction, ref RimWorld.Ideo __result)
        {
            if (!___faction.IsPlayer)
            {
                __result = __instance.PrimaryIdeo;
                if (Rand.Chance(AtheismSettings.IdeologicalVariationChance))
                {
                    __result = Find.IdeoManager.IdeosListForReading.Where(i => i != __instance.PrimaryIdeo).RandomElement();
                }
                return false;
            }
            return true;
        }
    }
}
