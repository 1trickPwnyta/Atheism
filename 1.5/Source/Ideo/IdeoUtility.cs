using Atheism.Discovery;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.Grammar;

namespace Atheism.Ideo
{
    public static class IdeoUtility
    {
        public static IdeoFoundationDef GetIdeoFoundationDef(IdeoGenerationParms parms)
        {
            if (parms.IsAtheism())
            {
                return DefDatabase<IdeoFoundationDef>.AllDefs.Where(d => d.IsAtheism()).First();
            }
            else
            {
                return DefDatabase<IdeoFoundationDef>.AllDefs.Where(d => !d.IsAtheism()).RandomElement();
            }
        }

        public static IEnumerable<IdeoIconDef> GetAllNonAtheismIdeoIconDefs()
        {
            return DefDatabase<IdeoIconDef>.AllDefsListForReading.Where(d => !d.IsAtheism());
        }

        public static bool IsAtheism(this RimWorld.Ideo ideo)
        {
            return ideo.foundation is IdeoFoundation_Atheism;
        }

        public static bool IsAtheism(this IdeoFoundationDef def)
        {
            return def == DefDatabase<IdeoFoundationDef>.GetNamed("Atheism");
        }

        public static bool IsAtheism(this MemeDef def)
        {
            return def == DefDatabase<MemeDef>.GetNamed("Structure_Atheist");
        }

        public static bool IsAtheism(this IdeoIconDef def)
        {
            return def == DefDatabase<IdeoIconDef>.GetNamed("Atheism");
        }

        public static bool IsAtheism(this IdeoGenerationParms parms)
        {
            return parms.forcedMemes != null && parms.forcedMemes.Any(m => m.IsAtheism());
        }

        public static bool IsAltar(this Precept precept)
        {
            return precept is Precept_Building && ((Precept_Building)precept).ThingDef.isAltar;
        }

        public static bool IsRelic(this Precept precept)
        {
            return precept is Precept_Relic;
        }

        public static bool AllowedByAtheism(this PreceptDef d)
        {
            return (
                d.classic &&
                !typeof(Precept_Role).IsAssignableFrom(d.preceptClass) &&
                d.preceptClass != typeof(Precept_Relic) &&
                !new List<string>()
                {
                    "Nudity_Female_UncoveredGroinOrChestDisapproved",
                    "MarriageName_UsuallyMans",
                    "LeaderSpeech",
                    "Trial",
                    "TrialPrisoner",
                    "TrialMentalState",
                    "Conversion",
                    "Execution",
                    "RoleChange",
                    "Funeral",
                    "FuneralNoCorpse",
                    "Classic_DrumParty",
                    "Classic_DanceParty"
                }.Contains(d.defName)) || 
                new List<string>()
                {
                    "Nudity_Female_UncoveredGroinDisapproved",
                    "MarriageName_Random"
                }.Contains(d.defName);
        }

        public static bool WasEverAtheist(this Pawn pawn)
        {
            return pawn.ideo != null && pawn.ideo.PreviousIdeos != null && pawn.ideo.PreviousIdeos.Any(i => i.IsAtheism());
        }

        public static List<Precept> UndiscoveredPrecepts(this RimWorld.Ideo ideo)
        {
            return ideo.PreceptsListForReading.Where(p => (p.IsAltar() || p.IsRelic()) && p.GetDiscoveryProgress() < DiscoveryProgress.Discovered).ToList();
        }

        public static Faction GetHostFaction(this RimWorld.Ideo ideo)
        {
            return Find.FactionManager.AllFactionsListForReading.Where(f => !f.IsPlayer && f.ideos.PrimaryIdeo == ideo).FirstOrDefault();
        }

        public static List<RimWorld.Ideo> GetAdversarialIdeos()
        {
            return Find.IdeoManager.IdeosInViewOrder.Where(i => !i.IsAtheism() && i.GetHostFaction() != null && !i.GetHostFaction().Hidden).ToList();
        }
    }
}
