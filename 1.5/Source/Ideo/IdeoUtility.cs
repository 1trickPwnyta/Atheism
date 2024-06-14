using RimWorld;
using System.Linq;
using Verse;

namespace Atheism.Ideo
{
    public static class IdeoUtility
    {
        public static IdeoFoundationDef GetIdeoFoundationDef(IdeoGenerationParms parms)
        {
            if (parms.forcedMemes != null && parms.forcedMemes.Contains(DefDatabase<MemeDef>.GetNamed("Structure_Atheist")))
            {
                return DefDatabase<IdeoFoundationDef>.GetNamed("Atheism");
            }
            else
            {
                return DefDatabase<IdeoFoundationDef>.AllDefs.Where(d => d != DefDatabase<IdeoFoundationDef>.GetNamed("Atheism")).RandomElement();
            }
        }

        public static bool IsAtheism(this RimWorld.Ideo ideo)
        {
            return ideo.foundation is IdeoFoundation_Atheism;
        }
    }
}
