using RimWorld;
using System.Collections.Generic;
using Verse.Grammar;

namespace Atheism.Scripture
{
    public abstract class BookOutcomeDoerDynamic : BookOutcomeDoer
    {
        public abstract List<Rule> GetDynamicRules();

        public virtual IDictionary<string, string> GetConstants()
        {
            return new Dictionary<string, string>();
        }
    }
}
