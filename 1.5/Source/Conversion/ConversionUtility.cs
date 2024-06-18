using RimWorld;
using System;
using Verse;

namespace Atheism.Conversion
{
    public static class ConversionUtility
    {
        public static DiscoveryProgress GetDiscoveryProgress(this Precept precept)
        {
            return Atheism.Current.GetDiscoveryProgress(precept);
        }

        public static void SetDiscoveryProgress(this Precept precept, DiscoveryProgress progress)
        {
            Atheism.Current.SetDiscoveryProgress(precept, progress);
        }

        public static void IncrementDiscoveryProgress(this Precept precept)
        {
            Atheism.Current.IncrementDiscoveryProgress(precept);
        }

        public static string GetStatusString(this DiscoveryProgress progress)
        {
            switch (progress)
            {
                case DiscoveryProgress.Undiscovered: return "Atheism_Undiscovered".Translate();
                case DiscoveryProgress.One: case DiscoveryProgress.Two: return "Atheism_Progress".Translate((int)progress);
                case DiscoveryProgress.Discovered: return "Atheism_Discovered".Translate();
                case DiscoveryProgress.Destroyed: return "Atheism_Destroyed".Translate();
                default: throw new Exception("Undefined DiscoveryProgress: " + progress);
            }
        }
    }
}
