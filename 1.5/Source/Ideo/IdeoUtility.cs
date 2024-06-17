using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Atheism.Ideo
{
    public static class IdeoUtility
    {
        public static IdeoFoundationDef GetIdeoFoundationDef(IdeoGenerationParms parms)
        {
            if (parms.forcedMemes != null && parms.forcedMemes.Any(m => m.IsAtheism()))
            {
                return DefDatabase<IdeoFoundationDef>.AllDefs.Where(d => d.IsAtheism()).First();
            }
            else
            {
                return DefDatabase<IdeoFoundationDef>.AllDefs.Where(d => !d.IsAtheism()).RandomElement();
            }
        }

        public static IEnumerable<IdeoIconDef> GetAllNonAtheismIdeoIconDefs()
        {
            return DefDatabase<IdeoIconDef>.AllDefsListForReading.Where(d => !d.IsAtheism());
        }

        public static bool IsAtheism(this RimWorld.Ideo ideo)
        {
            return ideo.foundation is IdeoFoundation_Atheism;
        }

        public static bool IsAtheism(this IdeoFoundationDef def)
        {
            return def == DefDatabase<IdeoFoundationDef>.GetNamed("Atheism");
        }

        public static bool IsAtheism(this MemeDef def)
        {
            return def == DefDatabase<MemeDef>.GetNamed("Structure_Atheist");
        }

        public static bool IsAtheism(this IdeoIconDef def)
        {
            return def == DefDatabase<IdeoIconDef>.GetNamed("Atheism");
        }

        public static bool IsAtheism(this IdeoGenerationParms parms)
        {
            return parms.forcedMemes != null && parms.forcedMemes.Any(m => m.IsAtheism());
        }
    }
}
