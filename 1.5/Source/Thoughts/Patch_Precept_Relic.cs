using Atheism.Discovery;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Atheism.Thoughts
{
    [HarmonyPatch(typeof(Precept_Relic))]
    [HarmonyPatch(nameof(Precept_Relic.Notify_ThingLost))]
    public static class Patch_Precept_Relic
    {
        public static bool Prefix(Precept_Relic __instance, Thing thing, bool destroyed)
        {
            if (thing.def == __instance.ThingDef && !QuestPart_NewColony.IsGeneratingNewColony)
            {
                Find.HistoryEventsManager.RecordEvent(new HistoryEvent(destroyed ? HistoryEventDefOf.RelicDestroyed : HistoryEventDefOf.RelicLost, thing.Named("SUBJECT")));
                HashSet<Pawn> pawns = new HashSet<Pawn>();
                foreach (Pawn pawn in PawnsFinder.AllMapsAndWorld_Alive)
                {
                    if (pawn.needs != null && pawn.needs.mood != null && pawn.needs.mood.thoughts != null && pawn.needs.mood.thoughts.memories != null && !pawns.Contains(pawn)) 
                    {
                        if (pawn.Ideo == __instance.ideo)
                        {
                            if (pawn.Faction != Faction.OfPlayer || thing.EverSeenByPlayer)
                            {
                                pawn.needs.mood.thoughts.memories.TryGainMemory(destroyed ? ThoughtDefOf.RelicDestroyed : ThoughtDefOf.RelicLost);
                                if (pawn.Faction == Faction.OfPlayer)
                                {
                                    pawns.Add(pawn);
                                }
                            }
                        }
                    }
                }
                if (pawns.Any() && (destroyed || thing.MapHeld == null || !thing.MapHeld.IsPlayerHome))
                {
                    TaggedString label = (destroyed ? "LetterLabelRelicDestroyed" : "LetterLabelRelicLost").Translate() + ": " + __instance.LabelCap;
                    TaggedString text = (destroyed ? "LetterTextRelicDestroyed" : "LetterTextRelicLost").Translate(__instance.LabelCap, pawns.Select(p => p.LabelNoCountColored.Resolve()).ToList<string>().ToLineList("- "));
                    Find.LetterStack.ReceiveLetter(label, text, LetterDefOf.NegativeEvent, new LookTargets(pawns));
                }
                if (Atheism.Active && destroyed)
                {
                    __instance.SetDiscoveryProgress(DiscoveryProgress.Destroyed);
                }
            }

            return false;
        }
    }
}
