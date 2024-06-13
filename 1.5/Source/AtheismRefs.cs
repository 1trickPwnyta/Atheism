using Atheism.UI;
using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

namespace Atheism
{
    public static class AtheismRefs
    {
        public static readonly MethodInfo m_DefDatabase_IdeoPresetCategoryDef_get_AllDefsListForReading = AccessTools.Method(typeof(DefDatabase<IdeoPresetCategoryDef>), "get_AllDefsListForReading");
        public static readonly MethodInfo m_UIUtility_DoAtheismIdeoPresetSection = AccessTools.Method(typeof(UIUtility), nameof(UIUtility.DoAtheismIdeoPresetSection));
    }
}
