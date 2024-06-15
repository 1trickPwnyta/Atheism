#if DEBUG
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Atheism.Dev
{
    [HarmonyPatch(typeof(ScenPart_PlayerFaction))]
    [HarmonyPatch(nameof(ScenPart_PlayerFaction.PostWorldGenerate))]
    public static class Patch_ScenPart_PlayerFaction
    {
        public static bool Prefix(FactionDef ___factionDef)
        {
            IdeoGenerationParms parms = default(IdeoGenerationParms);
            parms.forcedMemes = new List<MemeDef>() { DefDatabase<MemeDef>.GetNamed("Structure_Atheist") };
            Find.GameInitData.playerFaction = FactionGenerator.NewGeneratedFaction(new FactionGeneratorParms(___factionDef, parms, null));
            Find.FactionManager.Add(Find.GameInitData.playerFaction);
            return false;
        }
    }
}
#endif
