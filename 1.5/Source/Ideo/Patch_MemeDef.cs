using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace Atheism.Ideo
{
    // Remove error for no worshipRoomLabel and/or no descriptionMaker
    // Patched manually in mod constructor
    public static class Patch_MemeDef
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundWorshipRoom = false;
            bool finishedWorshipRoom = false;
            bool foundDescriptionMaker = false;
            bool foundDescriptionMakerBrtrue = false;
            bool finishedDescriptionMaker = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == AtheismRefs.f_MemeDef_worshipRoomLabel)
                {
                    foundWorshipRoom = true;
                }

                if (foundWorshipRoom && !finishedWorshipRoom && instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    instruction.opcode = OpCodes.Br_S;
                    finishedWorshipRoom = true;
                }

                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == AtheismRefs.f_MemeDef_descriptionMaker)
                {
                    foundDescriptionMaker = true;
                }

                if (foundDescriptionMaker && !foundDescriptionMakerBrtrue && instruction.opcode == OpCodes.Brtrue_S)
                {
                    yield return instruction;
                    foundDescriptionMakerBrtrue = true;
                    continue;
                }

                if (foundDescriptionMakerBrtrue && !finishedDescriptionMaker)
                {
                    if (instruction.opcode == OpCodes.Br)
                    {
                        finishedDescriptionMaker = true;
                    }
                    else
                    {
                        instruction.opcode = OpCodes.Nop;
                        instruction.operand = null;
                    }
                }

                yield return instruction;
            }
        }
    }
}
