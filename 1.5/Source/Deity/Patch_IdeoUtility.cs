using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Atheism.Deity
{
    // Assign memes to non-atheist ideologies within the constraints of the deity count setting
    [HarmonyPatch(typeof(RimWorld.IdeoUtility))]
    [HarmonyPatch(nameof(RimWorld.IdeoUtility.GenerateRandomMemes))]
    [HarmonyPatch(new[] { typeof(int), typeof(IdeoGenerationParms) })]
    public static class Patch_IdeoUtility_GenerateRandomMemes
    {
        private static Func<MemeDef, List<MemeDef>, FactionDef, bool, bool> canAdd = (meme, memes, forFaction, fluid) => (bool)typeof(RimWorld.IdeoUtility).Method("CanAdd").Invoke(null, new object[] { meme, memes, forFaction, fluid });
        private static Func<MemeDef, List<MemeDef>, bool> canAddDesperate = (meme, memes) => !memes.Contains(meme) && !meme.exclusionTags.Intersect(memes.SelectMany(m => m.exclusionTags)).Any();
        private static Func<MemeDef, bool> anyIdeoHas = meme => (bool)typeof(RimWorld.IdeoUtility).Method("AnyIdeoHas").Invoke(null, new[] { meme });
        private static Func<MemeDef, bool> withinDeityCountRange = meme => (meme.deityCount.min < 0 || meme.deityCount.min <= AtheismSettings.DeityCountRange.max) && (meme.deityCount.max < 0 || meme.deityCount.max >= AtheismSettings.DeityCountRange.min);

        public static bool Prefix(int count, IdeoGenerationParms parms, ref List<MemeDef> __result)
        {
            if (parms.IsAtheism())
            {
                return true;
            }

            bool forPlayerFaction = parms.forFaction != null && parms.forFaction.isPlayer;

            List<MemeDef> memes = new List<MemeDef>();

            if (parms.forFaction != null && parms.forFaction.requiredMemes != null)
            {
                memes.AddRange(parms.forFaction.requiredMemes.Where(m => withinDeityCountRange(m)));
                count -= memes.Count(m => m.category == MemeCategory.Normal);
            }

            if (parms.forcedMemes != null)
            {
                memes.AddRange(parms.forcedMemes.Where(m => withinDeityCountRange(m)));
            }

            if (!memes.Any(m => m.category == MemeCategory.Structure))
            {
                if (parms.forFaction != null && parms.forFaction.structureMemeWeights != null)
                {
                    MemeWeight memeWeight;
                    if (parms.forFaction.structureMemeWeights.Where(w => canAdd(w.meme, memes, parms.forFaction, parms.forNewFluidIdeo) && (forPlayerFaction || !anyIdeoHas(w.meme)) && withinDeityCountRange(w.meme)).TryRandomElementByWeight(w => w.selectionWeight * w.meme.randomizationSelectionWeightFactor, out memeWeight))
                    {
                        memes.Add(memeWeight.meme);
                    }
                    else if (parms.forFaction.structureMemeWeights.Where(w => canAdd(w.meme, memes, parms.forFaction, parms.forNewFluidIdeo) && withinDeityCountRange(w.meme)).TryRandomElementByWeight(w => w.selectionWeight * w.meme.randomizationSelectionWeightFactor, out memeWeight))
                    {
                        memes.Add(memeWeight.meme);
                    }
                }
            }

            if (!memes.Any(m => m.category == MemeCategory.Structure))
            {
                MemeDef meme;
                if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Structure && canAdd(m, memes, parms.forFaction, parms.forNewFluidIdeo) && (forPlayerFaction || !anyIdeoHas(m)) && withinDeityCountRange(m)).TryRandomElement(out meme))
                {
                    memes.Add(meme);
                }
                else if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Structure && canAdd(m, memes, parms.forFaction, parms.forNewFluidIdeo) && withinDeityCountRange(m)).TryRandomElementByWeight(m => m.randomizationSelectionWeightFactor, out meme))
                {
                    memes.Add(meme);
                }
                else if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Structure && canAddDesperate(m, memes) && withinDeityCountRange(m)).TryRandomElementByWeight(m => m.randomizationSelectionWeightFactor, out meme))
                {
                    memes.Add(meme);
                }
                else if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Structure && canAddDesperate(m, memes)).TryRandomElementByWeight(m => m.randomizationSelectionWeightFactor, out meme))
                {
                    memes.Add(meme);
                }
            }

            for (int i = 0; i < count; i++)
            {
                MemeDef meme;
                if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Normal && canAdd(m, memes, parms.forFaction, parms.forNewFluidIdeo) && (forPlayerFaction || !anyIdeoHas(m)) && (parms.disallowedMemes == null || !parms.disallowedMemes.Contains(m)) && withinDeityCountRange(m)).TryRandomElementByWeight(m => m.randomizationSelectionWeightFactor, out meme)) 
                {
                    memes.Add(meme);
                }
                else if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Normal && canAdd(m, memes, parms.forFaction, parms.forNewFluidIdeo) && (parms.disallowedMemes == null || !parms.disallowedMemes.Contains(m)) && withinDeityCountRange(m)).TryRandomElementByWeight(m => m.randomizationSelectionWeightFactor, out meme))
                {
                    memes.Add(meme);
                }
                else if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Normal && canAddDesperate(m, memes) && (parms.disallowedMemes == null || !parms.disallowedMemes.Contains(m)) && withinDeityCountRange(m)).TryRandomElementByWeight(m => m.randomizationSelectionWeightFactor, out meme))
                {
                    memes.Add(meme);
                }
                else if (DefDatabase<MemeDef>.AllDefsListForReading.Where(m => m.category == MemeCategory.Normal && canAddDesperate(m, memes) && (parms.disallowedMemes == null || !parms.disallowedMemes.Contains(m))).TryRandomElementByWeight(m => m.randomizationSelectionWeightFactor, out meme))
                {
                    memes.Add(meme);
                }
            }

            __result = memes;
            return false;
        }
    }
}
