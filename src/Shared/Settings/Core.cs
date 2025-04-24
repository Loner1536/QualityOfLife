using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QualityOfLife.Shared.Settings;

public static class Core
{
    public static void Initialize(GameObject root, string sceneName)
    {
        string settingsObjectName;
        switch (sceneName)
        {
            case "Main":
            case "Tutorial":
                settingsObjectName = "SettingsScreen_Ingame";
                break;
            case "Menu":
                settingsObjectName = "Settings";
                break;
            default:
                settingsObjectName = null;
                break;
        }

        if (settingsObjectName != null)
        {
            GameObject settingsMenu = root.transform.Find(settingsObjectName)?.gameObject;

            if (settingsMenu != null)
            {
                var allActions = new List<Action<GameObject>>();
                allActions.AddRange(Interface.Graphics.GetInitializers());
                allActions.AddRange(Interface.Controls.GetInitializers());
                allActions.AddRange(Interface.Display.GetInitializers());
                allActions.AddRange(Interface.Audio.GetInitializers());

                foreach (var action in allActions)
                {
                    action?.Invoke(settingsMenu);
                }
            }
            else
            {
                MelonLogger.Warning($"[Settings] {settingsObjectName} GameObject not found. Skipping initialization.");
            }
        }
        else
        {
            MelonLogger.Warning($"[Settings] No valid settings menu for scene '{sceneName}'. Skipping initialization.");
        }
    }
}
