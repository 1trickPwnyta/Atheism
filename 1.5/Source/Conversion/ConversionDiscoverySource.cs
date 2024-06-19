using Atheism.Discovery;
using RimWorld;
using Verse;

namespace Atheism.Conversion
{
    public class ConversionDiscoverySource : IDiscoverySource
    {
        private Pawn converter;
        private Pawn convertee;

        public ConversionDiscoverySource(Pawn converter, Pawn convertee) 
        {
            this.converter = converter;
            this.convertee = convertee;
        }

        private TaggedString GetOpener(Precept precept)
        {
            return "Atheism_ConversionDiscoveryOpenerDesc".Translate(converter.Named("CONVERTER"), convertee.Named("CONVERTEE"), converter.Ideo.Named("IDEO"), precept.Named("PRECEPT"));
        }

        public TaggedString GetDiscoveredDescription(Precept precept)
        {
            return GetOpener(precept) + " " + "Atheism_ConversionDiscoveryDiscoveredDesc".Translate(converter.Named("CONVERTER"));
        }

        public TaggedString GetProgressDescription(Precept precept)
        {
            return GetOpener(precept) + " " + "Atheism_ConversionDiscoveryProgressDesc".Translate(precept.LabelCap);
        }

        public LookTargets GetLookTargets()
        {
            return converter;
        }
    }
}
