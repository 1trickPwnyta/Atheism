using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Atheism.UI
{
    // Don't show the atheism icon as an option
    [HarmonyPatch(typeof(Dialog_ChooseIdeoSymbols))]
    [HarmonyPatch("DoIconSelector")]
    public static class Patch_Dialog_ChooseIdeoSymbols
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo && (MethodInfo)instruction.operand == AtheismRefs.m_DefDatabase_IdeoIconDef_get_AllDefs)
                {
                    instruction.operand = AtheismRefs.m_IdeoUtility_GetAllNonAtheismIdeoIconDefs;
                }

                yield return instruction;
            }
        }
    }
}
