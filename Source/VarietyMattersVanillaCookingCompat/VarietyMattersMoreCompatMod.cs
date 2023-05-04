using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Verse;

namespace VarietyMattersMoreCompat;

public class VarietyMattersMoreCompatMod : Mod
{
    public static VarietyMattersMoreCompatSettings settings;
    public static Harmony harmony;

    public VarietyMattersMoreCompatMod(ModContentPack content) : base(content)
    {
        settings = GetSettings<VarietyMattersMoreCompatSettings>();
        harmony = new Harmony("Dra.VarietyMattersMoreCompat");
        harmony.PatchAll();

        var hasVariety = false;
        var hasSupported = false;

        foreach (var mod in LoadedModManager.RunningMods)
        {
            var id = mod.PackageId.ToLower().NoModIdSuffix();

            if (!hasVariety && id is "cozarkian.varietymattersimproved" or "cozarkian.varietymattersimproved_1_4fork")
            {
                hasVariety = true;
                if (hasSupported) break;
            }
            else if (!hasSupported && id is "vanillaexpanded.vcooke" or "mrkociak.morearchotechstuffandthingsreupload")
            {
                hasSupported = true;
                if (hasVariety) break;
            }
        }

        if (!hasVariety)
            Log.Error("[Variety Matters - More Compat] - Variety Matters is not running, having this mod enabled is pointless.");
        else if (!hasSupported)
            Log.Error("[Variety Matters - More Compat] - no supported mod is running, having this mod enabled is pointless.");
    }

    public override string SettingsCategory() => "Variety Matters - More Compat";
    public override void DoSettingsWindowContents(Rect inRect) => settings.DoSettingsWindowContents(inRect);
}