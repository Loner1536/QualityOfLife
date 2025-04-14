using UnityEngine;
using HarmonyLib;
using ScheduleOne.UI.MainMenu;

namespace QualityOfLife
{
    [HarmonyPatch(typeof(Disclaimer), "Awake")]
    class Disclaimer_Awake_Patch
    {
        [HarmonyPrefix]
        private static void Prefix(Disclaimer __instance)
        {
            GameObject DisclaimerText = GameObject.Find("DisclaimerCanvas/Disclaimer/Text (TMP)").gameObject;
            GameObject.Destroy(DisclaimerText);

            __instance.Duration = 0.01f;
            var fadeMethod = typeof(Disclaimer).GetMethod("Fade", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            fadeMethod?.Invoke(__instance, null);
        }
    }
}
