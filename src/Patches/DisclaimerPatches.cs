using UnityEngine;
using HarmonyLib;
using ScheduleOne.UI.MainMenu;
using System.Reflection;

namespace QualityOfLife.Patches;

[HarmonyPatch(typeof(Disclaimer))]
public class DisclaimerPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Disclaimer.Awake))]
    private static void Awake_Prefix(Disclaimer __instance)
    {
        var disclaimerText = GameObject.Find("DisclaimerCanvas/Disclaimer/Text (TMP)");
        if (disclaimerText != null)
        {
            GameObject.Destroy(disclaimerText);
        }

        __instance.Duration = 0.01f;

        var fadeMethod = typeof(Disclaimer).GetMethod("Fade", BindingFlags.Instance | BindingFlags.NonPublic);
        fadeMethod?.Invoke(__instance, null);
    }
}
