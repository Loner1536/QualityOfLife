using MelonLoader;
using UnityEngine;
using HarmonyLib;
using ScheduleOne.UI.MainMenu;

[assembly: MelonInfo(typeof(QualityOfLife.Core), "QualityOfLife", "0.0.1", "Loner")]
[assembly: MelonGame("TVGS", "Schedule I")]
[assembly: MelonColor(255, 0, 255, 255)]

namespace QualityOfLife
{
    public class Core : MelonMod
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

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "Menu")
            {
                GameObject MainMenuObject = GameObject.Find("MainMenu");

                if (MainMenuObject == null)
                {
                    MelonLogger.Warning("[Core] 'MainMenu' GameObject not found.");
                }
                else
                {
                    QOLSettings.Initialize(MainMenuObject);
                    SmoothFadeAllDescendants.Initialize(MainMenuObject);
                }
            }
            else if (sceneName == "Main")
            {
                GameObject PauseMenuObject = GameObject.Find("UI/PauseMenu/Container");
                if (PauseMenuObject == null)
                {
                    MelonLogger.Warning("[Core] 'PauseMenu' GameObject not found.");
                }
                else
                {
                    QOLSettings.Initialize(PauseMenuObject);
                    SmoothFadeAllDescendants.Initialize(PauseMenuObject);
                }
            }
            else
            {
                MelonLogger.Msg($"[Core] Scene '{sceneName}' loaded.");
            }
        }

        public override void OnDeinitializeMelon()
        {
            string filePath = Path.Combine("UserData", "QualityOfLife.cfg");

            try
            {
                File.WriteAllText(filePath, "[Settings]\n");
                MelonLogger.Msg($"[Core] Config file written to {filePath}");
            }
            catch (IOException e)
            {
                MelonLogger.Warning($"[Core] Failed to write config file: {e.Message}");
            }
        }
    }
}
