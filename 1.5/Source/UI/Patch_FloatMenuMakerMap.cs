using Atheism.Jobs;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Atheism.UI
{
    [HarmonyPatch(typeof(FloatMenuMakerMap))]
    [HarmonyPatch("AddHumanlikeOrders")]
    public static class Patch_FloatMenuMakerMap
    {
        public static void Postfix(Vector3 clickPos, Pawn pawn, List<FloatMenuOption> opts)
        {
            if (pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation))
            {
                IntVec3 clickCell = IntVec3.FromVector3(clickPos);
                List<Thing> thingList = clickCell.GetThingList(pawn.Map);
                foreach (Thing thing in thingList)
                {
                    if (thing.IsRelic())
                    {
                        opts.Add(FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("Atheism_DestroyRelic".Translate(thing.Label, JobDriver_DestroyRelic.TicksToDestroy.ToStringTicksToPeriod()), delegate
                        {
                            Job job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("DestroyRelic"), thing);
                            job.count = 1;
                            pawn.jobs.TryTakeOrderedJob(job);
                        }), pawn, new LocalTargetInfo(thing)));
                    }

                    CompRelicContainer container = thing.TryGetComp<CompRelicContainer>();
                    if (container != null && container.Full)
                    {
                        opts.Add(FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("Atheism_DestroyRelic".Translate(container.ContainedThing.Label, JobDriver_DestroyRelic.TicksToDestroy.ToStringTicksToPeriod()), delegate
                        {
                            Job job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("DestroyRelic"), thing, container.ContainedThing);
                            job.count = 1;
                            pawn.jobs.TryTakeOrderedJob(job);
                        }), pawn, new LocalTargetInfo(thing)));
                    }
                }
            }
        }
    }
}
