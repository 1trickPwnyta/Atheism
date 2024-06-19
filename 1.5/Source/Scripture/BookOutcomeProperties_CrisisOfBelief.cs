using RimWorld;
using System;

namespace Atheism.Scripture
{
    public class BookOutcomeProperties_CrisisOfBelief : BookOutcomeProperties
    {
        public override Type DoerClass
        {
            get
            {
                return typeof(ReadingOutcomeDoerCrisisOfBelief);
            }
        }
    }
}
