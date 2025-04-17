using System;
using System.Reflection;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Modules.ModManager;

public static class Config
{
    public static List<Action<GameObject>> GetInitializers()
    {
        return new List<Action<GameObject>>
        {
            (mainMenu) => MelonLogger.Msg("[ModManager] Initializing Configs..."),
            (mainMenu) => LoadCUETest()
        };
    }

    private static void LoadCUETest()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            if (assembly.GetName().Name.Contains("CUE") || assembly.GetName().Name.Contains("CinematicUnityExplorer"))
            {
                MelonLogger.Msg($"Found CUE assembly: {assembly.FullName}");

                foreach (var type in assembly.GetTypes())
                {
                    if (type.Name.Contains("Settings") || type.Name.Contains("Config"))
                    {
                        MelonLogger.Msg($"Found potential settings class: {type.FullName}");

                        var settingsInstance = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
                        if (settingsInstance != null)
                        {
                            var field = type.GetField("ShowFPS", BindingFlags.Public | BindingFlags.Instance);
                            if (field != null)
                            {
                                MelonLogger.Msg($"Current ShowFPS: {field.GetValue(settingsInstance)}");

                                // Change value
                                field.SetValue(settingsInstance, false);
                                MelonLogger.Msg("Set ShowFPS to false.");
                            }
                        }
                    }
                }
            }
        }
    }
}
