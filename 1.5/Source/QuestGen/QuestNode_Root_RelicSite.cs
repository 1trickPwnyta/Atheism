using RimWorld;
using RimWorld.Planet;
using RimWorld.QuestGen;
using System.Linq;
using Verse;

namespace Atheism.QuestGen
{
    public class QuestNode_Root_RelicSite : QuestNode
    {
        private const int MinDistanceFromColony = 2;
        private const int MaxDistanceFromColony = 10;

        protected override void RunInt()
        {
            Quest quest = RimWorld.QuestGen.QuestGen.quest;
            Slate slate = RimWorld.QuestGen.QuestGen.slate;
            //QuestGenUtility.RunAdjustPointsForDistantFight();
            Precept_Relic relic = slate.Get<Precept_Relic>("relic");
            Faction faction = slate.Get<Faction>("faction");
            TryFindSiteTile(out int tile);
            float points = StorytellerUtility.DefaultSiteThreatPointsNow();
            SitePartParams parms = new SitePartParams
            {
                points = points,
                threatPoints = points,
                relic = relic, 
                relicThing = relic.GenerateRelic()
            };
            Site site = QuestGen_Sites.GenerateSite(Gen.YieldSingle<SitePartDefWithParams>(new SitePartDefWithParams(DefDatabase<SitePartDef>.GetNamed("RelicSite"), parms)), tile, faction);
            slate.Set<Site>("site", site);
            quest.SpawnWorldObject(site);
        }

        protected override bool TestRunInt(Slate slate)
        {
            return TryFindSiteTile(out _);
        }

        private bool TryFindSiteTile(out int tile)
        {
            return TileFinder.TryFindNewSiteTile(out tile, MinDistanceFromColony, MaxDistanceFromColony);
        }
    }
}
