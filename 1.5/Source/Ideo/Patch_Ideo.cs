using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace Atheism.Ideo
{
    // Don't return a ritual sound for atheism
    [HarmonyPatch(typeof(RimWorld.Ideo))]
    [HarmonyPatch("get_SoundOngoingRitual")]
    public static class Patch_Ideo_get_SoundOngoingRitual
    {
        public static void Postfix(RimWorld.Ideo __instance, ref SoundDef __result)
        {
            if (__instance.foundation is IdeoFoundation_Atheism)
            {
                __result = null;
            }
        }
    }

    // Always use white for atheism ideo color
    [HarmonyPatch(typeof(RimWorld.Ideo))]
    [HarmonyPatch("get_Color")]
    public static class Patch_Ideo_get_Color
    {
        public static bool Prefix(RimWorld.Ideo __instance, ref Color __result)
        {
            if (__instance.IsAtheism())
            {
                __result = Color.white;
                return false;
            }
            return true;
        }
    }

    // Don't try to fix atheism by adding "missing" precepts
    [HarmonyPatch(typeof(RimWorld.Ideo))]
    [HarmonyPatch(nameof(RimWorld.Ideo.ExposeData))]
    public static class Patch_Ideo_ExposeData
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundRitualSeat = false;
            bool finishedRitualSeat = false;
            bool foundVisible = false;
            bool finishedVisible = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AtheismRefs.m_Ideo_get_RitualSeatDef)
                {
                    foundRitualSeat = true;
                }

                if (foundRitualSeat && !finishedRitualSeat && instruction.opcode == OpCodes.Brtrue_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_IdeoUtility_IsAtheism_Ideo);
                    yield return instruction;
                    finishedRitualSeat = true;
                    continue;
                }

                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == AtheismRefs.f_PreceptDef_visible)
                {
                    foundVisible = true;
                }

                if (foundVisible && !finishedVisible && instruction.opcode == OpCodes.Brtrue_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AtheismRefs.m_IdeoUtility_IsAtheism_Ideo);
                    yield return instruction;
                    finishedVisible = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
