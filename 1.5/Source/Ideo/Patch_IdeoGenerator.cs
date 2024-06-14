﻿using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Atheism.Ideo
{
    // Use the correct IdeoFoundationDef instead of a random one
    [HarmonyPatch(typeof(IdeoGenerator))]
    [HarmonyPatch(nameof(IdeoGenerator.GenerateIdeo))]
    public static class Patch_IdeoGenerator
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AtheismRefs.m_GenCollection_RandomElement_IdeoFoundationDef)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_IdeoUtility_GetIdeoFoundationDef);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
