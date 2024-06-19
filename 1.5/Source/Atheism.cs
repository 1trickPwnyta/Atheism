using Atheism.Discovery;
using Atheism.Ideo;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Atheism
{
    public class Atheism : GameComponent
    {
        public static Atheism Current
        {
            get
            {
                return Verse.Current.Game.GetComponent<Atheism>();
            }
        }

        public static bool Active
        {
            get
            {
                return Find.IdeoManager.IdeosListForReading.Any(i => i.IsAtheism());
            }
        }

        private Dictionary<Precept, DiscoveryProgress> discoveryProgress = new Dictionary<Precept, DiscoveryProgress>();
        private List<Precept> workingPreceptList;
        private List<DiscoveryProgress> workingDiscoveryProgressList;
        private Dictionary<Pawn, RimWorld.Ideo> lastReadScriptureIdeo = new Dictionary<Pawn, RimWorld.Ideo>();

        public Atheism(Game game)
        {
            
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref discoveryProgress, "discoveryProgress", LookMode.Reference, LookMode.Value, ref workingPreceptList, ref workingDiscoveryProgressList);
            Scribe_Collections.Look(ref lastReadScriptureIdeo, "lastReadScriptureIdeo", LookMode.Reference, LookMode.Reference);
        }

        public DiscoveryProgress GetDiscoveryProgress(Precept precept)
        {
            if (discoveryProgress.ContainsKey(precept))
            {
                return discoveryProgress[precept];
            }
            return DiscoveryProgress.Undiscovered;
        }

        public void SetDiscoveryProgress(Precept precept, DiscoveryProgress progress, IDiscoverySource source = null)
        {
            discoveryProgress[precept] = progress;
            if (progress > DiscoveryProgress.Undiscovered)
            {
                Find.LetterStack.ReceiveLetter(progress.GetLetterLabel(precept), progress.GetLetterText(precept, source), progress.GetLetterDef(), source?.GetLookTargets());
            }
        }

        public void IncrementDiscoveryProgress(Precept precept, IDiscoverySource source = null)
        {
            if (!discoveryProgress.ContainsKey(precept))
            {
                discoveryProgress[precept] = DiscoveryProgress.Undiscovered;
            }
            if (discoveryProgress[precept] < DiscoveryProgress.Destroyed)
            {
                SetDiscoveryProgress(precept, discoveryProgress[precept] + 1, source);
            }
        }

        public RimWorld.Ideo GetLastReadScriptureIdeo(Pawn pawn)
        {
            if (lastReadScriptureIdeo.ContainsKey(pawn))
            {
                return lastReadScriptureIdeo[pawn];
            }
            else
            {
                return null;
            }
        }

        public void SetLastReadScriptureIdeo(Pawn pawn, RimWorld.Ideo ideo)
        {
            lastReadScriptureIdeo[pawn] = ideo;
        }
    }
}
