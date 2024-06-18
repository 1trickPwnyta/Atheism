using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Atheism.Dev
{
    // Use atheism ideology for dev quicktest
    [HarmonyPatch(typeof(ScenPart_PlayerFaction))]
    [HarmonyPatch(nameof(ScenPart_PlayerFaction.PostWorldGenerate))]
    public static class Patch_ScenPart_PlayerFaction
    {
        public static bool Prefix(FactionDef ___factionDef)
        {
            if (Prefs.DevMode && AtheismSettings.AtheismQuickTest)
            {
                IdeoGenerationParms parms = new IdeoGenerationParms(___factionDef)
                {
                    fixedIdeo = true,
                    forcedMemes = new List<MemeDef>() { DefDatabase<MemeDef>.GetNamed("Structure_Atheist") }
                };
                Find.GameInitData.playerFaction = FactionGenerator.NewGeneratedFaction(new FactionGeneratorParms(___factionDef, parms));
                Find.FactionManager.Add(Find.GameInitData.playerFaction);
                return false;
            }
            return true;
        }
    }
}
