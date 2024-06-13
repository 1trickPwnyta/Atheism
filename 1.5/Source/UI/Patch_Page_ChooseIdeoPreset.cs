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
}
