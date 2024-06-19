using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Atheism.Scripture
{
    [HarmonyPatch(typeof(Book))]
    [HarmonyPatch("AppendDoerRules")]
    public static class Patch_Book_AppendDoerRules
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == AtheismRefs.m_BookOutcomeDoer_OnBookGenerated)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_ScriptureUtility_AddTopicDynamicRules);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
