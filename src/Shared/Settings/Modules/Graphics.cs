using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace QualityOfLife.Modules.Settings;

public static class Graphics
{
    public static List<Action<GameObject>> GetInitializers()
    {
        return new List<Action<GameObject>>
        {
            (settingsMenu) => MelonLogger.Msg("[Settings] Initializing Graphics settings...")
        };
    }
}
