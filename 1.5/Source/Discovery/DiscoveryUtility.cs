using RimWorld;
using Verse;

namespace Atheism.Discovery
{
    public static class DiscoveryUtility
    {
        public static DiscoveryProgress GetDiscoveryProgress(this Precept precept)
        {
            return Atheism.Current.GetDiscoveryProgress(precept);
        }

        public static void SetDiscoveryProgress(this Precept precept, DiscoveryProgress progress, IDiscoverySource source = null)
        {
            Atheism.Current.SetDiscoveryProgress(precept, progress, source);
        }

        public static void IncrementDiscoveryProgress(this Precept precept, IDiscoverySource source = null)
        {
            Atheism.Current.IncrementDiscoveryProgress(precept, source);
        }

        public static string GetStatusString(this DiscoveryProgress progress)
        {
            switch (progress)
            {
                case DiscoveryProgress.Undiscovered: return "Atheism_Undiscovered".Translate();
                case DiscoveryProgress.Discovered: return "Atheism_Discovered".Translate();
                case DiscoveryProgress.Destroyed: return "Atheism_Destroyed".Translate();
                default: return "Atheism_Progress".Translate((int)progress, (int)DiscoveryProgress.Discovered);
            }
        }

        public static string GetLetterLabel(this DiscoveryProgress progress, Precept precept)
        {
            switch (progress)
            {
                case DiscoveryProgress.Discovered: return "Atheism_DiscoveredLetterLabel".Translate(precept.LabelCap);
                case DiscoveryProgress.Destroyed: return "Atheism_DestroyedLetterLabel".Translate(precept.LabelCap);
                default: return "Atheism_ProgressLetterLabel".Translate(precept.LabelCap, (int)progress, (int)DiscoveryProgress.Discovered);
            }
        }

        public static TaggedString GetLetterText(this DiscoveryProgress progress, Precept precept, IDiscoverySource source = null)
        {
            switch (progress)
            {
                case DiscoveryProgress.Discovered: return source != null ? source.GetDiscoveredDescription(precept) : "Atheism_DiscoveredLetterText".Translate(precept.LabelCap);
                case DiscoveryProgress.Destroyed: return "Atheism_DestroyedLetterText".Translate(precept.Named("PRECEPT"), precept.ideo.Named("IDEO"));
                default: return source != null ? source.GetProgressDescription(precept) : "Atheism_ProgressLetterText".Translate(precept.LabelCap);
            }
        }

        public static LetterDef GetLetterDef(this DiscoveryProgress progress)
        {
            switch (progress)
            {
                case DiscoveryProgress.Discovered: case DiscoveryProgress.Destroyed: return LetterDefOf.PositiveEvent;
                default: return LetterDefOf.NeutralEvent;
            }
        }
    }
}
