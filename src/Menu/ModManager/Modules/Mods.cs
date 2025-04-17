using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Modules.ModManager;

public static class Mods
{
    public static List<Action<GameObject>> GetInitializers()
    {
        return new List<Action<GameObject>>
        {
            (mainMenu) => MelonLogger.Msg("[ModManager] Initializing Mods...")
        };
    }
}
