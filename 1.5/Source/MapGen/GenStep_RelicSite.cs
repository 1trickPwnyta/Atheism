using RimWorld;
using RimWorld.BaseGen;
using System.Collections.Generic;
using Verse;

namespace Atheism.MapGen
{
    public class GenStep_RelicSite : GenStep
    {
        private static readonly IntVec2 size = new IntVec2(23, 15);

        public override int SeedPart => 398638182;

        public override void Generate(Map map, GenStepParams parms)
        {
            if (!MapGenerator.TryGetVar<List<CellRect>>("UsedRects", out List<CellRect> usedRects))
            {
                usedRects = new List<CellRect>();
                MapGenerator.SetVar<List<CellRect>>("UsedRects", usedRects);
            }

            ResolveParams resolveParams = default;
            resolveParams.rect = GetRelicRoomRect(map);
            resolveParams.relicThing = parms.sitePart.parms.relicThing;
            resolveParams.faction = map.ParentFaction;
            resolveParams.settlementPawnGroupPoints = parms.sitePart.parms.threatPoints;
            BaseGen.globalSettings.map = map;
            BaseGen.symbolStack.Push("relicRoom", resolveParams);
            BaseGen.Generate();

            usedRects.Add(resolveParams.rect);
        }

        private CellRect GetRelicRoomRect(Map map)
        {
            CellRect mapRect = new CellRect(0, 0, map.Size.x, map.Size.z);
            return CellRect.CenteredOn(mapRect.CenterCell, size);
        }
    }
}
