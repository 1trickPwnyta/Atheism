using RimWorld;
using System;

namespace Atheism.Scripture
{
    public class BookOutcomeProperties_MakeDiscoveryProgress : BookOutcomeProperties
    {
        public override Type DoerClass
        {
            get
            {
                return typeof(ReadingOutcomeDoerMakeDiscoveryProgress);
            }
        }
    }
}
