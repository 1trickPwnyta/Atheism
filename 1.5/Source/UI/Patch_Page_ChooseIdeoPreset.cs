using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Atheism.UI
{
    // Add the atheism ideo preset into the dialog just before the preset category
    [HarmonyPatch(typeof(Page_ChooseIdeoPreset))]
    [HarmonyPatch(nameof(Page_ChooseIdeoPreset.DoWindowContents))]
    public static class Patch_Page_ChooseIdeoPreset_DoWindowContents
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo && (MethodInfo)instruction.operand == AtheismRefs.m_DefDatabase_IdeoPresetCategoryDef_get_AllDefsListForReading)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 7);
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 0);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_UIUtility_DoAtheismIdeoPresetSection);
                }

                yield return instruction;
            }
        }
    }

    // Don't show the atheism preset category after the other preset categories
    // Patched manually in mod constructor
    public static class Patch_Page_ChooseIdeoPreset_c_DoWindowContents_b__26_5
    {
        public static void Postfix(IdeoPresetCategoryDef c, ref bool __result)
        {
            if (c == DefDatabase<IdeoPresetCategoryDef>.GetNamed("Atheism"))
            {
                __result = false;
            }
        }
    }

    // Fix vanilla bug where selecting a preset ideo doesn't fix the page order
    /*
     * More info: If you select a custom ideo option first, it sets the character screen's prev page to the custom ideo page.
     * If you back out of the custom ideo page and select a different ideo preset, there could be a problem.
     * Classic mode has a line that sets the character screen's prev page back to the ideo preset page.
     * The preset ideo option does not do this, so if custom had been selected first, selecting a preset ideo after ward 
     * and then backing out of the character page will show you the custom ideo page, allowing you to customize the preset ideo.
     */
    [HarmonyPatch(typeof(Page_ChooseIdeoPreset))]
    [HarmonyPatch("DoPreset")]
    public static class Patch_Page_ChooseIdeoPreset_DoPreset
    {
        public static void Prefix(Page_ChooseIdeoPreset __instance)
        {
            __instance.next.prev = __instance;
        }
    }
}
