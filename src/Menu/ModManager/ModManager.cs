using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Menu;

public class ModManager
{
    public static List<Action<GameObject>> GetInitializers()
    {
        return new List<Action<GameObject>>
        {
            (mainMenu) => MelonLogger.Msg("[Settings] Initializing Mod Manager...")
        };
    }
}
