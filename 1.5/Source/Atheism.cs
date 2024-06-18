using Atheism.Conversion;
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

        public Atheism(Game game)
        {
            
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref discoveryProgress, "discoveryProgress", LookMode.Reference, LookMode.Value, ref workingPreceptList, ref workingDiscoveryProgressList);
        }

        public DiscoveryProgress GetDiscoveryProgress(Precept precept)
        {
            if (discoveryProgress.ContainsKey(precept))
            {
                return discoveryProgress[precept];
            }
            return DiscoveryProgress.Undiscovered;
        }

        public void SetDiscoveryProgress(Precept precept, DiscoveryProgress progress)
        {
            discoveryProgress[precept] = progress;
        }

        public void IncrementDiscoveryProgress(Precept precept)
        {
            if (!discoveryProgress.ContainsKey(precept))
            {
                discoveryProgress[precept] = DiscoveryProgress.Undiscovered;
            }
            if (discoveryProgress[precept] < DiscoveryProgress.Destroyed)
            {
                discoveryProgress[precept]++;
            }
        }
    }
}
