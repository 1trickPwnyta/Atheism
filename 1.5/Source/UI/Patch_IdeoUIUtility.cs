using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Atheism.UI
{
    // Don't show leader title and ritual room in tooltip if undefined
    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch(nameof(IdeoUIUtility.DoNameAndSymbol))]
    public static class Patch_IdeoUIUtility_DoNameAndSymbol
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo && (MethodInfo)instruction.operand == AtheismRefs.m_TooltipHandler_TipRegion)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Ldarg_3);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_UIUtility_GetNameAndSymbolTooltip);
                }

                yield return instruction;
            }
        }
    }

    // Don't show the save/load buttons for atheism ideo
    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch(nameof(IdeoUIUtility.DoIdeoSaveLoad))]
    public static class Patch_IdeoUIUtility_DoIdeoSaveLoad
    {
        public static bool Prefix(ref float curY, RimWorld.Ideo ideo)
        {
            if (ideo.IsAtheism())
            {
                curY += 4f + 30f + 4f;
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    // Ignore edit mode for atheism ideo
    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch(nameof(IdeoUIUtility.DoIdeoDetails))]
    public static class Patch_IdeoUIUtility_DoIdeoDetails
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundEditMode = false;
            bool finished = false;

            Label notAtheismLabel = il.DefineLabel();

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo && (MethodInfo)instruction.operand == AtheismRefs.m_IdeoUIUtility_get_DevEditMode)
                {
                    foundEditMode = true;
                }

                if (foundEditMode && !finished && instruction.opcode == OpCodes.Ldloc_0)
                {
                    CodeInstruction loadIdeoInstruction = new CodeInstruction(OpCodes.Ldarg_1);
                    loadIdeoInstruction.labels.AddRange(instruction.labels);
                    yield return loadIdeoInstruction;
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_IdeoUtility_IsAtheism_Ideo);
                    yield return new CodeInstruction(OpCodes.Brfalse_S, notAtheismLabel);
                    yield return new CodeInstruction(OpCodes.Ldc_I4_0);
                    yield return new CodeInstruction(OpCodes.Stloc_1);
                    instruction.labels.Clear();
                    instruction.labels.Add(notAtheismLabel);
                    finished = true;
                }

                yield return instruction;
            }
        }
    }

    // Don't show the debug buttons for atheism ideo
    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch("DoDebugButtons")]
    public static class Patch_IdeoUIUtility_DoDebugButtons
    {
        public static bool Prefix(RimWorld.Ideo ideo)
        {
            return !ideo.IsAtheism();
        }
    }

    // Don't show any precepts for atheism
    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch(nameof(IdeoUIUtility.DoPrecepts))]
    public static class Patch_IdeoUIUtility_DoPrecepts
    {
        public static bool Prefix(RimWorld.Ideo ideo)
        {
            return !ideo.IsAtheism();
        }
    }

    // Don't show any appearance options for atheism
    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch(nameof(IdeoUIUtility.DoAppearanceItems))]
    public static class Patch_IdeoUIUtility_DoAppearanceItems
    {
        public static bool Prefix(RimWorld.Ideo ideo)
        {
            return !ideo.IsAtheism();
        }
    }
}
