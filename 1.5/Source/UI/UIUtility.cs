using Atheism.Dev;
using Atheism.Ideo;
using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Atheism.UI
{
    public static class UIUtility
    {
        public static void DoAtheismInfo(ref float curY, float width)
        {
            Widgets.Label(0f, ref curY, width, "Atheism_IdeoLiberationProgress".Translate());
            Widgets.Label(0f, ref curY, width, "Atheism_IdeoLiberationProgressDesc".Translate());
            curY += 16f;

            foreach (RimWorld.Ideo ideo in Find.IdeoManager.IdeosInViewOrder.Where(i => !i.IsAtheism()))
            {
                Rect iconRect = new Rect(0f, curY, Text.LineHeight, Text.LineHeight);
                Widgets.DrawHighlightIfMouseover(iconRect);
                ideo.DrawIcon(iconRect);
                if (Widgets.ButtonInvisible(iconRect))
                {
                    IdeoUIUtility.selected = ideo;
                    SoundDefOf.DialogBoxAppear.PlayOneShotOnCamera();
                }
                Widgets.Label(Text.LineHeight + 8f, ref curY, width - Text.LineHeight - 8f, ideo.name);
                curY += 8f;

                Precept altarPrecept = ideo.PreceptsListForReading.Where(p => p.IsAltar()).FirstOrDefault();
                if (altarPrecept != null)
                {
                    DoLiberationPrecept(new Rect(0, curY, IdeoUIUtility.PreceptBoxSize.x, IdeoUIUtility.PreceptBoxSize.y), altarPrecept);
                    curY += IdeoUIUtility.PreceptBoxSize.y;
                    curY += 8f;
                }

                List<Precept> relicPrecepts = ideo.PreceptsListForReading.Where(p => p.IsRelic()).ToList();
                if (relicPrecepts.Any())
                {
                    for (int i = 0; i < relicPrecepts.Count; i++)
                    {
                        DoLiberationPrecept(new Rect(i * (IdeoUIUtility.PreceptBoxSize.x + 8f), curY, IdeoUIUtility.PreceptBoxSize.x, IdeoUIUtility.PreceptBoxSize.y), relicPrecepts[i]);
                    }
                    curY += IdeoUIUtility.PreceptBoxSize.y;
                    curY += 8;
                }

                curY += 16f;
            }
        }

        private static void DoLiberationPrecept(Rect rect, Precept precept)
        {
            precept.DrawPreceptBox(rect, IdeoEditMode.None);

            List<FloatMenuOption> options = new List<FloatMenuOption>();
            if (Prefs.DevMode)
            {
                options.AddRange(DevUtility.GetDiscoveryProgressOptions(precept));
            }
            if (options.Any() && Widgets.ButtonInvisible(rect, false))
            {
                Find.WindowStack.Add(new FloatMenu(options));
            }
        }

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
