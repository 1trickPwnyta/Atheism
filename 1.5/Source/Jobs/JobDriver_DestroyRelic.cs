using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Atheism.Jobs
{
    public class JobDriver_DestroyRelic : JobDriver
    {
        public const int TicksToDestroy = 300;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.GetTarget(TargetIndex.A), job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(TargetIndex.A);
            if (job.GetTarget(TargetIndex.B).IsValid)
            {
                this.FailOnDestroyedOrNull(TargetIndex.B);
            }
            yield return Toils_Goto.GotoThing(TargetIndex.A, job.GetTarget(TargetIndex.A).Thing.def.hasInteractionCell ? PathEndMode.InteractionCell : PathEndMode.Touch).FailOnDespawnedNullOrForbidden(TargetIndex.A).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
            yield return Toils_General.Wait(TicksToDestroy, TargetIndex.A);
            yield return Toils_General.Do(delegate
            {
                if (job.GetTarget(TargetIndex.B).IsValid)
                {
                    job.GetTarget(TargetIndex.A).Thing.TryGetComp<CompThingContainer>().innerContainer.TryDropAll(pawn.Position, pawn.Map, ThingPlaceMode.Near);
                    job.GetTarget(TargetIndex.B).Thing.Destroy(DestroyMode.KillFinalize);
                } 
                else
                {
                    job.GetTarget(TargetIndex.A).Thing.Destroy(DestroyMode.KillFinalize);
                }
            });
        }
    }
}
