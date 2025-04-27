using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MelonLoader;
using System;

using ScheduleOne.UI.MainMenu;

namespace QualityOfLife.Interface.ModManager.Components;

public static class ModManagerButton
{
    private static readonly Action<GameObject> OnModManagerButtonClicked = HandleModManagerButtonClicked;

    public static void Setup(GameObject root, string sceneName)
    {
        var settingsButton = Utils.FindFirstValid(root.transform, "Home/Bank/Settings", "Container/Bank/Settings");
        var settingsScreen = Utils.FindFirstValid(root.transform, "Settings", "SettingsScreen_Ingame");

        if (settingsButton == null || settingsScreen == null)
        {
            MelonLogger.Warning("[ModManager] Failed to find Settings button or screen!");
            return;
        }

        root.SetActive(true);
        settingsScreen.SetActive(true);
        var modManagerScreen = ModManagerScreen.Create(settingsScreen);
        settingsScreen.SetActive(false);

        CreateModManagerButton(settingsButton, modManagerScreen);
    }

    private static void CreateModManagerButton(GameObject templateButton, GameObject modManagerScreen)
    {
        var modManagerButton = GameObject.Instantiate(templateButton, templateButton.transform.parent, false);
        if (modManagerButton == null)
        {
            MelonLogger.Warning("[ModManager] Failed to clone Settings button!");
            return;
        }

        modManagerButton.name = "ModManager";
        modManagerButton.transform.SetSiblingIndex(3);

        if (modManagerButton.TryGetComponent(out Button modManagerButtonComponent))
        {
            modManagerButtonComponent.onClick = new Button.ButtonClickedEvent();
            modManagerButtonComponent.onClick.AddListener(() => HandleModManagerButtonClicked(modManagerScreen));
        }

        var textObject = modManagerButton.transform.Find("TextContainer/Text")?.gameObject;
        if (textObject?.TryGetComponent(out TextMeshProUGUI text) == true)
        {
            text.text = "Mod Manager";
        }
        else
        {
            MelonLogger.Warning("[ModManager] Could not set button text.");
        }
    }

    private static void HandleModManagerButtonClicked(GameObject modManagerScreen)
    {
        if (modManagerScreen.TryGetComponent(out SettingsScreen screen))
        {
            screen.Open(true);
        }
        else
        {
            MelonLogger.Warning("[ModManager] Failed to find SettingsScreen on Mod Manager Screen!");
        }
    }
}
