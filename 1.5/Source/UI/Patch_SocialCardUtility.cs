using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Atheism.UI
{
    // Don't show role or role selection button in social card for atheism
    [HarmonyPatch(typeof(SocialCardUtility))]
    [HarmonyPatch(nameof(SocialCardUtility.DrawSocialCard))]
    public static class Patch_SocialCardUtility_DrawSocialCard
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundIdeology = false;
            bool foundLabel = false;
            bool foundCertainty = false;
            bool finished = false;

            Label endIdeologyLabel = default;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo && (MethodInfo)instruction.operand == AtheismRefs.m_ModsConfig_get_IdeologyActive)
                {
                    foundIdeology = true;
                }

                if (foundIdeology && !foundLabel && instruction.opcode == OpCodes.Brfalse)
                {
                    endIdeologyLabel = (Label)instruction.operand;
                    foundLabel = true;
                }

                if (instruction.opcode == OpCodes.Call && instruction.operand is MethodInfo && (MethodInfo)instruction.operand == AtheismRefs.m_SocialCardUtility_DrawPawnCertainty)
                {
                    foundCertainty = true;
                }

                if (foundCertainty && !finished && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == AtheismRefs.m_Pawn_get_Ideo)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_IdeoUtility_IsAtheism_Ideo);
                    yield return new CodeInstruction(OpCodes.Brtrue, endIdeologyLabel);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    finished = true;
                }

                yield return instruction;
            }
        }
    }
}
