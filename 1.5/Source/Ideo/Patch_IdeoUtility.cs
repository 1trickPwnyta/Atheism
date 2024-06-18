using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using RimWorld;

namespace Atheism.Ideo
{
    // Use the correct IdeoFoundationDef instead of a random one
    [HarmonyPatch(typeof(RimWorld.IdeoUtility))]
    [HarmonyPatch(nameof(RimWorld.IdeoUtility.MakeEmptyIdeo))]
    public static class Patch_IdeoUtility_MakeEmptyIdeo
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            LocalBuilder parms = il.DeclareLocal(typeof(IdeoGenerationParms));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AtheismRefs.m_GenCollection_RandomElement_IdeoFoundationDef)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldloca_S, parms);
                    yield return new CodeInstruction(OpCodes.Initobj, typeof(IdeoGenerationParms));
                    yield return new CodeInstruction(OpCodes.Ldloc_S, parms);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_IdeoUtility_GetIdeoFoundationDef);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
