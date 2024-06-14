using Atheism.UI;
using HarmonyLib;
using RimWorld;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Atheism
{
    public static class AtheismRefs
    {
        public static readonly FieldInfo f_MemeDef_worshipRoomLabel = AccessTools.Field(typeof(MemeDef), nameof(MemeDef.worshipRoomLabel));

        public static readonly MethodInfo m_DefDatabase_IdeoPresetCategoryDef_get_AllDefsListForReading = AccessTools.Method(typeof(DefDatabase<IdeoPresetCategoryDef>), "get_AllDefsListForReading");
        public static readonly MethodInfo m_UIUtility_DoAtheismIdeoPresetSection = AccessTools.Method(typeof(UIUtility), nameof(UIUtility.DoAtheismIdeoPresetSection));
        public static readonly MethodInfo m_IdeoUtility_GetIdeoFoundationDef = AccessTools.Method(typeof(Ideo.IdeoUtility), nameof(Ideo.IdeoUtility.GetIdeoFoundationDef));
        public static readonly MethodInfo m_GenCollection_RandomElement_IdeoFoundationDef = AccessTools.Method(typeof(GenCollection), nameof(GenCollection.RandomElement), null, new[] { typeof(IdeoFoundationDef) });
        public static readonly MethodInfo m_UIUtility_GetNameAndSymbolTooltip = AccessTools.Method(typeof(UIUtility), nameof(UIUtility.GetNameAndSymbolTooltip));
        public static readonly MethodInfo m_TooltipHandler_TipRegion = AccessTools.Method(typeof(TooltipHandler), nameof(TooltipHandler.TipRegion), new[] { typeof(Rect), typeof(TipSignal) });
        public static readonly MethodInfo m_IdeoUIUtility_get_DevEditMode = AccessTools.Method(typeof(IdeoUIUtility), "get_DevEditMode");
        public static readonly MethodInfo m_IdeoUtility_IsAtheism = AccessTools.Method(typeof(Ideo.IdeoUtility), nameof(Ideo.IdeoUtility.IsAtheism));
    }
}
