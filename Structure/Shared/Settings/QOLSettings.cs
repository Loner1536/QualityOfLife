using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core.Shared
{
    public static class Settings
    {
        public static void Initialize(GameObject root, string sceneName)
        {
            string settingsObjectName = sceneName switch
            {
                "Main" or "Tutorial" => "Settings_Ingame",
                "Menu" => "Settings",
                _ => null
            };

            if (settingsObjectName != null)
            {
                GameObject settingsMenu = root.transform.Find(settingsObjectName)?.gameObject;

                if (settingsMenu != null)
                {
                    var allActions = new List<Action<GameObject>>();
                    allActions.AddRange(Core.Shared.SettingsModules.Display.GetInitializers());
                    allActions.AddRange(Core.Shared.SettingsModules.Graphics.GetInitializers());
                    allActions.AddRange(Core.Shared.SettingsModules.Audio.GetInitializers());
                    allActions.AddRange(Core.Shared.SettingsModules.Controls.GetInitializers());

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
}
