using RimWorld;
using Verse;

namespace Atheism.Discovery
{
    public interface IDiscoverySource
    {
        TaggedString GetProgressDescription(Precept precept);

        TaggedString GetDiscoveredDescription(Precept precept);

        LookTargets GetLookTargets();
    }
}
