using UnityEngine;
using Verse;

namespace Atheism
{
    public class AtheismSettings : ModSettings
    {
        public static float AtheismConversionPower = 2f;
        public static bool AtheismQuickTest = false;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            AtheismConversionPower = Mathf.RoundToInt(listingStandard.SliderLabeled("Atheism_AtheismConversionPower".Translate(AtheismConversionPower), AtheismConversionPower, 1f, 5f, 0.5f, "Atheism_AtheismConversionPowerDesc".Translate()));

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
        }
    }
}
