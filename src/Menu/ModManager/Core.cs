using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using ScheduleOne.UI.MainMenu;
using TMPro;

namespace QualityOfLife.Menu.ModManager;

public class Core
{
    public static List<Action<GameObject>> GetInitializers()
    {
        var initializers = new List<Action<GameObject>>
        {
            SetupManagerButton,
            RearrangeMenu,
        };

        initializers.AddRange(QualityOfLife.Menu.ModManager.Mods.GetInitializers());
        return initializers;
    }

    private static void RearrangeMenu(GameObject mainMenu)
    {
        string[] buttonPaths = { "Home/Bank/Settings", "Home/Bank/Credits", "Home/Bank/Quit" };

        foreach (string path in buttonPaths)
        {
            Transform button = mainMenu.transform.Find(path);
            if (button != null && button.TryGetComponent(out RectTransform buttonRect))
                buttonRect.anchoredPosition -= new Vector2(0, 40);
            else if (button != null && !button.TryGetComponent(out RectTransform _))
                MelonLogger.Warning($"[ModManager] Failed to get RectTransform for {path}!");
            else
                MelonLogger.Warning($"[ModManager] Failed to find {path} in Main Menu!");
        }
    }
    private static void SetupManagerButton(GameObject mainMenu)
    {
        GameObject settingsButton = mainMenu.transform.Find("Home/Bank/Settings")?.gameObject;
        GameObject settingsScreen = mainMenu.transform.Find("Settings")?.gameObject;

        if (settingsButton == null || settingsScreen == null)
        {
            MelonLogger.Warning("[ModManager] Failed to find Settings button or screen!");
            return;
        }

        settingsScreen.SetActive(true);
        GameObject modManagerScreen = CreateModManagerScreen(settingsScreen);
        settingsScreen.SetActive(false);

        CreateModManagerButton(settingsButton, modManagerScreen);
    }

    private static GameObject CreateModManagerScreen(GameObject settingsScreen)
    {
        GameObject modManagerScreen = GameObject.Instantiate(settingsScreen);
        modManagerScreen.transform.SetParent(settingsScreen.transform.parent, false);

        if (modManagerScreen == null)
            MelonLogger.Warning("[ModManager] Failed to clone Settings screen!");
        else
        {
            modManagerScreen.name = "ModManager";
            modManagerScreen.transform.SetSiblingIndex(4);

            GameObject modManagerScreenTitle = modManagerScreen.transform.Find("Title")?.gameObject;
            if (modManagerScreenTitle != null && modManagerScreenTitle.TryGetComponent(out TextMeshProUGUI modManagerScreenTitleTMPro))
            {
                modManagerScreenTitleTMPro.text = "Mod Manager";
                modManagerScreenTitleTMPro.fontSize = 20;
            }
            else if (modManagerScreenTitle != null && modManagerScreenTitle.TryGetComponent(out Text modManagerScreenTitleTextText))
                MelonLogger.Warning("[ModManager] Failed to find Mod Manager Title in Mod Manager Screen!");
            else
                MelonLogger.Warning("[ModManager] Failed to get Mod Manager Title TMPro in Mod Manager Screen!");

            Transform modManagerScreenContent = modManagerScreen.transform.Find("Content");
            if (modManagerScreenContent)
            {
                foreach (Transform child in modManagerScreenContent)
                {
                    if (child.name == "Display")
                    {
                        foreach (Transform grandChild in child)
                            GameObject.Destroy(grandChild.gameObject);

                        GameObject.Destroy(child.GetComponent<VerticalLayoutGroup>());

                        GameObject WIPTitle = GameObject.Instantiate(modManagerScreenTitle, child, false);
                        WIPTitle.name = "WIPTitle";
                        if (WIPTitle != null && WIPTitle.TryGetComponent(out TextMeshProUGUI WIPTitleText))
                        {
                            WIPTitleText.text = "Work In Progress";
                            WIPTitleText.fontSize = 20;
                            RectTransform WIPTitleRect = WIPTitle.GetComponent<RectTransform>();
                            if (WIPTitleRect != null)
                            {
                                WIPTitleRect.anchoredPosition = new Vector2(240, -100);
                            }
                            else
                                MelonLogger.Warning("[ModManager] Failed to get RectTransform for WIP Title!");
                        }
                        else
                            MelonLogger.Warning("[ModManager] Failed to get Mod Manager Title TMPro in Mod Manager Screen!");
                    }
                    else GameObject.Destroy(child.gameObject);
                }
            }
            Transform modManagerScreenButtons = modManagerScreen.transform.Find("Buttons");
            if (modManagerScreenButtons)
            {
                foreach (Transform child in modManagerScreenButtons)
                {
                    if (child.name == "Display")
                    {
                        GameObject chilName = child.Find("Name").gameObject;
                        if (chilName != null && chilName.TryGetComponent(out TextMeshProUGUI childNameTMPro))
                        {
                            childNameTMPro.text = "Test";
                        }
                        else
                            MelonLogger.Warning("[ModManager] Failed to get Mod Manager Title TMPro in Mod Manager Screen!");
                        continue;
                    }
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
        return modManagerScreen;
    }

    private static void CreateModManagerButton(GameObject settingsButton, GameObject modManagerScreen)
    {
        GameObject modManagerButton = GameObject.Instantiate(settingsButton, settingsButton.transform.parent, false);
        if (modManagerButton == null)
            MelonLogger.Warning("[ModManager] Failed to clone Settings button!");
        else
        {
            modManagerButton.name = "ModManager";
            modManagerButton.transform.SetSiblingIndex(3);

            if (modManagerButton.TryGetComponent(out Button modManagerButtonComponent))
            {
                modManagerButtonComponent.onClick = new Button.ButtonClickedEvent();
                modManagerButtonComponent.onClick.AddListener(() =>
                {
                    if (modManagerScreen.TryGetComponent(out SettingsScreen screen))
                        screen.Open(true);
                    else
                        MelonLogger.Warning("[ModManager] Failed to find SettingsScreen on Mod Manager Screen!");
                });
            }
            else
                MelonLogger.Warning("[ModManager] Failed to get Button component from Mod Manager Button!");

            GameObject modManagerText = modManagerButton.transform.Find("TextContainer/Text").gameObject;
            if (modManagerText != null && modManagerText.TryGetComponent(out TextMeshProUGUI modManagerTextTMPro))
                modManagerTextTMPro.text = "Mod Manager";
            else if (modManagerText != null && modManagerText.TryGetComponent(out Text modManagerTextText))
                MelonLogger.Warning("[ModManager] Failed to find Mod Manager Text in Mod Manager Button!");
            else
                MelonLogger.Warning("[ModManager] Failed to get Mod Manager Text TMPro in Mod Manager Button!");
        }
    }
}
