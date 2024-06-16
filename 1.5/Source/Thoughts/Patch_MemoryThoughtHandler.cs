using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Atheism.Thoughts
{
    // Remove the source precept for atheist pawn memories
    [HarmonyPatch(typeof(MemoryThoughtHandler))]
    [HarmonyPatch(nameof(MemoryThoughtHandler.TryGainMemory))]
    [HarmonyPatch(new[] { typeof(Thought_Memory), typeof(Pawn) })]
    public static class Patch_MemoryThoughtHandler
    {
        public static void Prefix(MemoryThoughtHandler __instance, Thought_Memory newThought)
        {
            if (newThought.sourcePrecept != null && __instance.pawn.Ideo.IsAtheism() && __instance.pawn.Ideo.PreceptsListForReading.Contains(newThought.sourcePrecept))
            {
                newThought.sourcePrecept = null;
            }
        }
    }
}
