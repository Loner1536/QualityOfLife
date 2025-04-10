using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using HarmonyLib;
using Il2CppScheduleOne.UI;
using Il2CppScheduleOne.Product;
using Il2CppScheduleOne.PlayerScripts;

[assembly: MelonInfo(typeof(QualityOfLife.Core), "QualityOfLife", "0.0.1", "Loner")]
[assembly: MelonGame("TVGS", "Schedule I")]
[assembly: MelonColor(255, 255, 255, 0)]

namespace QualityOfLife
{
    public static class BuildInfo
    {
        public const string Name = "QualityOfLife";
        public const string Description = "";
        public const string Author = "Loner";
        public const string Company = null;
        public const string Version = "0.0.1";
        public const string DownloadLink = null;
    }

    public static class Variables
    {
        public static GameObject? quitButton;
        public static GameObject? creditsButton;
        public static GameObject? settingsButton;
        public static GameObject? managerSettingsButton;
    }

    public static class Settings
    {

    }

    public class Core : MelonMod
    {
        // Scene Loading
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg($"Scene Loaded: {sceneName}");
            if (sceneName == "Menu")
            {
                if (GameObject.Find("MainMenu/Home/Bank") == null)
                {
                    MelonLogger.Warning("MenuUI not found, Please check if the scene is loaded correctly");
                    return;
                }
                else
                {
                    // Define Variables
                    Variables.settingsButton = GameObject.Find("MainMenu/Home/Bank/Settings");
                    Variables.creditsButton = GameObject.Find("MainMenu/Home/Bank/Credits");
                    Variables.quitButton = GameObject.Find("MainMenu/Home/Bank/Quit");
                    // Functions
                    CreateManager();
                }
            }
        }
        // Save Preferences
        public override void OnDeinitializeMelon()
        {
            string filePath = System.IO.Path.Combine("UserData", "QualityOfLife.cfg");
            System.IO.File.WriteAllText(filePath, "[Settings]\n");
        }
        // Public Functions For Small But Useful Features
        public void ChangeGameObjectName(GameObject gameObject, string newName)
        {
            if (gameObject != null)
            {
                gameObject.name = newName;
            }
            else
            {
                MelonLogger.Warning("The provided GameObject is null.");
            }
        }
        // public void ChangeGameObjectText(GameObject gameObject, string newText)
        // {
        //     if (gameObject != null)
        //     {
        //         Text textComponent = gameObject.GetComponent<>();
        //         if (textComponent != null)
        //         {
        //             textComponent.text = newText;
        //         }
        //         else
        //         {
        //             MelonLogger.Warning("Text component not found on " + gameObject.name);
        //         }
        //     }
        //     else
        //     {
        //         MelonLogger.Warning("The provided GameObject is null.");
        //     }
        // }
        public void ChangeGameObjectPos(GameObject gameObject, int x, int y, int? z = null)
        {

        }
        public void ChangeGameObjectParent(GameObject gameObject, Transform parent)
        {
            if (gameObject != null && parent != null)
            {
                gameObject.transform.SetParent(parent);
            }
            else
            {
                MelonLogger.Warning("Either the provided GameObject or parent GameObject is null.");
            }
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ToggleMenu();
            }
        }

        private void CreateManager()
        {
            if (Variables.settingsButton == null)
            {
                MelonLogger.Warning("Settings button not found.");
                return;
            }
            else
            {
                Variables.managerSettingsButton = GameObject.Instantiate(Variables.settingsButton.gameObject, Variables.settingsButton.transform.parent);
            }

            var Settings = Variables.settingsButton;
            var Manager = Variables.managerSettingsButton;

            RectTransform SettingsRectTransform = Settings.GetComponent<RectTransform>();
            RectTransform ManagerRectTransform = Manager.GetComponent<RectTransform>();
            // MANAGER BUTTON
            if (SettingsRectTransform == null)
            {
                MelonLogger.Warning("Failed to get RectTransform components from Settings.");
                return;
            }
            else if (ManagerRectTransform == null)
            {
                MelonLogger.Warning("Failed to get RectTransform components from Manager.");
            }
            else
            {
                ManagerRectTransform.anchorMin = SettingsRectTransform.anchorMin;
                ManagerRectTransform.anchorMax = SettingsRectTransform.anchorMax;
                ManagerRectTransform.pivot = SettingsRectTransform.pivot;
                ManagerRectTransform.sizeDelta = SettingsRectTransform.sizeDelta;

                Vector2 anchoredPosition = SettingsRectTransform.anchoredPosition;
                anchoredPosition.y = SettingsRectTransform.sizeDelta.y + -325f;
                ManagerRectTransform.anchoredPosition = anchoredPosition;

                Vector3 localPosition = Manager.transform.localPosition;
                localPosition.z = Settings.transform.localPosition.z;
                Manager.transform.localPosition = localPosition;

                LayoutElement layoutElement = Manager.AddComponent<LayoutElement>();
                layoutElement.ignoreLayout = true;

                Manager.name = "Manager";
            }
            // CREDITS BUTTON
            if (Variables.creditsButton == null)
            {
                MelonLogger.Warning("Credits button not found.");
                return;
            }
            var Credits = Variables.creditsButton;

            RectTransform CreditsRectTransform = Credits.GetComponent<RectTransform>();

            if (CreditsRectTransform == null)
            {
                MelonLogger.Warning("Failed to get RectTransform components from Credits.");
            }
        }

        public void ToggleMenu()
        {
            GameObject gameObject = GameObject.Find("MainMenu/Home/Bank");
            if (gameObject != null)
            {
                gameObject.SetActive(!gameObject.activeSelf);
            }
        }
    }
}
