using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Atheism.UI
{
    public static class UIUtility
    {
        public static void DoAtheismIdeoPresetSection(Page_ChooseIdeoPreset page, float x, ref float num)
        {
            IdeoPresetDef atheismDef = DefDatabase<IdeoPresetDef>.GetNamed("Atheism");

            Widgets.Label(new Rect(0f, num, 300f, Text.LineHeight), "Atheism_Atheism".Translate());
            GUI.color = new Color(1f, 1f, 1f, 0.5f);
            Widgets.DrawLineHorizontal(0f, num + Text.LineHeight + 2f, 901f);
            GUI.color = Color.white;
            num += 12f;
            Rect rect = new Rect(x, num + Text.LineHeight, 160f, 60f);
            typeof(Page_ChooseIdeoPreset).Method("<DoWindowContents>g__DrawSplitCategoryInfo|26_0").Invoke(null, new object[] { DefDatabase<IdeoPresetCategoryDef>.GetNamed("Atheism"), rect });
            typeof(Page_ChooseIdeoPreset).Method("DrawSelectable").Invoke(page, new object[] 
            { 
                rect, 
                "Atheism_Atheism".Translate().ToString(), 
                null, 
                4,
                typeof(Page_ChooseIdeoPreset).Field("selectedIdeo").GetValue(page) == atheismDef, 
                true, 
                null, 
                (Action)delegate
                {
                    typeof(Page_ChooseIdeoPreset).Field("selectedIdeo").SetValue(page, atheismDef);
                    typeof(Page_ChooseIdeoPreset).Field("presetSelection").SetValue(page, typeof(Page_ChooseIdeoPreset).GetNestedType("PresetSelection", BindingFlags.NonPublic).Field("Preset").GetValue(page));
                }
            });
            num = rect.yMax + 10f;
        }

        public static TipSignal GetNameAndSymbolTooltip(RimWorld.Ideo ideo, IdeoEditMode editMode)
        {
            return string.Concat(new string[]
            {
                ("Name".Translate() + ": ").Colorize(ColoredText.TipSectionTitleColor),
                ideo.name,
                "\n",
                ("Adjective".Translate() + ": ").Colorize(ColoredText.TipSectionTitleColor),
                ideo.adjective.CapitalizeFirst(),
                "\n",
                ("IdeoMembers".Translate() + ": ").Colorize(ColoredText.TipSectionTitleColor),
                ideo.memberName.CapitalizeFirst(),
                "\n",
                ideo.leaderTitleMale != null ? ("LeaderTitle".Translate() + ": ").Colorize(ColoredText.TipSectionTitleColor) + ideo.leaderTitleMale.CapitalizeFirst() + "\n" : "",
                ideo.WorshipRoomLabel != null ? ("WorshipRoom".Translate() + ": ").Colorize(ColoredText.TipSectionTitleColor) + ideo.WorshipRoomLabel.CapitalizeFirst() + "\n" : "",
                (ideo.leaderTitleFemale != ideo.leaderTitleMale) ? (" (" + ideo.leaderTitleFemale.CapitalizeFirst() + ")") : "",
                (editMode != IdeoEditMode.None) ? ("\n" + IdeoUIUtility.ClickToEdit) : string.Empty
            });
        }
    }
}
