using Atheism.Scripture;
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
        public static readonly FieldInfo f_MemeDef_descriptionMaker = AccessTools.Field(typeof(MemeDef), nameof(MemeDef.descriptionMaker));
        public static readonly FieldInfo f_IdeoManager_classicMode = AccessTools.Field(typeof(IdeoManager), nameof(IdeoManager.classicMode));
        public static readonly FieldInfo f_PreceptDef_visible = AccessTools.Field(typeof(PreceptDef), nameof(PreceptDef.visible));

        public static readonly MethodInfo m_DefDatabase_IdeoPresetCategoryDef_get_AllDefsListForReading = AccessTools.Method(typeof(DefDatabase<IdeoPresetCategoryDef>), "get_AllDefsListForReading");
        public static readonly MethodInfo m_UIUtility_DoAtheismIdeoPresetSection = AccessTools.Method(typeof(UIUtility), nameof(UIUtility.DoAtheismIdeoPresetSection));
        public static readonly MethodInfo m_IdeoUtility_GetIdeoFoundationDef = AccessTools.Method(typeof(Ideo.IdeoUtility), nameof(Ideo.IdeoUtility.GetIdeoFoundationDef));
        public static readonly MethodInfo m_GenCollection_RandomElement_IdeoFoundationDef = AccessTools.Method(typeof(GenCollection), nameof(GenCollection.RandomElement), null, new[] { typeof(IdeoFoundationDef) });
        public static readonly MethodInfo m_UIUtility_GetNameAndSymbolTooltip = AccessTools.Method(typeof(UIUtility), nameof(UIUtility.GetNameAndSymbolTooltip));
        public static readonly MethodInfo m_TooltipHandler_TipRegion = AccessTools.Method(typeof(TooltipHandler), nameof(TooltipHandler.TipRegion), new[] { typeof(Rect), typeof(TipSignal) });
        public static readonly MethodInfo m_IdeoUIUtility_get_DevEditMode = AccessTools.Method(typeof(IdeoUIUtility), "get_DevEditMode");
        public static readonly MethodInfo m_IdeoUtility_IsAtheism_Ideo = AccessTools.Method(typeof(Ideo.IdeoUtility), nameof(Ideo.IdeoUtility.IsAtheism), new[] { typeof(RimWorld.Ideo) });
        public static readonly MethodInfo m_SocialCardUtility_DrawPawnCertainty = AccessTools.Method(typeof(SocialCardUtility), nameof(SocialCardUtility.DrawPawnCertainty));
        public static readonly MethodInfo m_Pawn_get_Ideo = AccessTools.Method(typeof(Pawn), "get_Ideo");
        public static readonly MethodInfo m_ModsConfig_get_IdeologyActive = AccessTools.Method(typeof(ModsConfig), "get_IdeologyActive");
        public static readonly MethodInfo m_IdeoUtility_GetAllNonAtheismIdeoIconDefs = AccessTools.Method(typeof(Ideo.IdeoUtility), nameof(Ideo.IdeoUtility.GetAllNonAtheismIdeoIconDefs));
        public static readonly MethodInfo m_DefDatabase_IdeoIconDef_get_AllDefs = AccessTools.Method(typeof(DefDatabase<IdeoIconDef>), "get_AllDefs");
        public static readonly MethodInfo m_Find_get_IdeoManager = AccessTools.Method(typeof(Find), "get_IdeoManager");
        public static readonly MethodInfo m_Ideo_get_RitualSeatDef = AccessTools.Method(typeof(RimWorld.Ideo), "get_RitualSeatDef");
        public static readonly MethodInfo m_Pawn_IdeoTracker_IdeoConversionAttempt = AccessTools.Method(typeof(Pawn_IdeoTracker), nameof(Pawn_IdeoTracker.IdeoConversionAttempt));
        public static readonly MethodInfo m_IdeoUtility_WasEverAtheist = AccessTools.Method(typeof(Ideo.IdeoUtility), nameof(Ideo.IdeoUtility.WasEverAtheist));
        public static readonly MethodInfo m_ConversionUtility_TryIncrementDiscoveryProgress = AccessTools.Method(typeof(Conversion.ConversionUtility), nameof(Conversion.ConversionUtility.TryIncrementDiscoveryProgress));
        public static readonly MethodInfo m_BookOutcomeDoer_OnBookGenerated = AccessTools.Method(typeof(BookOutcomeDoer), nameof(BookOutcomeDoer.OnBookGenerated));
        public static readonly MethodInfo m_ScriptureUtility_AddTopicDynamicRules = AccessTools.Method(typeof(ScriptureUtility), nameof(ScriptureUtility.AddTopicDynamicRules));
    }
}
