using MelonLoader;
using MelonLoader.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Il2CppTMPro;
using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppScheduleOne.UI;
using Il2CppScheduleOne.UI.MainMenu;
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

    public static class Settings
    {

    }

    public class Core : MelonMod
    {
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
                    // Functions
                    CreateManager();
                    // Varaibles
                    GameObject SettingsButton = GameObject.Find("MainMenu/Home/Bank/Settings");
                    GameObject CreditsButton = GameObject.Find("MainMenu/Home/Bank/Credits");
                    GameObject QuitButton = GameObject.Find("MainMenu/Home/Bank/Quit");
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
                }
            }
        }
        // Save Preferences
        public override void OnDeinitializeMelon()
        {
            string filePath = System.IO.Path.Combine("UserData", "QualityOfLife.cfg");
            System.IO.File.WriteAllText(filePath, "[Settings]\n");
        }

        private void CreateManager()
        {

            GameObject MainMenu = GameObject.Find("MainMenu");
            if (MainMenu != null)
            {
                GameObject SettingsMenu = MainMenu.transform.Find("Settings").gameObject;
                GameObject ManagerScreen = UnityEngine.Object.Instantiate<GameObject>(SettingsMenu, MainMenu.transform);
                ManagerScreen.name = "Manager";

                GameObject SettingsButton = MainMenu.transform.Find("Home/Bank/Settings").gameObject;
                GameObject ManagerButton = UnityEngine.Object.Instantiate<GameObject>(SettingsButton, SettingsButton.transform.parent);

                Button ManagerButtonComponent = ManagerButton.GetComponent<Button>();
                ManagerButtonComponent.onClick = new Button.ButtonClickedEvent();
                ManagerButtonComponent.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(delegate ()
                {
                    MainMenuScreen ManagerMainMenuScreenComponent = ManagerScreen.GetComponent<MainMenuScreen>();
                    if (ManagerMainMenuScreenComponent != null)
                    {
                        ManagerMainMenuScreenComponent.Open(true);
                    }
                    else
                    {
                        MelonLogger.Warning("MainMenuScreen component not found on ModManager!");
                    }
                }));

                GameObject ManagerButtonText = ManagerButton.transform.Find("TextContainer").gameObject.transform.Find("Text").gameObject;
                TextMeshProUGUI ManagerButtonTextComponent = ManagerButtonText.GetComponent<TextMeshProUGUI>();
                if (ManagerButtonTextComponent != null)
                {
                    ManagerButtonTextComponent.text = "Manager";
                }
                else
                {
                    MelonLogger.Warning("TextMeshProUGUI component not found on ManagerButton!");
                }
            }
            else
            {
                MelonLogger.Warning("MainMenu not found, Please check if the scene is loaded correctly");
                return;
            }
        }

        public void PositionGameObject(GameObject gameObject, Vector2 vector2)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = vector2;
            }
            else
            {
                MelonLogger.Warning("RectTransform component not found on GameObject!");
            }
        }
    }
}
