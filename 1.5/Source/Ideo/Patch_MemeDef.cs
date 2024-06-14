using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace Atheism.Ideo
{
    // Remove error for no worshipRoomLabel
    // Patched manually in mod constructor
    public static class Patch_MemeDef
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundWorshipRoom = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == AtheismRefs.f_MemeDef_worshipRoomLabel)
                {
                    foundWorshipRoom = true;
                }

                if (foundWorshipRoom && !finished && instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    instruction.opcode = OpCodes.Br_S;
                    finished = true;
                }

                yield return instruction;
            }
        }
    }
}
