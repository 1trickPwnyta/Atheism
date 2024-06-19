using Atheism.Discovery;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Atheism.Dev
{
    public static class DevUtility
    {
        public static List<FloatMenuOption> GetDiscoveryProgressOptions(Precept precept)
        {
            List<FloatMenuOption> options = new List<FloatMenuOption>();
            DiscoveryProgress progress = precept.GetDiscoveryProgress();
            if (progress < DiscoveryProgress.Destroyed) 
            {
                options.Add(new FloatMenuOption("Atheism_DevIncrementDiscoveryProgress".Translate(), delegate
                {
                    precept.IncrementDiscoveryProgress();
                }));
            }
            if (progress < DiscoveryProgress.Discovered)
            {
                options.Add(new FloatMenuOption("Atheism_DevDiscover".Translate(), delegate
                {
                    precept.SetDiscoveryProgress(DiscoveryProgress.Discovered);
                }));
            }
            if (progress < DiscoveryProgress.Destroyed)
            {
                options.Add(new FloatMenuOption("Atheism_DevDestroy".Translate(), delegate
                {
                    precept.SetDiscoveryProgress(DiscoveryProgress.Destroyed);
                }));
            }
            return options;
        }
    }
}
