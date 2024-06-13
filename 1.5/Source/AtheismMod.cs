using HarmonyLib;
using Verse;

namespace Atheism
{
    public class AtheismMod : Mod
    {
        public const string PACKAGE_ID = "atheism.1trickPwnyta";
        public const string PACKAGE_NAME = "Atheism";

        public AtheismMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
