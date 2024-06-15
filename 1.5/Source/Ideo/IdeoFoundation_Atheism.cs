using RimWorld;
using System.Linq;
using Verse;

namespace Atheism.Ideo
{
    public class IdeoFoundation_Atheism : IdeoFoundation
    {
        public override void DoInfo(ref float curY, float width, IdeoEditMode editMode)
        {
            
        }

        public override void GenerateTextSymbols()
        {
            ideo.name = "Atheism_Atheism".Translate();
            ideo.adjective = "Atheism_AtheistAdjective".Translate();
            ideo.memberName = "Atheism_AtheistMemberName".Translate();
        }

        public override void Init(IdeoGenerationParms parms)
        {
            SetCulture(parms);
            SetStructure(parms);
            GenerateTextSymbols();
            ideo.description = parms.forcedMemes.First().description;
            ideo.SetIcon(DefDatabase<IdeoIconDef>.GetNamed("Atheism"), DefDatabase<ColorDef>.GetNamed("Structure_White"));
            SetClassicPrecepts();
        }

        private void SetCulture(IdeoGenerationParms parms)
        {
            if (parms.forFaction != null && parms.forFaction.allowedCultures != null)
            {
                ideo.culture = parms.forFaction.allowedCultures.RandomElement<CultureDef>();
                return;
            }
            ideo.culture = DefDatabase<CultureDef>.AllDefsListForReading.RandomElement<CultureDef>();
        }

        private void SetStructure(IdeoGenerationParms parms)
        {
            ideo.memes.Clear();
            ideo.memes.AddRange(parms.forcedMemes);
        }

        private void SetClassicPrecepts()
        {
            foreach (PreceptDef def in DefDatabase<PreceptDef>.AllDefsListForReading.Where(
                d => d.classic && 
                !typeof(Precept_Role).IsAssignableFrom(d.preceptClass) && 
                d.preceptClass != typeof(Precept_Ritual) && 
                d.preceptClass != typeof(Precept_Relic) && 
                d != DefDatabase<PreceptDef>.GetNamed("Nudity_Female_UncoveredGroinOrChestDisapproved") && 
                d != DefDatabase<PreceptDef>.GetNamed("MarriageName_UsuallyMans")))
            {
                ideo.AddPrecept(PreceptMaker.MakePrecept(def), true);
            }
            ideo.AddPrecept(PreceptMaker.MakePrecept(DefDatabase<PreceptDef>.GetNamed("Nudity_Female_UncoveredGroinDisapproved")), true);
            ideo.AddPrecept(PreceptMaker.MakePrecept(DefDatabase<PreceptDef>.GetNamed("MarriageName_Random")), true);
        }
    }
}
