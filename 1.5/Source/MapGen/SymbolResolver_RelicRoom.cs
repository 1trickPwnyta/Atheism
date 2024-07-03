using RimWorld;
using RimWorld.BaseGen;
using RimWorld.SketchGen;
using System.Linq;
using Verse;
using Verse.AI.Group;

namespace Atheism.MapGen
{
    public class SymbolResolver_RelicRoom : SymbolResolver
    {
        public override void Resolve(RimWorld.BaseGen.ResolveParams rp)
        {
            Map map = BaseGen.globalSettings.map;
            IntVec3 center = rp.rect.CenterCell;

            RimWorld.SketchGen.ResolveParams sketchParams = default;
            sketchParams.sketch = new Sketch();
            sketchParams.monumentOpen = false;
            sketchParams.monumentSize = new IntVec2(rp.rect.Width, rp.rect.Height);
            sketchParams.allowMonumentDoors = true;
            sketchParams.allowWood = false;
            sketchParams.allowFlammableWalls = false;
            Sketch sketch = SketchGen.Generate(SketchResolverDefOf.Monument, sketchParams);
            sketch.Spawn(map, center, rp.faction, Sketch.SpawnPosType.Unchanged, Sketch.SpawnMode.Normal, true, true, null, false, true);
            CellRect biggestRect = SketchGenUtility.FindBiggestRect(sketch, x =>
            {
                if (sketch.TerrainAt(x) != null)
                {
                    return !sketch.ThingsAt(x).Any(t => t.def == ThingDefOf.Wall);
                }
                return false;
            }).MovedBy(center);
            
            if (!biggestRect.IsEmpty)
            {
                Thing reliquary = ThingMaker.MakeThing(ThingDefOf.Reliquary, BaseGenUtility.CheapStuffFor(ThingDefOf.Reliquary, rp.faction));
                reliquary.TryGetComp<CompThingContainer>().innerContainer.TryAdd(rp.relicThing);
                RimWorld.BaseGen.ResolveParams reliquaryParams = rp;
                reliquaryParams.rect = CellRect.CenteredOn(biggestRect.CenterCell, reliquary.def.Size.x, reliquary.def.Size.z);
                reliquaryParams.thingRot = Rot4.South;
                reliquaryParams.singleThingToSpawn = reliquary;
                BaseGen.symbolStack.Push("thing", reliquaryParams);
            }

            Lord lord;
            if ((lord = rp.singlePawnLord) == null)
            {
                lord = LordMaker.MakeNewLord(rp.faction, new LordJob_DefendBase(rp.faction, center, false), map);
            }
            rp.settlementLord = lord;
            TraverseParms traverseParms = TraverseParms.For(TraverseMode.PassDoors);
            RimWorld.BaseGen.ResolveParams pawnParams = rp;
            pawnParams.singlePawnLord = lord;
            pawnParams.pawnGroupKindDef = PawnGroupKindDefOf.Settlement;
            pawnParams.singlePawnSpawnCellExtraPredicate = rp.singlePawnSpawnCellExtraPredicate ?? (x => map.reachability.CanReachMapEdge(x, traverseParms));
            if (pawnParams.pawnGroupMakerParams == null)
            {
                pawnParams.pawnGroupMakerParams = new PawnGroupMakerParms();
                pawnParams.pawnGroupMakerParams.tile = map.Tile;
                pawnParams.pawnGroupMakerParams.faction = pawnParams.faction;
                pawnParams.pawnGroupMakerParams.points = pawnParams.settlementPawnGroupPoints ?? SymbolResolver_Settlement.DefaultPawnsPoints.RandomInRange;
                pawnParams.pawnGroupMakerParams.inhabitants = true;
                pawnParams.pawnGroupMakerParams.seed = pawnParams.settlementPawnGroupSeed;
            }
            BaseGen.symbolStack.Push("pawnGroup", pawnParams);

            BaseGen.symbolStack.Push("outdoorLighting", rp);

            RimWorld.BaseGen.ResolveParams defenseParams = rp;
            defenseParams.rect = sketch.OccupiedRect.MovedBy(center).ExpandedBy(2);
            defenseParams.edgeDefenseWidth = 2;
            defenseParams.edgeThingMustReachMapEdge = true;
            BaseGen.symbolStack.Push("edgeDefense", defenseParams);

            RimWorld.BaseGen.ResolveParams mapEdgeParams = rp;
            mapEdgeParams.rect = rp.rect.ContractedBy(2);
            BaseGen.symbolStack.Push("ensureCanReachMapEdge", mapEdgeParams);

            RimWorld.BaseGen.ResolveParams floorParams = rp;
            floorParams.floorDef = TerrainDefOf.Bridge;
            floorParams.floorOnlyIfTerrainSupports = true;
            floorParams.allowBridgeOnAnyImpassableTerrain = true;
            BaseGen.symbolStack.Push("floor", floorParams);


            if (ModsConfig.BiotechActive)
            {
                RimWorld.BaseGen.ResolveParams pollutionParams = rp;
                pollutionParams.rect = rp.rect.ExpandedBy(Rand.Range(1, 4));
                pollutionParams.edgeUnpolluteChance = 0.5f;
                BaseGen.symbolStack.Push("unpollute", pollutionParams);
            }
        }
    }
}
