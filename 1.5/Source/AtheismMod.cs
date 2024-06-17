using Atheism.Ideo;
using Atheism.UI;
using HarmonyLib;
using RimWorld;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Atheism
{
    public class AtheismMod : Mod
    {
        public const string PACKAGE_ID = "atheism.1trickPwnyta";
        public const string PACKAGE_NAME = "Atheism";

        public static AtheismSettings Settings;

        public AtheismMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<AtheismSettings>();

            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            harmony.Patch(typeof(Page_ChooseIdeoPreset).GetNestedType("<>c", BindingFlags.NonPublic).Method("<DoWindowContents>b__26_5"), null, typeof(Patch_Page_ChooseIdeoPreset_c_DoWindowContents_b__26_5).Method(nameof(Patch_Page_ChooseIdeoPreset_c_DoWindowContents_b__26_5.Postfix)));
            harmony.Patch(typeof(MemeDef).GetNestedType("<ConfigErrors>d__52", BindingFlags.NonPublic).Method("MoveNext"), null, null, typeof(Patch_MemeDef).Method(nameof(Patch_MemeDef.Transpiler)));

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            AtheismSettings.DoSettingsWindowContents(inRect);
        }
    }
}
