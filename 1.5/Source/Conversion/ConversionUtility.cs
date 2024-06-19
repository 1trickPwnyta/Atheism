using Atheism.Discovery;
using Atheism.Ideo;
using RimWorld;
using Verse;

namespace Atheism.Conversion
{
    public static class ConversionUtility
    {
        public static void TryIncrementDiscoveryProgress(Pawn converter, Pawn convertee, bool freshConvert, RimWorld.Ideo previousIdeo)
        {
            if (converter.Ideo.IsAtheism() && freshConvert)
            {
                Precept precept;
                if (previousIdeo.UndiscoveredPrecepts().TryRandomElement(out precept))
                {
                    precept.IncrementDiscoveryProgress(new ConversionDiscoverySource(converter, convertee));
                }
            }
        }
    }
}
