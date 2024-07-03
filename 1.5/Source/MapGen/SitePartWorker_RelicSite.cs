using RimWorld;
using RimWorld.Planet;
using Verse;

namespace Atheism.MapGen
{
    public class SitePartWorker_RelicSite : SitePartWorker
    {
        public override void Init(Site site, SitePart sitePart)
        {
            base.Init(site, sitePart);
            sitePart.relicThing = sitePart.parms.relicThing;
        }
    }
}
