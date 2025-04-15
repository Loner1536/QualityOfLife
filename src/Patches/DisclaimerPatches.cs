using UnityEngine;
using HarmonyLib;
using ScheduleOne.UI.MainMenu;
using System.Reflection;

namespace Core.Patches
{
    [HarmonyPatch(typeof(Disclaimer))]
    public class DisclaimerPatches
    {
        [HarmonyPatch(nameof(Disclaimer.Awake))]
        [HarmonyPrefix]
        private static void Prefix(Disclaimer __instance)
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
}
