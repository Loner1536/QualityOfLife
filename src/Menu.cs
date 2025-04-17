using MelonLoader;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace QualityOfLife.Menu;

public static class Core
{
    public static void Initialize(GameObject mainMenu)
    {
        if (mainMenu != null)
        {
            var allActions = new List<Action<GameObject>>();
            allActions.AddRange(QualityOfLife.Menu.ModManager.GetInitializers());

            foreach (var action in allActions)
            {
                action?.Invoke(mainMenu);
            }
        }
        else
        {
            MelonLogger.Warning($"[Menu] {mainMenu} GameObject not found. Skipping initialization.");
        }
    }
}
