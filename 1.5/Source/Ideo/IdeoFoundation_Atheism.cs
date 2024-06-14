﻿using RimWorld;
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
    }
}
