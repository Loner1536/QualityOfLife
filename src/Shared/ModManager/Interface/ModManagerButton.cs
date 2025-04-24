using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MelonLoader;

namespace QualityOfLife.Menu.ModManager.Interface;

public static class ModManagerButton
{
    public static void Setup(GameObject mainMenu)
    {
        GameObject settingsButton = mainMenu.transform.Find("Home/Bank/Settings")?.gameObject;
        GameObject settingsScreen = mainMenu.transform.Find("Settings")?.gameObject;

        if (settingsButton == null || settingsScreen == null)
        {
            MelonLogger.Warning("[ModManager] Failed to find Settings button or screen!");
            return;
        }

        settingsScreen.SetActive(true);
        GameObject modManagerScreen = ModManagerScreen.Create(settingsScreen);
        settingsScreen.SetActive(false);

        CreateButton(settingsButton, modManagerScreen);
    }

    private static void CreateButton(GameObject settingsButton, GameObject modManagerScreen)
    {
        GameObject modManagerButton = GameObject.Instantiate(settingsButton, settingsButton.transform.parent, false);
        if (modManagerButton == null)
        {
            MelonLogger.Warning("[ModManager] Failed to clone Settings button!");
            return;
        }

        modManagerButton.name = "ModManager";
        modManagerButton.transform.SetSiblingIndex(3);

        if (modManagerButton.TryGetComponent(out Button button))
        {
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(() =>
            {
                if (modManagerScreen.TryGetComponent(out ScheduleOne.UI.MainMenu.SettingsScreen screen))
                    screen.Open(true);
                else
                    MelonLogger.Warning("[ModManager] Failed to find SettingsScreen on Mod Manager Screen!");
            });
        }

        GameObject modManagerText = modManagerButton.transform.Find("TextContainer/Text").gameObject;
        if (modManagerText != null && modManagerText.TryGetComponent(out TextMeshProUGUI text))
            text.text = "Mod Manager";
    }
}
