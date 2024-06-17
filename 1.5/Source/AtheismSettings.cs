using UnityEngine;
using Verse;

namespace Atheism
{
    public class AtheismSettings : ModSettings
    {
        public static float AtheismConversionPower = 2f;
        public static bool AtheismQuickTest = false;
        public static float IdeologicalVariationChance = 0.1f;
        public static IntRange DeityCountRange = IntRange.one;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            AtheismConversionPower = Mathf.RoundToInt(listingStandard.SliderLabeled("Atheism_AtheismConversionPower".Translate(AtheismConversionPower), AtheismConversionPower, 1f, 5f, 0.5f, "Atheism_AtheismConversionPowerDesc".Translate()) * 100f) / 100f;
            IdeologicalVariationChance = Mathf.RoundToInt(listingStandard.SliderLabeled("Atheism_IdeologicalVariationChance".Translate(IdeologicalVariationChance), IdeologicalVariationChance, 0f, 1f, 0.5f, "Atheism_IdeologicalVariationChanceDesc".Translate()) * 100f) / 100f;
            Rect deityCountRangeRect = listingStandard.GetRect(32f);
            Rect deityCountRangeLabelRect = new Rect(deityCountRangeRect);
            deityCountRangeLabelRect.width /= 2;
            Widgets.Label(deityCountRangeLabelRect, "Atheism_DeityCountRange".Translate());
            TooltipHandler.TipRegionByKey(deityCountRangeLabelRect, "Atheism_DeityCountRangeDesc");
            Rect deityCountRangeSliderRect = new Rect(deityCountRangeRect);
            deityCountRangeSliderRect.width /= 2;
            deityCountRangeSliderRect.x += deityCountRangeLabelRect.width;
            Widgets.IntRange(deityCountRangeSliderRect, (int)deityCountRangeSliderRect.y, ref DeityCountRange, 0, 4);

            if (Prefs.DevMode)
            {
                listingStandard.CheckboxLabeled("Atheism_AtheismQuickTest".Translate(), ref AtheismQuickTest, "Atheism_AtheismQuickTestDesc".Translate());
            }

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref AtheismConversionPower, "AtheismConversionPower", 2f);
            Scribe_Values.Look(ref AtheismQuickTest, "AtheismQuickTest", false);
            Scribe_Values.Look(ref IdeologicalVariationChance, "IdeologicalVariationChance", 0.1f);
            Scribe_Values.Look(ref DeityCountRange, "DeityCountRange", IntRange.one);
        }
    }
}
