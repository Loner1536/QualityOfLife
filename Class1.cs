using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Il2CppInterop.Runtime;
using Il2CppScheduleOne.UI.MainMenu;
using Il2CppSystem;
using Il2CppTMPro;
using MelonLoader;
using MelonLoader.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public static class Settings
    {
    }

    public class Core : MelonMod
    {
        // Global UI Buttons
        public static GameObject? SettingsButton = null;
        public static GameObject? CreditsButton = null;
        public static GameObject? QuitButton = null;

        public static bool MenuSceneLoaded = false;

        // Scene Loading
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "Menu")
            {
                if (GameObject.Find("MainMenu/Home/Bank") == null)
                {
                    MelonLogger.Warning("MenuUI not found, Please check if the scene is loaded correctly");
                    return;
                }
                else
                {
                    // Assign global variables
                    SettingsButton = GameObject.Find("MainMenu/Home/Bank/Settings");
                    CreditsButton = GameObject.Find("MainMenu/Home/Bank/Credits");
                    QuitButton = GameObject.Find("MainMenu/Home/Bank/Quit");
                    // Positioning
                    PositionGameObject(SettingsButton, new Vector2(
                        SettingsButton.GetComponent<RectTransform>().anchoredPosition.x,
                        SettingsButton.GetComponent<RectTransform>().anchoredPosition.y - 40f
                    ));
                    PositionGameObject(CreditsButton, new Vector2(
                        CreditsButton.GetComponent<RectTransform>().anchoredPosition.x,
                        CreditsButton.GetComponent<RectTransform>().anchoredPosition.y - 40f
                    ));
                    PositionGameObject(QuitButton, new Vector2(
                        QuitButton.GetComponent<RectTransform>().anchoredPosition.x,
                        QuitButton.GetComponent<RectTransform>().anchoredPosition.y - 40f
                    ));
                    // Functions
                    CreateManager();
                    // Flags
                    MenuSceneLoaded = true;
                }
            }
        }
        // Save Preferences
        public override void OnDeinitializeMelon()
        {
            string filePath = Path.Combine("UserData", "QualityOfLife.cfg");
            File.WriteAllText(filePath, "[Settings]\n");
        }

        private void CreateManager()
        {
            GameObject mainMenu = GameObject.Find("MainMenu");
            if (mainMenu == null)
            {
                MelonLogger.Warning("MainMenu object not found!");
                return;
            }

            if (Core.CreditsButton == null)
            {
                MelonLogger.Warning("Credits button not found!");
                return;
            }

            Transform creditButtonTransform = Core.CreditsButton.transform;
            Transform creditsScreen = mainMenu.transform.Find("Credits");
            if (creditsScreen == null)
            {
                MelonLogger.Warning("Credits screen not found!");
                return;
            }

            creditsScreen.gameObject.SetActive(true);
            if (creditButtonTransform == null)
            {
                MelonLogger.Warning("Credits object not found!");
                return;
            }

            GameObject managerScreen = UnityEngine.Object.Instantiate(creditsScreen.gameObject);
            creditsScreen.gameObject.SetActive(false);
            managerScreen.name = "Manager";
            managerScreen.transform.SetParent(mainMenu.transform, false);

            RectTransform managerBackground = managerScreen.transform.GetChild(0).GetComponent<RectTransform>();
            if (managerBackground == null)
            {
                MelonLogger.Warning("Background not found on ModManager screen!");
                return;
            }
            managerBackground.sizeDelta = new Vector2(450f, 150f);

            for (int i = managerScreen.transform.childCount - 1; i > 0; i--)
            {
                GameObject.Destroy(managerScreen.transform.GetChild(i).gameObject);
            }


            GameObject managerButton = UnityEngine.Object.Instantiate(creditButtonTransform.gameObject);
            if (managerButton == null)
            {
                MelonLogger.Warning("Failed to clone Credits button!");
                return;
            }

            managerButton.name = "Manager";
            managerButton.transform.SetParent(creditButtonTransform.parent, false);

            RectTransform managerRect = managerButton.GetComponent<RectTransform>();
            RectTransform creditRect = creditButtonTransform.GetComponent<RectTransform>();
            managerRect.anchoredPosition = new Vector2(
                creditRect.anchoredPosition.x,
                creditRect.anchoredPosition.y + 80f
            );

            Button buttonComponent = managerButton.GetComponent<Button>();
            if (buttonComponent == null)
            {
                MelonLogger.Warning("Button component not found on cloned Credits!");
                return;
            }

            buttonComponent.onClick = new Button.ButtonClickedEvent();
            buttonComponent.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(() =>
            {
                MainMenuScreen screen = managerScreen.GetComponent<MainMenuScreen>();
                if (screen != null)
                {
                    screen.Open(true);
                }
                else
                {
                    MelonLogger.Warning("MainMenuScreen component not found on ModManager!");
                }
            }));

            Transform textContainer = managerButton.transform.Find("TextContainer");
            if (textContainer == null)
            {
                MelonLogger.Warning("Text Container not found in cloned Credits!");
                return;
            }

            Transform text = textContainer.Find("Text");
            TextMeshProUGUI textMesh = text.GetComponent<TextMeshProUGUI>();
            if (textMesh == null)
            {
                MelonLogger.Warning("Menu Label Text not found in TextContainer!");
                return;
            }

            textMesh.text = "Mod Manager";

            SetupModList(managerBackground);
        }
        private void SetupModList(RectTransform managerBackground)
        {
            GameObject mainMenu = GameObject.Find("MainMenu");
            Transform SettingsCategoryButtons = mainMenu.transform.Find("Settings/Buttons");
            Transform SettingsCategoryContent = mainMenu.transform.Find("Settings/Content");
            Transform SettingsTitle = mainMenu.transform.Find("Settings/Title");

            GameObject ManagerCategoryButtons = UnityEngine.Object.Instantiate<GameObject>(SettingsCategoryButtons.gameObject, managerBackground);
            GameObject ManagerCategoryContent = UnityEngine.Object.Instantiate<GameObject>(SettingsCategoryContent.gameObject, managerBackground);
            GameObject ManagerTitle = UnityEngine.Object.Instantiate<GameObject>(SettingsTitle.gameObject, managerBackground);

            ManagerCategoryButtons.name = "Buttons";
            ManagerCategoryContent.name = "Content";
            ManagerTitle.name = "Title";

            GameObject[] objectsToDestroy = {
                ManagerCategoryButtons.transform.Find("Controls").gameObject,
                ManagerCategoryButtons.transform.Find("Audio").gameObject,
                ManagerCategoryContent.transform.Find("Controls").gameObject,
                ManagerCategoryContent.transform.Find("Audio").gameObject
            };
            foreach (var obj in objectsToDestroy)
            {
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }

            GameObject ButtonMods = ManagerCategoryButtons.transform.Find("Display").gameObject;
            GameObject ButtonConfigMods = ManagerCategoryButtons.transform.Find("Graphics").gameObject;

            ButtonMods.name = "Mods";
            ButtonConfigMods.name = "Config Mods";

            TextMeshProUGUI TitleText = ManagerTitle.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ModsText = ButtonMods.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ConfigModsText = ButtonConfigMods.transform.Find("Name").GetComponent<TextMeshProUGUI>();

            ConfigModsText.text = "Config Mods";
            TitleText.text = "Mod Manager";
            ModsText.text = "Mods";

            Button ButtonModsButton = ButtonMods.GetComponent<Button>();
            Button ButtonConfigModsButton = ButtonConfigMods.GetComponent<Button>();

            Button[] buttonsToAddPressEvent = {
                ButtonModsButton,
                ButtonConfigModsButton
            };
            foreach (Button obj in buttonsToAddPressEvent)
            {
                if (obj != null)
                {
                    obj.onClick = new Button.ButtonClickedEvent();
                    obj.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(() =>
                    {
                        if (obj.name == "Mods")
                        {
                            ButtonModsButton.interactable = false;
                            ButtonConfigModsButton.interactable = true;
                        }
                        else if (obj.name == "Config Mods")
                        {
                            ButtonModsButton.interactable = true;
                            ButtonConfigModsButton.interactable = false;
                        }
                    }));
                }
            }
        }

        private void SetupMods(Transform content)
        {
            return;
        }

        public void PositionGameObject(GameObject gameObject, Vector2 position)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = position;
            }
            else
            {
                MelonLogger.Warning("RectTransform component not found on GameObject!");
            }
        }
    }
}
