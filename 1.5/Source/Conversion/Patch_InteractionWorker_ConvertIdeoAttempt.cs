using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace Atheism.Conversion
{
    // Factor in atheism conversion power from settings
    [HarmonyPatch(typeof(InteractionWorker_ConvertIdeoAttempt))]
    [HarmonyPatch(nameof(InteractionWorker_ConvertIdeoAttempt.CertaintyReduction))]
    public static class Patch_InteractionWorker_ConvertIdeoAttempt_CertaintyReduction
    {
        public static void Postfix(Pawn initiator, ref float __result)
        {
            if (initiator.Ideo != null && initiator.Ideo.IsAtheism())
            {
                __result *= AtheismSettings.AtheismConversionPower;
            }
        }
    }

    // Increment discovery progress after converting to atheism
    [HarmonyPatch(typeof(InteractionWorker_ConvertIdeoAttempt))]
    [HarmonyPatch(nameof(InteractionWorker_ConvertIdeoAttempt.Interacted))]
    public static class Patch_InteractionWorker_ConvertIdeoAttempt_Interacted
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundConversionAttempt = false;
            bool finished = false;

            LocalBuilder freshConvert = il.DeclareLocal(typeof(bool));
            LocalBuilder previousIdeo = il.DeclareLocal(typeof(RimWorld.Ideo));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == AtheismRefs.m_Pawn_IdeoTracker_IdeoConversionAttempt)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_IdeoUtility_WasEverAtheist);
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                    yield return new CodeInstruction(OpCodes.Xor);
                    yield return new CodeInstruction(OpCodes.Stloc_S, freshConvert);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Callvirt, AtheismRefs.m_Pawn_get_Ideo);
                    yield return new CodeInstruction(OpCodes.Stloc_S, previousIdeo);
                    foundConversionAttempt = true;
                }

                if (foundConversionAttempt && !finished && instruction.opcode == OpCodes.Brfalse)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, freshConvert);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, previousIdeo);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_ConversionUtility_TryIncrementDiscoveryProgress);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
