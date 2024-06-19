using RimWorld;
using Verse;
using Verse.Grammar;

namespace Atheism.Scripture
{
    public static class ScriptureUtility
    {
        public static void AddTopicDynamicRules(BookOutcomeDoer doer, GrammarRequest request)
        {
            if (doer is BookOutcomeDoerDynamic)
            {
                request.Rules.AddRange(((BookOutcomeDoerDynamic)doer).GetDynamicRules());
                request.Constants.AddRange(((BookOutcomeDoerDynamic)doer).GetConstants());
            }
        }

        public static RimWorld.Ideo GetLastReadScriptureIdeo(this Pawn pawn)
        {
            return Atheism.Current.GetLastReadScriptureIdeo(pawn);
        }

        public static void SetLastReadScriptureIdeo(this Pawn pawn, RimWorld.Ideo ideo)
        {
            Atheism.Current.SetLastReadScriptureIdeo(pawn, ideo);
        }
    }
}
